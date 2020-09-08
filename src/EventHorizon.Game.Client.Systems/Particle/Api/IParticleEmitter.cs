namespace EventHorizon.Game.Client.Systems.Particle.Api
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface IParticleEmitter
        : IInitializableEntity,
        IDisposableEntity,
        IUpdatableEntity
    {
        bool IsActive { get; }
        Task Start();
        Task Stop();
    }
}
