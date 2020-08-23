namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Dispose;
    using EventHorizon.Game.Client.Engine.Gui.Register;
    using EventHorizon.Game.Client.Engine.Gui.Setup;
    using EventHorizon.Game.Client.Engine.Gui.Update;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class GuiDefinitionFromData
        : ClientLifecycleEntityBase,
        IGuiDefinition
    {
        private readonly IMediator _mediator = GameServiceProvider.GetService<IMediator>();
        private readonly ILocalizer _localizer = GameServiceProvider.GetService<ILocalizer>();
        private readonly IGuiControlChildrenState _guiControlChildrenState = GameServiceProvider.GetService<IGuiControlChildrenState>();

        private IGuiLayoutData _layout;
        private IEnumerable<IGuiControlData> _controlDataList;
        private string? _parentControlId;
        private bool _initialized = false;
        private bool _runActivate = false;
        private IList<IGuiLayoutControlData> _flattenedControlList = new List<IGuiLayoutControlData>();
        private Func<Task> _handleUpdateScript;
        private Func<Task> _handleDrawScript;

        public string GuiId { get; }
        public string LayoutId => _layout.Id;

        public GuiDefinitionFromData(
            string id,
            IGuiLayoutData layout,
            IEnumerable<IGuiControlData>? controlDataList,
            string? parentControlId
        ) : base(
            new ObjectEntityDetailsModel
            {
                Id = -1,
            }
        )
        {
            GuiId = id;

            _layout = layout;
            _controlDataList = controlDataList ?? new List<IGuiControlData>();
            _parentControlId = parentControlId;
            _handleUpdateScript = () => Task.CompletedTask;
            _handleDrawScript = () => Task.CompletedTask;
        }

        public override async Task Initialize()
        {
            if (!string.IsNullOrEmpty(
                _layout.InitializeScript
            ))
            {
                // TODO: [Client Script] : Run Client Script
            }
            _initialized = true;
            if (_runActivate)
            {
                await Activate();
            }

            if (!string.IsNullOrEmpty(
                _layout.UpdateScript
            ))
            {
                _handleUpdateScript = () =>
                {
                    // TODO: [Client Script] : Run Client Script
                    return Task.CompletedTask;
                };
            }

            if (!string.IsNullOrEmpty(
                _layout.DrawScript
            ))
            {
                _handleDrawScript = () =>
                {
                    // TODO: [Client Script] : Run Client Script
                    return Task.CompletedTask;
                };
            }
        }

        public override async Task Dispose()
        {
            if (!string.IsNullOrEmpty(
                _layout.DisposeScript
            ))
            {
                // TODO: [Client Script] : Run Client Script
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

            _flattenedControlList = GetFlattenedControls();
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

            if (!string.IsNullOrEmpty(
                _layout.ActivateScript
            ))
            {
                // TODO: [Client Script] : Run Client Script
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

        private IList<IGuiLayoutControlData> GetFlattenedControls()
        {
            return FlattenControlListInto(
                new List<IGuiLayoutControlData>(),
                _layout.ControlList
            );
        }

        private IList<IGuiLayoutControlData> FlattenControlListInto(
            List<IGuiLayoutControlData> list,
            IEnumerable<IGuiLayoutControlData> controlList
        )
        {
            return controlList.Aggregate(
                list,
                (accumulator, control) =>
                {
                    // Take all controls and add them to the Accumulator
                    FlattenControlListInto(
                        accumulator,
                        control.ControlList ?? new List<IGuiLayoutControlData>()
                    );
                    // Add the current Control into the Accumulator list
                    var accumulatorControl = new GuiLayoutControlDataModel(
                        control
                    );
                    accumulatorControl.Options = GuiControlOptionsModel.MergeControlOptions(
                        SanitizeControlOptions(
                            accumulatorControl.Options ?? new GuiControlOptionsModel()
                        ),
                        GetGeneratedOptions(
                            GuiId,
                            _layout,
                            accumulatorControl.Options,
                            accumulatorControl
                        ),
                        GetControlOptionsForControl(
                            accumulatorControl.Id
                        )
                    );
                    accumulator.Add(
                        accumulatorControl
                    );
                    return accumulator;
                }
            );
        }

        private IGuiControlOptions GetGeneratedOptions(
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
            var onClick = OptionOnClickFromOnClick(
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
                    options[modelOption] = GetGeneratedOptions(
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

        private Func<Task>? OptionOnClickFromOnClick(
            string guiId,
            IGuiLayoutData layout,
            IGuiLayoutControlData control,
            IGuiControlOptions controlOptions
        )
        {
            var options = control.Options;
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
                return () =>
                {
                    // TODO: [Client Scripting] - Run Script
                    GameServiceProvider.GetService<ILogger<GuiDefinitionFromData>>().LogError(
                        "TODO: [Client Scripting] - Run Script Not Implemented"
                    );
                    return Task.CompletedTask;
                };
            }
            return null;
        }

        private IGuiControlOptions GetControlOptionsForControl(
            string controlId
        )
        {
            return _controlDataList.FirstOrDefault(
                a => a.ControlId == controlId
            )?.Options ?? new GuiControlOptionsModel();
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
    }
}
