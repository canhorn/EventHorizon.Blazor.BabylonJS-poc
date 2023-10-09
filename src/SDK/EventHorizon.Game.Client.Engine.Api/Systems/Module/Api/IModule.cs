namespace EventHorizon.Game.Client.Engine.Systems.Module.Api;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;

public interface IModule : IDisposableEntity, IUpdatableEntity
{
    int Priority { get; }
    Task Initialize();
}
