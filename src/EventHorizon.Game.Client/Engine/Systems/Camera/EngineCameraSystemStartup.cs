namespace EventHorizon.Game.Client.Engine.Systems.Camera;

using System;

using EventHorizon.Game.Client.Engine.Systems.Camera.Api;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;

using Microsoft.Extensions.DependencyInjection;

public static class EngineCameraSystemStartup
{
    public static IServiceCollection AddEngineCameraSystemServices(
        this IServiceCollection services
    ) => services.AddSingleton<ICameraState, StandardCameraState>();
}
