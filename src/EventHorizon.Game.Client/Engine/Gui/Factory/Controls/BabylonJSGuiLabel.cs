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

    public class BabylonJSGuiLabel
        : IBabylonJSGuiControl
    {
        private readonly Rectangle _control;
        private readonly TextBlock _textControl;

        public string Id { get; }
        public GuiControlType Type => GuiControlType.LABEL;
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

        public BabylonJSGuiLabel(
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
                _textControl
            );

            Options = GuiControlOptionsModel.MergeControlOptions(
                Options,
                options
            );
        }

        private (Rectangle, TextBlock) CreateControl(
            string id,
            IGuiControlOptions options
        )
        {
            var labelBoxControl = new Rectangle(
                $"{id}_box"
            );
            var textControl = new TextBlock(
                $"{id}_text"
            );
            labelBoxControl.thickness = 0;
            textControl.isPointerBlocker = false;

            Update(
                options,
                labelBoxControl,
                textControl
            );

            labelBoxControl.addControl(
                textControl
            );

            return (labelBoxControl, textControl);
        }

        private static IList<string> IGNORE_PROPERTY_LIST = new List<string>
        {
            "animation",
            "textOptions"
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
                else if (option.Key == "textOptions")
                {
                    var textBlockOptions = option.Value.To<GuiControlOptionsModel>();
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
