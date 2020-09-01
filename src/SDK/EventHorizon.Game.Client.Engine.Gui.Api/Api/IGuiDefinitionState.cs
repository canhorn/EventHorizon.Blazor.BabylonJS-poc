namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;

    public interface IGuiDefinitionState
    {
        Option<IGuiDefinition> Get(
            string id
        );

        void Set(
            IGuiDefinition gui
        );

        Option<IGuiDefinition> Remove(
            string id
        );
    }
}
