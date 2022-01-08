namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System.Collections.Generic;

public interface IGuiDefinitionState
{
    IEnumerable<IGuiDefinition> All { get; }

    Option<IGuiDefinition> Get(string id);

    void Set(IGuiDefinition gui);

    Option<IGuiDefinition> Remove(string id);
}
