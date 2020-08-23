namespace EventHorizon.Game.Client.Engine.Gui.Model
{
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class GuiGridLocationModel
        : IGuiGridLocation
    {
        public GuiGridLocationModel() { }
        public GuiGridLocationModel(
            IGuiGridLocation gridLocation
        )
        {
            Column = gridLocation.Column;
            Row = gridLocation.Row;
        }

        public int Column { get; set; }
        public int Row { get; set; }
    }
}