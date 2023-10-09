namespace EventHorizon.Game.Client.Engine.Core.Model;

using EventHorizon.Game.Client.Engine.Core.Api;

public class IndexPoolBase : IIndexPool
{
    private long _index;

    public long NextIndex()
    {
        return _index++;
    }
}
