namespace EventHorizon.Game.Editor.Core.Services.Registered;

using System;

using EventHorizon.Observer.Model;

using MediatR;

public record ZoneUnregisteredOnCoreServer(string ZoneId) : INotification;

public interface ZoneUnregisteredOnCoreServerObserver
    : ArgumentObserver<ZoneUnregisteredOnCoreServer> { }
