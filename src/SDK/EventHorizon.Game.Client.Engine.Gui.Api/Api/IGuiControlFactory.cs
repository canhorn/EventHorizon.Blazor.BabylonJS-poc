namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IGuiControlFactory
    {
        IGuiControl Build(
            string id, 
            IGuiControlTemplate value,
            [MaybeNull] IGuiControlOptions options,
            [MaybeNull] IGuiGridLocation gridLocation
        );
    }
}
