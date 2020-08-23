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

    public class BabylonJSGuiScrollViewer
        : IBabylonJSGuiControl
    {
        private readonly ScrollViewer _control;

        public string Id { get; }
        public GuiControlType Type => GuiControlType.SCROLL_VIEWER;
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

        public BabylonJSGuiScrollViewer(
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
            if (guiControl is IBabylonJSGuiControl bjsGuiControl)
            {
                _control.addControl(
                    bjsGuiControl.Control
                );
            }
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

        private ScrollViewer CreateControl(
            string id,
            IGuiControlOptions options
        )
        {
            var scrollViewerControl = new ScrollViewer(
                $"{id}-scroll_viewer"
            );
            scrollViewerControl.thickness = 0;
            scrollViewerControl.isHitTestVisible = false;

            Update(
                options,
                scrollViewerControl
            );

            return scrollViewerControl;
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
            ScrollViewer scrollViewerControl
        )
        {
            foreach (var option in options)
            {
                if (!IGNORE_PROPERTY_LIST.Contains(
                    option.Key
                ))
                {
                    SetPropertyOnControl(
                        scrollViewerControl,
                        option.Key,
                        option.Value
                    );
                }
            }
        }
    }
}
