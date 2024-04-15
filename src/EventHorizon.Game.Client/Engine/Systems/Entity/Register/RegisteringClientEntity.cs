namespace EventHorizon.Game.Client.Engine.Systems.Entity.Register;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct RegisteringClientEntity : INotification
{
    public IObjectEntityDetails EntityDetails { get; }

    public RegisteringClientEntity(IObjectEntityDetails entityDetails)
    {
        EntityDetails = entityDetails;
    }
}

public interface RegisteringClientEntityObserver : ArgumentObserver<RegisteringClientEntity> { }

public class RegisteringClientEntityHandler : INotificationHandler<RegisteringClientEntity>
{
    private readonly ObserverState _observer;

    public RegisteringClientEntityHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(RegisteringClientEntity notification, CancellationToken cancellationToken) =>
        _observer.Trigger<RegisteringClientEntityObserver, RegisteringClientEntity>(
            notification,
            cancellationToken
        );
}
