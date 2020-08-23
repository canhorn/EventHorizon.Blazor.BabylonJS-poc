namespace EventHorizon.Game.Client.Engine.Gui.Factory.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using BabylonJS;
    using BabylonJS.GUI;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Gui.Model.Options;

    public class BabylonJSGuiText
        : IBabylonJSGuiControl
    {
        private readonly TextBlock _control;

        public string Id { get; }
        public GuiControlType Type => GuiControlType.TEXT;
        private bool _isVisible = true;
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                // TODO: GUI Animation
                _isVisible = value;
                Control.isVisible = _isVisible;
            }
        }
        public int Layer { get; set; }
        public IGuiControlOptions Options { get; private set; }
        public string? ParentId { get; }
        public IGuiGridLocation? GridLocation { get; }

        public Control Control => _control;

        public BabylonJSGuiText(
            string id,
            IGuiControlOptions options,
            IGuiGridLocation? gridLocation
        )
        {
            Id = id;
            Options = options;
            GridLocation = gridLocation;

            _control = CreateControl(
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
                "GuiText does not support adding Controls"
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
                _control
            );

            Options = GuiControlOptionsModel.MergeControlOptions(
                Options,
                options
            );
        }

        private TextBlock CreateControl(
            string id,
            IGuiControlOptions options
        )
        {
            var textControl = new TextBlock(
                $"{id}-text"
            );
            textControl.isHitTestVisible = false;

            Update(
                options,
                textControl
            );

            return textControl;
        }

        IList<string> IGNORE_PROPERTY_LIST = new List<string>
        {
            "animation",
            "onClick",
        };

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

        private void Update(
            IGuiControlOptions options,
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
                        textControl,
                        option.Key,
                        option.Value
                    );
                }
            }
        }
    }
}
