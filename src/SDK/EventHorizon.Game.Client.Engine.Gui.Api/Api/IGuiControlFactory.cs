namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;

    public interface IGuiControlFactory
    {
        IGuiControl Build(
            string id, 
            IGuiControlTemplate value, 
            IGuiControlOptions? options, 
            IGuiGridLocation? gridLocation
        );
    }
}
