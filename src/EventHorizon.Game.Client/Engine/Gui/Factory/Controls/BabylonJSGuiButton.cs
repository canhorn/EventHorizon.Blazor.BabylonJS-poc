namespace EventHorizon.Game.Client.Engine.Gui.Factory.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BabylonJS;
    using BabylonJS.GUI;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Gui.Model.Options;

    public class BabylonJSGuiButton
        : IBabylonJSGuiControl
    {
        private readonly Button _control;
        private readonly TextBlock _textControl;

        public string Id { get; }
        public GuiControlType Type => GuiControlType.BUTTON;
        private bool _isVisiable = true;
        public bool IsVisible
        {
            get
            {
                return _isVisiable;
            }
            set
            {
                _isVisiable = value;
                Control.isVisible = _isVisiable;
            }
        }
        public int Layer { get; set; }
        public IGuiControlOptions Options { get; private set; }
        public string? ParentId { get; }
        public IGuiGridLocation? GridLocation { get; }

        public Control Control => _control;

        public BabylonJSGuiButton(
            string id,
            IGuiControlOptions options,
            IGuiGridLocation? gridLocation
        )
        {
            Id = id;
            Options = options;
            GridLocation = gridLocation;

            (_control, _textControl) = CreateControl(
                id,
                options
            );
        }

        public void AddControl(
            IGuiControl guiControl
        )
        {
            throw new GameException(
                "gui_add_control_not_supported",
                "GuiButton does not support adding Controls"
            );
        }

        public void Dispose()
        {
            if (!string.IsNullOrEmpty(
                _onPointerEnterObserverHandler
            ))
            {
                _control.onPointerEnterObservable.add_Remove(
                    _onPointerEnterObserverHandler
                );
            }
            if (!string.IsNullOrEmpty(
                _onPointerOutObserverHandler
            ))
            {
                _control.onPointerOutObservable.add_Remove(
                    _onPointerOutObserverHandler
                );
            }
            if (!string.IsNullOrEmpty(
                _onClickObserverHandler
            ))
            {
                _control.onPointerClickObservable.add_Remove(
                    _onClickObserverHandler
                );
            }
            Control.dispose();
        }

        public void LinkWith(
            object obj
        )
        {
            if (obj is AbstractMesh mesh)
            {
                Control.linkWithMesh(
                    mesh
                );
            }
        }

        public void Update(
            IGuiControlOptions options
        )
        {
            Update(
                options,
                _control,
                _textControl
            );

            Options = GuiControlOptionsModel.MergeControlOptions(
                Options,
                options
            );
        }

        private string _onClickObserverHandler = string.Empty;
        private string _onPointerEnterObserverHandler = string.Empty;
        private string _onPointerOutObserverHandler = string.Empty;

        private (Button, TextBlock) CreateControl(
            string id,
            IGuiControlOptions options
        )
        {
            //var buttonControl = new Rectangle(
            //    $"{id}_button"
            //);
            //var textControl = new TextBlock(
            //    $"{id}_text-block"
            //);
            var buttonControl = Button.CreateSimpleButton(
                $"{id}_button",
                ""
            );
            var textControl = buttonControl.textBlock;
            buttonControl.thickness = 0;
            // TextWrapping.WordWrap = 1
            textControl.textWrapping = 1;
            textControl.isPointerBlocker = false;

            Update(
                options,
                buttonControl,
                textControl
            );

            buttonControl.addControl(
                textControl
            );

            // Hover Observer Setup
            _onPointerEnterObserverHandler = buttonControl.onPointerEnterObservable.add(
                (_, __) =>
                {
                    if (_control.isEnabled)
                    {
                        options.HasValueCallback<string>(
                            "hoverColor",
                            value =>
                            {
                                buttonControl.background = value;
                            }
                        );
                    }
                    else
                    {
                        options.HasValueCallback<string>(
                            "disabledHoverColor",
                            value =>
                            {
                                buttonControl.background = value;
                            }
                        );
                    }
                    return Task.CompletedTask;
                }
            );
            _onPointerOutObserverHandler = buttonControl.onPointerEnterObservable.add(
                (_, __) =>
                {
                    if (_control.isEnabled)
                    {
                        options.HasValueCallback<string>(
                            "background",
                            value =>
                            {
                                buttonControl.background = value;
                            }
                        );
                    }
                    else
                    {
                        options.HasValueCallback<string>(
                            "disabledColor",
                            value =>
                            {
                                buttonControl.background = value;
                            }
                        );
                    }
                    return Task.CompletedTask;
                }
            );

            return (buttonControl, textControl);
        }

        IList<string> IGNORE_PROPERTY_LIST = new List<string>
        {
            "animation",
            "textBlockOptions",
            "onClick",
        };

        private void Update(
            IGuiControlOptions options,
            Rectangle buttonControl,
            TextBlock textControl
        )
        {
            foreach (var option in options)
            {
                if (!IGNORE_PROPERTY_LIST.Contains(
                    option.Key
                ))
                {
                    SetPropertyOnControl(
                        buttonControl,
                        option.Key,
                        option.Value
                    );
                }
                else if (option.Key == "textBlockOptions")
                {
                    var textBlockOptions = option.Value.Cast<GuiControlOptionsModel>();
                    foreach (var textBlockOption in textBlockOptions)
                    {
                        SetPropertyOnControl(
                            textControl,
                            textBlockOption.Key,
                            textBlockOption.Value
                        );
                    }
                }
            }

            // Enable/Disable Button and Cursor Setters
            if (buttonControl.isEnabled)
            {
                options.HasValueCallback<string>(
                    "background",
                    value =>
                    {
                        buttonControl.background = value;
                    }
                );
                options.HasValueCallback<string>(
                    "hoverCursor",
                    value =>
                    {
                        buttonControl.hoverCursor = value;
                    }
                );
            }
            else
            {
                options.HasValueCallback<string>(
                    "disabledColor",
                    value =>
                    {
                        buttonControl.background = value;
                    }
                );
                options.HasValueCallback<string>(
                    "disabledHoverCursor",
                    value =>
                    {
                        buttonControl.hoverCursor = value;
                    }
                );
            }

            // OnClick Setup
            options.HasValueCallback<Func<Task>>(
                "onClick",
                value =>
                {
                    if (!string.IsNullOrEmpty(
                        _onClickObserverHandler
                    ))
                    {
                        buttonControl.onPointerEnterObservable.add_Remove(
                            _onClickObserverHandler
                        );
                    }
                    _onClickObserverHandler = buttonControl.onPointerClickObservable.add(
                        (_, __) => value()
                    );
                }
            );
        }

        private void SetPropertyOnControl(
            Control control,
            string property,
            object value
        )
        {
            EventHorizonBlazorInterop.Set(
                control.___guid,
                property,
                value
            );
        }
    }
}
