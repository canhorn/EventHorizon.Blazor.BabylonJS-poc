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

    public class BabylonJSGuiGrid
        : IBabylonJSGuiControl
    {
        private readonly Grid _control;

        public string Id { get; }
        public GuiControlType Type => GuiControlType.GRID;
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

        public BabylonJSGuiGrid(
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
            if (guiControl.GridLocation == null)
            {
                throw new GameException(
                    "gui_invalid_grid_location",
                    "Grid Location is required for GuiGrid"
                );
            }
            if (guiControl is IBabylonJSGuiControl bjsGuiControl)
            {
                _control.addControl(
                    bjsGuiControl.Control,
                    guiControl.GridLocation.Row,
                    guiControl.GridLocation.Column
                );
            }
            CorrectCellsHitTestVisible();
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

        private Grid CreateControl(
            string id,
            IGuiControlOptions options
        )
        {
            var gridControl = new Grid(
                $"{id}_grid"
            );
            gridControl.isHitTestVisible = false;

            Update(
                options,
                gridControl
            );

            options.HasValueCallback<int>(
                "column",
                value =>
                {
                    for (int i = 0; i < value; i++)
                    {
                        gridControl.addColumnDefinition(1);
                    }
                }
            );

            options.HasValueCallback<int>(
                "row",
                value =>
                {
                    for (int i = 0; i < value; i++)
                    {
                        gridControl.addRowDefinition(1);
                    }
                }
            );

            return gridControl;
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
            Grid gridControl
        )
        {
            foreach (var option in options)
            {
                Console.WriteLine("Grid Key: " + option.Key);
                if (!IGNORE_PROPERTY_LIST.Contains(
                    option.Key
                ))
                {
                    SetPropertyOnControl(
                        gridControl,
                        option.Key,
                        option.Value
                    );
                }
            }
        }

        private void CorrectCellsHitTestVisible()
        {
            // Old was using _cells, but with the Grid they are the same as the Children
            var children = _control.children;
            if (children == null)
            {
                return;
            }
            foreach (var child in children)
            {
                child.isHitTestVisible = false;
            }
        }
    }
}
