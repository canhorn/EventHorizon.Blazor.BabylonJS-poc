namespace EventHorizon.Game.Client.Engine.Gui.Model;

using System;

public class GuiControlType
{
    public static GuiControlType EMPTY = new GuiControlType("EMPTY");
    public static GuiControlType PANEL = new GuiControlType("Panel");
    public static GuiControlType SCROLL_VIEWER = new GuiControlType(
        "ScrollViewer"
    );
    public static GuiControlType LABEL = new GuiControlType("Label");
    public static GuiControlType BUTTON = new GuiControlType("Button");
    public static GuiControlType BAR = new GuiControlType("Bar");
    public static GuiControlType SPACER = new GuiControlType("Spacer");
    public static GuiControlType GRID = new GuiControlType("Grid");
    public static GuiControlType CONTAINER = new GuiControlType("Container");
    public static GuiControlType INPUT = new GuiControlType("Input");
    public static GuiControlType TEXT = new GuiControlType("Text");

    public string Value { get; }

    public GuiControlType(string value)
    {
        Value = value;
    }
}
