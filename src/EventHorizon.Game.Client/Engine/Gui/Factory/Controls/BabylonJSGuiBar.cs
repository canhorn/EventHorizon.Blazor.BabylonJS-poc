namespace EventHorizon.Game.Client.Engine.Gui.Factory.Controls
{
    using System;
    using System.Collections.Generic;
    using BabylonJS.GUI;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;

    public class BabylonJSGuiBar
        : IBabylonJSGuiControl
    {
        private readonly Rectangle _control;
        private readonly TextBlock _textControl;
        private readonly Rectangle _percentageBarControl;

        public string Id { get; }
        public GuiControlType Type => GuiControlType.BAR;
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

        public BabylonJSGuiBar(
            string id,
            IGuiControlOptions options,
            IGuiGridLocation? gridLocation
        )
        {
            Id = id;
            Options = options;
            GridLocation = gridLocation;

            (_control, _textControl, _percentageBarControl) = CreateControl(
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
                "GuiBar does not support adding Controls"
            );
        }

        public void Dispose()
        {
            Control.dispose();
        }

        public void LinkWith(
            object obj
        )
        {
            if (obj is BabylonJSEngineMesh mesh)
            {
                Control.linkWithMesh(
                    mesh.Mesh
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
                _textControl,
                _percentageBarControl
            );

            Options = GuiControlOptionsModel.MergeControlOptions(
                Options,
                options
            );
        }

        private (Rectangle, TextBlock, Rectangle) CreateControl(
            string id,
            IGuiControlOptions options
        )
        {
            var backgroundControl = new Rectangle(
                $"{id}_background"
            );
            var textControl = new TextBlock(
                $"{id}_text"
            );
            var percentageBarControl = new Rectangle(
                $"{id}_percent-bar"
            );

            Update(
                options,
                backgroundControl,
                textControl,
                percentageBarControl
            );

            backgroundControl.addControl(
                percentageBarControl
            );
            backgroundControl.addControl(
                textControl
            );

            return (backgroundControl, textControl, percentageBarControl);
        }

        private static IList<string> IGNORE_PROPERTY_LIST = new List<string>
        {
            "animation",
            "barDirection",
            "percent",
            "textOptions",
            "barOptions",
        };

        private void Update(
            IGuiControlOptions options,
            Rectangle backgroundControl,
            TextBlock textControl,
            Rectangle barControl
        )
        {
            foreach (var option in options)
            {
                if (!IGNORE_PROPERTY_LIST.Contains(
                    option.Key
                ))
                {
                    SetPropertyOnControl(
                        backgroundControl,
                        option.Key,
                        option.Value
                    );
                }
                else if (option.Key == "textOptions")
                {
                    var textOptions = option.Value.To<GuiControlOptionsModel>();
                    foreach (var textOption in textOptions)
                    {
                        SetPropertyOnControl(
                            textControl,
                            textOption.Key,
                            textOption.Value
                        );
                    }
                }
                else if (option.Key == "barOptions")
                {
                    var barOptions = option.Value.To<GuiControlOptionsModel>();
                    foreach (var barOption in barOptions)
                    {
                        SetPropertyOnControl(
                            barControl,
                            barOption.Key,
                            barOption.Value
                        );
                    }
                }
            }

            // Create Bar based on Direction and Percentage
            var barDirectionOption = Options.GetValue<string>(
                "barDirection"
            );
            options.HasValueCallback<string>(
                "barDirection",
                value =>
                {
                    barDirectionOption = value.ToOption();
                }
            );
            var barDirection = "paddingLeft";
            if (barDirectionOption.HasValue)
            {
                barDirection = barDirectionOption.Value;
            }

            var percentOption = Options.GetValue<int>(
                "percent"
            );
            options.HasValueCallback<int>(
                "percent",
                value =>
                {
                    percentOption = value.ToOption();
                }
            );

            if (percentOption.HasValue)
            {
                if (barDirection == "paddingLeft")
                {
                    barControl.paddingLeft = $"{percentOption.Value}%";
                }
                else if (barDirection == "paddingRight")
                {
                    barControl.paddingRight = $"{percentOption.Value}%";
                }
            }
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
