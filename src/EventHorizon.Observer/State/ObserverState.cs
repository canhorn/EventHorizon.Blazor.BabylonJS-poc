using EventHorizon.Observer.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EventHorizon.Observer.State
{
    public interface ObserverState
    {
        IEnumerable<ObserverBase> List { get; }
        void Register(
            ObserverBase observer
        );
        void Remove(
            ObserverBase observer
        );
        Task Trigger<TInstance, TArgs>(
            TArgs args,
            CancellationToken cancellationToken = default
        ) where TInstance : ArgumentObserver<TArgs>;
    }
}
