namespace EventHorizon.Game.Client.Engine.Gui.Platform;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

public class GuiInitializePlatformService : IServiceEntity
{
    public int Priority => 0;

    public Task Initialize()
    {
        var templateState =
            GameServiceProvider.GetService<IGuiControlTemplateState>();

        // Create Platform Bar Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-bar",
                GuiControlType.BAR,
                new GuiControlOptionsModel
                {
                    { "text", "- / -" },
                    { "backgroundColor", "gray" },
                    { "background", "gray" },
                    { "barColor", "white" },
                }
            )
        );

        // Create Platform Button Template
        Func<Task> noOpOnClick = () => Task.CompletedTask;
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-button",
                GuiControlType.BUTTON,
                new GuiControlOptionsModel
                {
                    { "height", "50px" },
                    { "width", "50px" },
                    { "text", "---" },
                    { "fontSize", 12 },
                    { "color", "white" },
                    { "background", "black" },
                    { "alignment", 2 },
                    { "vAlignment", 2 },
                    { "onClick", noOpOnClick },
                }
            )
        );

        // Create Platform Container Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-container",
                GuiControlType.CONTAINER,
                new GuiControlOptionsModel { { "background", "gray" }, }
            )
        );

        // Create Platform Grid Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-grid",
                GuiControlType.GRID,
                new GuiControlOptionsModel
                {
                    { "column", 1 },
                    { "row", 1 },
                    { "backgroundColor", "transparent" },
                    { "background", "transparent" },
                    { "paddingBottom", 0 },
                    { "paddingTop", 0 },
                    { "paddingLeft", 0 },
                    { "paddingRight", 0 },
                }
            )
        );

        // Create Platform Input Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-input",
                GuiControlType.INPUT,
                new GuiControlOptionsModel { { "onClick", noOpOnClick }, }
            )
        );

        // Create Platform Label Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-label",
                GuiControlType.LABEL,
                new GuiControlOptionsModel
                {
                    {
                        "textOptions",
                        new GuiControlOptionsModel { { "text", "---" } }
                    },
                }
            )
        );

        // Create Platform Panel Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-panel",
                GuiControlType.PANEL,
                new GuiControlOptionsModel { { "isHitTestVisible", true }, }
            )
        );

        // Create Platform Scroll Viewer Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-scroll_viewer",
                GuiControlType.SCROLL_VIEWER,
                new GuiControlOptionsModel()
            )
        );

        // Create Platform Spacer Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-spacer",
                GuiControlType.SPACER,
                new GuiControlOptionsModel { { "padding", 5 }, }
            )
        );

        // Create Platform Text Template
        templateState.Set(
            new GuiControlTemplateModel(
                "platform-text",
                GuiControlType.TEXT,
                new GuiControlOptionsModel
                {
                    { "color", "white" },
                    { "fontSize", "1em" },
                    { "text", "---" },
                }
            )
        );

        return Task.CompletedTask;
    }

    public Task Dispose()
    {
        return Task.CompletedTask;
    }
}
