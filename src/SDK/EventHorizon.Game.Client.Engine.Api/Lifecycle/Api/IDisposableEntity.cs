namespace EventHorizon.Game.Client.Engine.Lifecycle.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    public interface IDisposableEntity : IClientEntity
    {
        Task Dispose();
    }
}
