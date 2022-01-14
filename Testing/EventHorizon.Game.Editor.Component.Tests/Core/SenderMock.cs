namespace EventHorizon.Game.Editor.Client.Core;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

public class SenderMock : ISender
{
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
}
