namespace EventHorizon.Game.Client.Engine.Systems.Module.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Core.Api;
using EventHorizon.Game.Client.Engine.Entity.Model;
using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public abstract class ModuleEntityBase : ClientEntityBase, IModule
{
    public abstract int Priority { get; }

    protected ModuleEntityBase()
        : base(GameServiceProvider.GetService<IIndexPool>().NextIndex()) { }

    public abstract Task Initialize();

    public abstract Task Dispose();

    public abstract Task Update();
}
