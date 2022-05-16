namespace EventHorizon.Observer.State;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Observer.Model;

public interface ObserverState
{
    IEnumerable<ObserverBase> List { get; }

    void Register(ObserverBase observer);

    void Remove(ObserverBase observer);

    Task Trigger<TInstance, TArgs>(
        TArgs args,
        CancellationToken cancellationToken = default
    ) where TInstance : ArgumentObserver<TArgs>;

    Task Trigger(
        Type instanceType,
        Type argsType,
        object args,
        CancellationToken cancellationToken = default
    );
}
