namespace EventHorizon.Game.Editor.Client.Core;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

public class SenderMock : ISender
{
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

    public Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken = default
    )
    {
        return Task.FromResult(default(TResponse));
    }

    public Task<object?> Send(
        object request,
        CancellationToken cancellationToken = default
    )
    {
        return Task.FromResult(default(object));
    }

    public Task Send<TRequest>(
        TRequest request,
        CancellationToken cancellationToken = default
    )
        where TRequest : IRequest
    {
        throw new System.NotImplementedException();
    }
}
