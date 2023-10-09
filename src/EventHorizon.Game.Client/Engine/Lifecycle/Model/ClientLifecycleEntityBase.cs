namespace EventHorizon.Game.Client.Engine.Lifecycle.Model;

using System.Collections;

using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

public abstract class ClientLifecycleEntityBase : LifecycleEntityBase
{
    public ClientLifecycleEntityBase(IObjectEntityDetails details)
        : base(
            GameServiceProvider.GetService<IIndexPool>().NextIndex(),
            details
        ) { }
}
