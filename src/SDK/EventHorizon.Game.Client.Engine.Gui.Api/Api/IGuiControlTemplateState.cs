namespace EventHorizon.Game.Client.Engine.Gui.Api
{
    using System;

    public interface IGuiControlTemplateState
    {
        void Set(IGuiControlTemplate template);
        Option<IGuiControlTemplate> Get(string id);
        bool Has(string id);
    }
}
