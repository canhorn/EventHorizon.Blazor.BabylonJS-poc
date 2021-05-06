namespace EventHorizon.Game.Editor.Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using BlazorPro.BlazorSize;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Systems;
    using EventHorizon.Game.Editor;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using EventHorizon.Game.Editor.Client.Authentication.State;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Localization.Map;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.State;
    using EventHorizon.Game.Editor.Model;
    using EventHorizon.Game.Server;
    using EventHorizon.Observer.Admin.State;
    using EventHorizon.Observer.State;
    using EventHorizon.Platform.ErrorBoundary;
    using EventHorizon.Platform.LogProvider;
    using EventHorizon.Platform.LogProvider.Model;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Configuration;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.Hierarchical;

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Setup Logging
            builder.Logging
                .AddPlatformLogger(
                    new PlatformLoggerConfiguration()
                )
            ;

            // Error Boundary
            builder.Services.AddErrorBoundary();

            // Browser Services
            builder.Services.AddBrowserServices();

            // Setup MediatR
            builder.Services.AddMediatR(
                new Type[]
                {
                    typeof(ObserverState),
                    typeof(Program),
                    typeof(EditorSharedExtensions),
                    typeof(EditorCoreExtensions),
                    typeof(EditorZoneExtensions),

                    // Platform Services
                    typeof(PlatformLoggerExtensions),

                    // Game Service Registration
                    typeof(ClientExtensions),
                    typeof(GameServerStartup),
                }
            );

            // Setup Blazor Services
            builder.Services.AddScoped<ResizeListener>();

            // Setup Game Settings
            builder.Services.AddSingleton(
                new GamePlatformServiceSettings()
                {
                    CoreServer = builder.Configuration["Game:CoreServer"],
                    AssetServer = builder.Configuration["Game:AssetServer"],
                }
            );

            // Game Client Services
            builder.Services
                .AddClientSystemsServices()
                .AddGameClient()
                .AddGameServerServices()
            ;

            // Core Services
            builder.Services
                .AddEditorCoreServices();

            // Editor Services
            builder.Services
                .AddEditorWizard();

            // Zone Services
            builder.Services
                .AddSingleton<ZoneStateCache, InMemoryZoneStateCache>()
                .AddEditorZoneServices();

            // I18n Services
            builder.Services
                .AddScoped(typeof(Localizer<>), typeof(StringBasedLocalizer<>))
                .AddLocalization(options => options.ResourcesPath = "Resources")
                .Configure<RequestLocalizationOptions>(
                    opts =>
                    {
                        var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                        };

                        opts.DefaultRequestCulture = new RequestCulture("en-US");
                        // Formatting numbers, dates, etc.
                        opts.SupportedCultures = supportedCultures;
                        // UI strings that we have localized.
                        opts.SupportedUICultures = supportedCultures;
                    })
                ;

            // Base HTTP Client
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // LocalStorage Services
            builder.Services.AddBlazoredLocalStorage();

            // Add Authentication Configuration
            builder.Services
                .AddOidcAuthentication(options =>
                {
                    builder.Configuration.Bind(
                        "Auth",
                        options.ProviderOptions
                    );
                    var authScopes = builder.Configuration["Auth:Scope"].Split(" ");
                    foreach (var authScope in authScopes)
                    {
                        options.ProviderOptions.DefaultScopes.Add(authScope);
                    }
                    //options.AuthenticationPaths.LogOutSucceededPath = "/sandbox";
                }
            );
            builder.Services
                .AddSingleton<EditorAuthenticationState, StandardEditorAuthenticationState>();

            // Observer State Manager 
            builder.Services.AddSingleton<GenericObserverState>()
                .AddSingleton<ObserverState>(services => services.GetRequiredService<GenericObserverState>())
                .AddSingleton<AdminObserverState>(services => services.GetRequiredService<GenericObserverState>());

            // Configure Logging
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );

            await builder.Build().RunAsync();
        }
    }
}
