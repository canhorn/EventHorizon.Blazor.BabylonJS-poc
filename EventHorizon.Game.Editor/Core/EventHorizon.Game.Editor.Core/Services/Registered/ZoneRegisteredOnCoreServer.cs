namespace EventHorizon.Game.Editor.Core.Services.Registered;

using EventHorizon.Game.Editor.Core.Services.Model;
using EventHorizon.Observer.Model;

using MediatR;

public record ZoneRegisteredOnCoreServer(CoreZoneDetails ZoneDetails)
    : INotification;

public interface ZoneRegisteredOnCoreServerObserver
    : ArgumentObserver<ZoneRegisteredOnCoreServer> { }
