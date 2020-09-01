namespace EventHorizon.Game.Client.Engine.Lifecycle.Api
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Api;

    public interface IInitializableEntity 
        : IClientEntity
    {
        Task Initialize();
        Task PostInitialize();
    }
}
