namespace EventHorizon.Game.Client.Engine.Gui.Api;

public interface IGuiControlFactory
{
    IGuiControl Build(
        string id,
        IGuiControlTemplate value,
        IGuiControlOptions? options,
        IGuiGridLocation? gridLocation
    );
}
