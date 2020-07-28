namespace EventHorizon.Game.Client.Engine.Entity.Tracking.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;

    public interface IServerEntityTrackingState
    {
        IEnumerable<T> QueryByTag<T>(
            string tag
        ) where T : ServerLifecycleEntityBase;
        IEnumerable<T> QueryByNotTag<T>(
            string tag
        ) where T : ServerLifecycleEntityBase;
        void Track(
            ServerLifecycleEntityBase entity
        );
        void Untrack(
            long clientId
        );
        Task DisposeOfTracked();
        Task DisposeOfTrackedEntity(
            long clientId
        );
    }
}
