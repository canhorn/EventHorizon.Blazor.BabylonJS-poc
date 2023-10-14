namespace EventHorizon.Game.Editor.Client.Core;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

public class MediatorMock : IMediator
{
    public Task Publish(
        object notification,
        CancellationToken cancellationToken = default
    )
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

    public Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        if (_stubMap.TryGetValue(request, out var response))
        {
            return Task.FromResult((TResponse)response);
        }
        return Task.FromResult(default(TResponse));
    }

    public Task<object?> Send(
        object request,
        CancellationToken cancellationToken = default
    )
    {
        _stubMap.TryGetValue(request, out var response);
        return Task.FromResult(response);
    }

    private IDictionary<object, object> _stubMap =
        new Dictionary<object, object>();

    public void Stub(object request, object response)
    {
        _stubMap.Add(request, response);
    }

    public Task Send<TRequest>(
        TRequest request,
        CancellationToken cancellationToken = default
    )
        where TRequest : IRequest
    {
        throw new System.NotImplementedException();
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(
        IStreamRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        throw new System.NotImplementedException();
    }

    public IAsyncEnumerable<object?> CreateStream(
        object request,
        CancellationToken cancellationToken = default
    )
    {
        throw new System.NotImplementedException();
    }
}
