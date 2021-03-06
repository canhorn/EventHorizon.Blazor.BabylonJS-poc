namespace EventHorizon.Blazor.BabylonJS
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BlazorPro.BlazorSize;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Client;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Server;
    using EventHorizon.Observer.Admin.State;
    using EventHorizon.Observer.State;
    using EventHorizon.Platform.LogProvider;
    using EventHorizon.Platform.LogProvider.Model;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.Hierarchical;

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Setup Logging
            builder.Logging
                .AddPlatformLogger(
                    new PlatformLoggerConfiguration
                    {
                        DebugView = true,
                    }
                )
            ;

            builder.Services
                .AddTransient(
                    sp => new HttpClient
                    {
                        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                    }
                );

            builder.Services
                // I18n Services
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

            builder.Services.AddScoped<ResizeListener>();

            // Add ExternalServices
            // Observer State Manager 
            builder.Services.AddSingleton<GenericObserverState>()
                .AddSingleton<ObserverState>(services => services.GetService<GenericObserverState>())
                .AddSingleton<AdminObserverState>(services => services.GetService<GenericObserverState>());

            // Add Authentication Configuration
            builder.Services.AddOidcAuthentication(options =>
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
            });

            // Setup Solution Dependencies
            builder.Services
                .AddClientServices()
                .AddGameClient()
                .AddGameServerServices();

            builder.Services
                .AddMediatR(
                    new Type[]
                    {
                        typeof(Program),
                        typeof(ObserverState),

                        // Platform Services
                        typeof(PlatformLoggerExtensions),
                        
                        // Game Service Registration
                        typeof(ClientExtensions),
                        typeof(GameServerStartup),
                    }
                );

            // Configure Logging
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );

            await builder.Build().RunAsync();
        }
    }
}
