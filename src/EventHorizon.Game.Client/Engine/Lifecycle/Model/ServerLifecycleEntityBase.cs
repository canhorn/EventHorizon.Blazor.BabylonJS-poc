namespace EventHorizon.Game.Client.Engine.Lifecycle.Model;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

public abstract class ServerLifecycleEntityBase : LifecycleEntityBase
{
    public ServerLifecycleEntityBase(long clientId, IObjectEntityDetails details)
        : base(clientId, details) { }

    public ServerLifecycleEntityBase(IObjectEntityDetails details)
        : base(details) { }
}
