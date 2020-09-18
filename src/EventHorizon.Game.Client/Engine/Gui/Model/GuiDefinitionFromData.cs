namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Dispose;
    using EventHorizon.Game.Client.Engine.Gui.Register;
    using EventHorizon.Game.Client.Engine.Gui.Setup;
    using EventHorizon.Game.Client.Engine.Gui.Update;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Scripting.Get;
    using EventHorizon.Game.Client.Engine.Scripting.Services;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using MediatR;

    public class GuiDefinitionFromData
        : ClientLifecycleEntityBase,
        IGuiDefinition
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly ILocalizer _localizer = GameServiceProvider.GetService<ILocalizer>();
        private readonly IGuiControlChildrenState _guiControlChildrenState = GameServiceProvider.GetService<IGuiControlChildrenState>();
        private readonly ScriptServices _scriptServices = GameServiceProvider.GetService<ScriptServices>();

        private readonly IGuiLayoutData _layout;
        private readonly IEnumerable<IGuiControlData> _controlDataList;
        private string? _parentControlId;
        private bool _initialized = false;
        private bool _runActivate = false;
        private IList<IGuiLayoutControlData> _flattenedControlList = new List<IGuiLayoutControlData>();
        private Func<Task> _handleUpdateScript;
        private Func<Task> _handleDrawScript;
        private readonly ScriptData _scriptData;

        public string GuiId { get; }
        public string LayoutId => _layout.Id;

        public GuiDefinitionFromData(
            string id,
            IGuiLayoutData layout,
            IEnumerable<IGuiControlData>? controlDataList,
            string? parentControlId
        ) : base(
            new ObjectEntityDetailsModel()
        )
        {
            GuiId = id;

            _layout = layout;
            _controlDataList = controlDataList ?? new List<IGuiControlData>();
            _parentControlId = parentControlId;
            _handleUpdateScript = () => Task.CompletedTask;
            _handleDrawScript = () => Task.CompletedTask;
            _scriptData = new ScriptData(
                new Dictionary<string, object>()
            );
        }

        public override async Task Initialize()
        {
            var initializeScript = await GetClientScript(
                _layout.InitializeScript
            );
            if (initializeScript.IsNotNull())
            {
                await initializeScript.Run(
                    _scriptServices,
                    _scriptData
                );
            }

            _initialized = true;
            if (_runActivate)
            {
                await Activate();
            }

            var updateScript = await GetClientScript(
                _layout.UpdateScript
            );
            if (updateScript.IsNotNull())
            {
                _handleUpdateScript = () => updateScript.Run(
                    _scriptServices,
                    _scriptData
                );
            }

            var drawScript = await GetClientScript(
                _layout.DrawScript
            );
            if (drawScript.IsNotNull())
            {
                _handleDrawScript = () => drawScript.Run(
                    _scriptServices,
                    _scriptData
                );
            }
        }

        public override async Task Dispose()
        {
            var disposeScript = await GetClientScript(
                _layout.DisposeScript
            );
            if (disposeScript.IsNotNull())
            {
                await disposeScript.Run(
                    _scriptServices,
                    _scriptData
                );
            }

            foreach (var control in _flattenedControlList)
            {
                await _mediator.Send(
                    new DisposeOfGuiControlCommand(
                        GuiId,
                        control.Id
                    )
                );
            }

            await base.Dispose();
        }

        public override async Task Update()
        {
            await base.Update();
            await _handleUpdateScript();
        }

        public override Task Draw()
        {
            return _handleDrawScript();
        }

        public async Task Activate()
        {
            if (!_initialized)
            {
                _runActivate = true;
                return;
            }

            _flattenedControlList = await GetFlattenedControls();
            foreach (var control in _flattenedControlList)
            {
                await _mediator.Send(
                    new RegisterGuiControlCommand(
                        GuiId,
                        control
                    )
                );
            }
            await _mediator.Send(
                new SetupGuiLayoutCommand(
                    GuiId,
                    _layout,
                    _parentControlId
                )
            );

            // Setup LinkWith
            foreach (var control in _flattenedControlList)
            {
                await _mediator.Send(
                    new UpdateGuiControlCommand(
                        GuiId,
                        new GuiControlDataModel
                        {
                            ControlId = control.Id,
                            LinkWith = control.LinkWith,
                        }
                    )
                );
            }

            // Track GUI to Parent
            if (!string.IsNullOrEmpty(
                _parentControlId
            ))
            {
                _guiControlChildrenState.AddChildGuiToControl(
                    _parentControlId,
                    GuiId
                );
            }

            var activateScript = await GetClientScript(
                _layout.ActivateScript
            );
            if (activateScript.IsNotNull())
            {
                await activateScript.Run(
                    _scriptServices,
                    _scriptData
                );
            }
        }

        public async Task Hide()
        {
            foreach (var control in _layout.ControlList)
            {
                await _mediator.Send(
                    new UpdateGuiControlCommand(
                        GuiId,
                        new GuiControlDataModel
                        {
                            ControlId = control.Id,
                            IsVisible = false,
                        }
                    )
                );
            }
        }

        public async Task Show()
        {
            foreach (var control in _layout.ControlList)
            {
                await _mediator.Send(
                    new UpdateGuiControlCommand(
                        GuiId,
                        new GuiControlDataModel
                        {
                            ControlId = control.Id,
                            IsVisible = true,
                        }
                    )
                );
            }
        }

        public async Task LinkWith(
            object linkWith
        )
        {
            foreach (var control in _layout.ControlList)
            {
                await _mediator.Send(
                    new UpdateGuiControlCommand(
                        GuiId,
                        new GuiControlDataModel
                        {
                            ControlId = control.Id,
                            LinkWith = linkWith,
                        }
                    )
                );
            }
        }

        private Task<IList<IGuiLayoutControlData>> GetFlattenedControls()
        {
            return FlattenControlListInto(
                new List<IGuiLayoutControlData>(),
                _layout.ControlList
            );
        }

        private async Task<IList<IGuiLayoutControlData>> FlattenControlListInto(
            IList<IGuiLayoutControlData> list,
            IEnumerable<IGuiLayoutControlData> controlList
        )
        {
            foreach (var control in controlList)
            {
                // Take all controls and add them to the Accumulator
                list = await FlattenControlListInto(
                    list,
                    control.ControlList ?? new List<IGuiLayoutControlData>()
                );
                // Add the current Control into the Accumulator list
                var accumulatorControl = new GuiLayoutControlDataModel(
                    control
                );
                var optionsFromData = GetControlOptionsForControl(
                    accumulatorControl.Id
                );
                accumulatorControl.Options = GuiControlOptionsModel.MergeControlOptions(
                    SanitizeControlOptions(
                        accumulatorControl.Options ?? new GuiControlOptionsModel()
                    ),
                    await GetGeneratedOptions(
                        GuiId,
                        _layout,
                        accumulatorControl.Options,
                        accumulatorControl
                    ),
                    optionsFromData,
                    await GetGeneratedOptions(
                        GuiId,
                        _layout,
                        optionsFromData,
                        accumulatorControl
                    )
                );

                accumulatorControl.LinkWith = GetControlDataForControl(
                    accumulatorControl.Id
                )?.LinkWith ?? accumulatorControl.LinkWith;

                list.Add(
                    accumulatorControl
                );
            }
            return list;
        }

        private async Task<IGuiControlOptions> GetGeneratedOptions(
            string guiId,
            IGuiLayoutData layout,
            IGuiControlOptions? controlOptions,
            IGuiLayoutControlData control
        )
        {
            if (controlOptions == null)
            {
                return new GuiControlOptionsModel();
            }
            var metadata = GetMetadata(
                controlOptions
            );
            var options = new GuiControlOptionsModel();
            var text = OptionTextFromKey(
                controlOptions
            );
            if (!string.IsNullOrEmpty(text))
            {
                options["text"] = text;
            }
            var onClick = await OptionOnClickFromOnClick(
                guiId,
                layout,
                control,
                controlOptions
            );
            if (onClick != null)
            {
                options["onClick"] = onClick;
            }

            foreach (var modelOption in metadata.ModelOptions)
            {
                if (controlOptions.TryGetValue(
                    modelOption,
                    out var controlOption
                ))
                {
                    options[modelOption] = await GetGeneratedOptions(
                        guiId,
                        layout,
                        controlOption.Cast<GuiControlOptionsModel>(),
                        control
                    );
                }
            }

            return options;
        }

        private IGuiControlOptions SanitizeControlOptions(
            GuiControlOptionsModel controlOptions
        )
        {
            var metadata = GetMetadata(
                controlOptions
            );
            foreach (var modelOption in metadata.ModelOptions)
            {
                if (controlOptions.TryGetValue(
                    modelOption,
                    out var controlOption
                ))
                {
                    controlOptions[modelOption] = controlOption.Cast<GuiControlOptionsModel>();
                }
            }

            return controlOptions;
        }

        private GuiControlOptionsModel.GuiControlMetadataOptionModel GetMetadata(
            IGuiControlOptions controlOptions
        )
        {
            if (controlOptions.TryGetValue(
                GuiControlOptionsModel.GuiControlMetadataOptionModel.OPTION_NAME,
                out var metadataObject
            ))
            {
                return metadataObject.Cast<GuiControlOptionsModel.GuiControlMetadataOptionModel>();
            }

            return new GuiControlOptionsModel.GuiControlMetadataOptionModel();
        }

        private async Task<Func<Task>?> OptionOnClickFromOnClick(
            string guiId,
            IGuiLayoutData layout,
            IGuiLayoutControlData control,
            IGuiControlOptions options
        )
        {
            var onClick = options.GetValue<Func<Task>>(
                "onClick"
            );
            if (onClick.HasValue)
            {
                return onClick.Value;
            }
            var onClickScript = options.GetValue<string>(
                "onClickScript"
            );
            if (onClickScript.HasValue)
            {
                var onClickClientScript = await GetClientScript(
                    onClickScript.Value
                );
                if (onClickClientScript.IsNotNull())
                {
                    var onClickData = new ScriptData(
                        new Dictionary<string, object>
                        {
                            { "guiId", guiId },
                            { "layout", layout },
                            { "control", control },
                        }
                    );
                    return () => onClickClientScript.Run(
                        _scriptServices,
                        onClickData
                    );
                }
            }
            return null;
        }

        private IGuiControlOptions GetControlOptionsForControl(
            string controlId
        )
        {
            return GetControlDataForControl(
                controlId
            )?.Options ?? new GuiControlOptionsModel();
        }

        private IGuiControlData? GetControlDataForControl(
            string controlId
        )
        {
            return _controlDataList.FirstOrDefault(
                a => a.ControlId == controlId
            );
        }

        private string OptionTextFromKey(
            IGuiControlOptions options
        )
        {
            // Contains textKey, run through Localizer
            if (options.ContainsKey("textKey"))
            {
                return _localizer[options.GetValue<string>("textKey").Value];
            }
            // Check text
            var text = options.GetValue<string>(
                "text"
            );
            if (text.HasValue)
            {
                return text.Value;
            }
            return string.Empty;
        }

        private async Task<IClientScript?> GetClientScript(
            string scriptId
        )
        {
            if (string.IsNullOrWhiteSpace(
                scriptId
            ))
            {
                return default;
            }
            var queryResult = await _mediator.Send(
                new QueryForClientScriptById(
                    scriptId
                )
            );
            if (!queryResult.Success)
            {
                return default;
            }

            return queryResult.Result;
        }
    }
}
