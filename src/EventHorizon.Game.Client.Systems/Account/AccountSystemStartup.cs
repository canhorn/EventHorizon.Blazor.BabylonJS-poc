namespace EventHorizon.Game.Client.Systems;

using EventHorizon.Game.Client.Systems.Account.Api;
using EventHorizon.Game.Client.Systems.Account.State;

using Microsoft.Extensions.DependencyInjection;

public static class AccountSystemStartup
{
    public static IServiceCollection AddAccountSystemServices(
        this IServiceCollection services
    ) => services.AddSingleton<IAccountState, StandardAccountState>();
}
