namespace EventHorizon.Game.Client.Engine.Gui.State;

using System.Collections.Generic;

using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Api;

public class StandardGuiDefinitionState
    : IGuiDefinitionState
{
    private readonly Dictionary<string, IGuiDefinition> _map = new();

    public IEnumerable<IGuiDefinition> All => _map.Values;

    public Option<IGuiDefinition> Get(string id)
    {
        if (_map.TryGetValue(id, out var definition))
        {
            return definition.ToOption();
        }
        return new();
    }

    public Option<IGuiDefinition> Remove(string id)
    {
        if (_map.TryGetValue(id, out var definition))
        {
            _map.Remove(id);
            return definition.ToOption();
        }
        return new();
    }

    public void Set(IGuiDefinition gui)
    {
        if (gui == null)
        {
            throw new GameException(
                "gui_definition_null",
                "Cannot set NULL GUI Definition into State"
            );
        }
        _map[gui.GuiId] = gui;
    }
}
