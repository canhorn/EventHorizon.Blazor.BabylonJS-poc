namespace EventHorizon.Game.Editor.Client;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorPro.BlazorSize;
using EventHorizon.Activity;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Core.Builder.Api;
using EventHorizon.Game.Client.Systems;
using EventHorizon.Game.Editor;
using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.State;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Localization.Map;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Builders;
using EventHorizon.Game.Editor.Client.Zone.Model;
using EventHorizon.Game.Editor.Client.Zone.State;
using EventHorizon.Game.Editor.Model;
using EventHorizon.Game.Server;
using EventHorizon.Game.Server.Asset;
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
using Microsoft.FluentUI.AspNetCore.Components;

public class Program
{
    public static async Task Main(string[] args)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.Hierarchical;

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddFluentUIComponents();

        // Setup Logging
        builder.Logging.AddPlatformLogger(new PlatformLoggerConfiguration());

        // Activity Services
        builder.Services.AddActivityServices();

        // Error Boundary
        builder.Services.AddErrorBoundary();

        // Browser Services
        builder.Services.AddBrowserServices();

        // Caching Services
        builder.Services.AddEasyCaching(option =>
        {
            option.UseInMemory(builder.Configuration, "DefaultInMemory", "EasyCaching:InMemory");
        });

        // Setup Blazor Services
        builder.Services.AddScoped<ResizeListener>();

        // Setup Game Settings
        builder.Services.AddSingleton(
            new GamePlatformServiceSettings()
            {
                CoreServer = builder.Configuration["Game:CoreServer"] ?? string.Empty,
                AssetServer = builder.Configuration["Game:AssetServer"] ?? string.Empty,
            }
        );

        // Game Client Services
        builder.Services.AddClientSystemsServices().AddGameClient().AddGameServerServices();

        // Artifact Management Services
        builder.Services.AddArtifactManagementServices().AddZoneArtifactManagementServices();

        // Asset Management Services
        builder.Services.AddAssetManagementServices();

        // Core Services
        builder.Services.AddEditorCoreServices();

        // Asset Server Services
        builder.Services.AddAssetServerAdminServices();

        // Editor Services
        builder.Services.AddEditorWizard();

        // Zone Services
        builder
            .Services.AddSingleton<ZoneStateCache, InMemoryZoneStateCache>()
            .AddSingleton<IBuilder<ZoneStateModel, ZoneState>, ZoneStateModelFromZoneStateBuilder>()
            .AddEditorZoneServices();

        // I18n Services
        builder
            .Services.AddScoped(typeof(Localizer<>), typeof(StringBasedLocalizer<>))
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), };

                opts.DefaultRequestCulture = new RequestCulture("en-US");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });

        // Base HTTP Client
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        // LocalStorage Services
        builder.Services.AddBlazoredLocalStorage();

        // Add Authentication Configuration
        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("Auth", options.ProviderOptions);
            var authScopes = (builder.Configuration["Auth:Scope"] ?? string.Empty).Split(" ");
            foreach (var authScope in authScopes)
            {
                options.ProviderOptions.DefaultScopes.Add(authScope);
            }
        });
        builder.Services.AddSingleton<
            EditorAuthenticationState,
            StandardEditorAuthenticationState
        >();

        // Observer State Manager
        builder
            .Services.AddSingleton<GenericObserverState>()
            .AddSingleton<ObserverState>(services =>
                services.GetRequiredService<GenericObserverState>()
            )
            .AddSingleton<AdminObserverState>(services =>
                services.GetRequiredService<GenericObserverState>()
            );

        // Setup MediatR
        builder.Services.AddMediatR(
            (configuration) =>
            {
                configuration.Lifetime = ServiceLifetime.Singleton;
                configuration.RegisterServicesFromAssemblies(
                    new[]
                    {
                        typeof(ObserverState).Assembly,
                        typeof(Program).Assembly,
                        typeof(EditorSharedExtensions).Assembly,
                        typeof(ActivityStartupExtensions).Assembly,
                        // Servers
                        typeof(AssetServerStartupExtensions).Assembly,
                        typeof(EditorCoreExtensions).Assembly,
                        typeof(EditorZoneExtensions).Assembly,
                        // Platform Services
                        typeof(PlatformLoggerExtensions).Assembly,
                        // Game Service Registration
                        typeof(ClientExtensions).Assembly,
                        typeof(GameServerStartup).Assembly,
                    }
                );
            }
        );

        // Configure Logging
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

        await builder.Build().RunAsync();
    }
}
