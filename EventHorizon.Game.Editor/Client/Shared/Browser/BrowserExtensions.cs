namespace EventHorizon.Game.Editor.Client
{
    using EventHorizon.Game.Editor.Client.Shared.Components.Window;
    using Microsoft.Extensions.DependencyInjection;

    public static class BrowserExtensions
    {
        public static IServiceCollection AddBrowserServices(
            this IServiceCollection services
        )
        {
            return services
                .AddSingleton<ViewableArea, BrowserViewableArea>()
            ;
        }
    }
}
