namespace EventHorizon.Game.Editor.Client.Core;

using System.Threading;
using System.Threading.Tasks;
using MediatR;

public class PublisherMock : IPublisher
{
    public Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task Publish<TNotification>(
        TNotification notification,
        CancellationToken cancellationToken = default
    )
        where TNotification : INotification
    {
        return Task.CompletedTask;
    }
}
