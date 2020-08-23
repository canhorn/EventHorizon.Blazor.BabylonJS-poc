namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;

    public interface IGuiLayoutDataState
    {
        Option<IGuiLayoutData> Get(
            string id
        );

        void Set(
            IGuiLayoutData layout
        );
    }
}
