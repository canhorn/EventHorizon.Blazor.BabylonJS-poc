namespace EventHorizon.Game.Client.Engine.Input;

using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Input.Api;
using EventHorizon.Game.Client.Engine.Input.State;
using Microsoft.Extensions.DependencyInjection;

public static class InputExtensions
{
    public static IServiceCollection AddEngineInputServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<StandardInputState>()
            .AddSingleton<IInputState>(services =>
                services.GetRequiredService<StandardInputState>()
            )
            .AddSingleton<IRegisterInput>(services =>
                services.GetRequiredService<StandardInputState>()
            )
            .AddSingleton<IUnregisterInput>(services =>
                services.GetRequiredService<StandardInputState>()
            );
    }
}
