namespace EventHorizon.Game.Client.Systems.Local.Modules.State.Model;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Model;
using EventHorizon.Game.Client.Systems.Local.Modules.State.Api;

public class StateModule : ModuleEntityBase, IStateModule
{
    private readonly IList<IState> _stateQueue = new List<IState>();
    private readonly IObjectEntity _entity;

    public override int Priority => 0;
    public int Size => _stateQueue.Count;

    public StateModule(IObjectEntity entity)
    {
        _entity = entity;
    }

    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override Task Dispose()
    {
        Clear();
        return Task.CompletedTask;
    }

    public override async Task Update()
    {
        if (Size == 0)
        {
            return;
        }

        // Check for Remove
        if (_stateQueue[0].Remove)
        {
            _stateQueue.RemoveAt(0);
        }

        if (Size > 0 && !_stateQueue[0].Remove)
        {
            await _stateQueue[0].Update();
        }
    }

    public void Add(IState state)
    {
        _stateQueue.Add(state);
    }

    public void AddPriority(IState state)
    {
        // Add to front
        _stateQueue.Insert(0, state);
    }

    public void Clear()
    {
        _stateQueue.Clear();
    }
}
