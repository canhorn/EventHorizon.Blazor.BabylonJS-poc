namespace EventHorizon.Game.Editor.Client.Zone;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.Factory.Api;
using EventHorizon.Game.Client.Core.Factory.Model;
using EventHorizon.Game.Client.Core.Timer.Api;
using EventHorizon.Game.Client.Core.Timer.Model;
using EventHorizon.Game.Editor.Client.Core;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Query;

using Microsoft.Extensions.DependencyInjection;

using Moq;

public static class MockZoneProviderStateExtensions
{
    public static IServiceCollection AddZoneStateProviderMockedServices(
        this IServiceCollection services,
        EditorComponentBaseMocks editorComponentBaseMocks,
        out ZoneProviderStateMocks zoneProviderStateMocks
    )
    {
        zoneProviderStateMocks =
            new ZoneProviderStateMocks();

        services.AddSingleton<IFactory<ITimerService>>(
            new StandardFactory<ITimerService>(
                () => new TimerService()
            )
        );
        services.AddSingleton<
            IFactory<IIntervalTimerService>
        >(
            new StandardFactory<IIntervalTimerService>(
                () => new IntervalTimerService()
            )
        );

        editorComponentBaseMocks.Mediator.Stub(
            new QueryForActiveZone(),
            new CommandResult<ZoneState>(
                new Mock<ZoneState>().Object
            )
        );

        return services;
    }
}

public class ZoneProviderStateMocks { }
