namespace EventHorizon.Game.Editor.Client.Shared.Components.Activity;

using System.Threading.Tasks;

using EventHorizon.Activity;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

public class ActivityTrackingComponent
    : ObservableComponentBase,
        ActivityEventObserver
{
    [Inject]
    public ILogger<ActivityTrackingComponent> Logger { get; set; } = null!;

    public Task Handle(ActivityEvent args)
    {
        Logger.LogDebug(
            "Tracked Activity Event: ({Category}.{Action}.{Tag})",
            args.Category,
            args.Action,
            args.Tag
        );

        return Task.CompletedTask;
    }
}
