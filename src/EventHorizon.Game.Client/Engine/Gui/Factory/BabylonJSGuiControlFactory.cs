namespace EventHorizon.Game.Client.Engine.Gui.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Factory.Controls;
    using EventHorizon.Game.Client.Engine.Gui.Model;

    public class ControlFactoryRunner
    {
        private readonly Func<string, IGuiControlOptions, IGuiGridLocation?, IGuiControl> _runner;

        public ControlFactoryRunner(
            Func<string, IGuiControlOptions, IGuiGridLocation?, IGuiControl> runner
        )
        {
            _runner = runner;
        }

        public IGuiControl Run(
            string id,
            IGuiControlOptions options,
            IGuiGridLocation? gridLocation
        ) => _runner(
            id,
            options,
            gridLocation
        );
    }

    public class BabylonJSGuiControlFactory
        : IGuiControlFactory
    {
        public readonly IDictionary<string, ControlFactoryRunner> _guiControlBuilderMap = new Dictionary<string, ControlFactoryRunner>
        {
            {
                GuiControlType.BAR.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiBar(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.BUTTON.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiButton(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.GRID.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiGrid(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.PANEL.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiPanel(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.SPACER.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiSpacer(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.LABEL.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiLabel(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.CONTAINER.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiContainer(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.TEXT.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiText(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
            {
                GuiControlType.SCROLL_VIEWER.Value,
                new ControlFactoryRunner(
                    (id, options, gridLocation) =>
                    {
                        return new BabylonJSGuiScrollViewer(
                            id,
                            options,
                            gridLocation
                        );
                    }
                )
            },
        };

        public IGuiControl Build(
            string id,
            IGuiControlTemplate template,
            IGuiControlOptions? options,
            IGuiGridLocation? gridLocation
        )
        {
            if (string.IsNullOrEmpty(
                id
            ))
            {
                throw new GameException(
                    "gui_control_needs_id",
                    "Control has to have Id."
                );
            }

            var mergedOptions = GuiControlOptionsModel.MergeControlOptions(
                template.Options,
                options ?? new GuiControlOptionsModel()
            );
            var mergedGridLocation = template.GridLocation;
            if (gridLocation != null)
            {
                mergedGridLocation = gridLocation;
            }

            if (_guiControlBuilderMap.TryGetValue(
                template.Type.Value,
                out var controlFactory
            ))
            {
                return controlFactory.Run(
                    id,
                    mergedOptions,
                    mergedGridLocation
                );
            }

            throw new GameException(
                "gui_control_type_not_supported",
                "Not supported Gui Control Type."
            );
        }
    }
}
