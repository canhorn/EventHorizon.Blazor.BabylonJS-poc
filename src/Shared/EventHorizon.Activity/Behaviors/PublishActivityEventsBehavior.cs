namespace EventHorizon.Activity.Behaviors;

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

public class PublishActivityEventsBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public PublishActivityEventsBehavior(
        IMediator mediator,
        ILogger<PublishActivityEventsBehavior<TRequest, TResponse>> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (request is not TrackActivity)
        {
            return await next();
        }

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var tag = request.GetType().Name;
        await IgnoreException(
            new ActivityEvent(
                StandardActivityCategoryTypes.DEFAULT,
                StandardActivityActionTypes.SENT,
                tag
            ),
            cancellationToken
        );

        var result = await next();

        await IgnoreException(
            new ActivityEvent(
                StandardActivityCategoryTypes.DEFAULT,
                StandardActivityActionTypes.TRIGGERED,
                tag
            ),
            cancellationToken
        );

        // TODO: Remove this or make it a Feature?
        _logger.LogWarning(
            "Took {ElapsedMilliseconds}ms ({ElapsedTicks} ticks) to trigger",
            stopwatch.ElapsedMilliseconds,
            stopwatch.ElapsedTicks
        );

        return result;
    }

    private async Task IgnoreException(ActivityEvent activity, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Publish(activity, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to Publish Activity Event: {@ActivityEvent}", activity);
        }
    }
}
