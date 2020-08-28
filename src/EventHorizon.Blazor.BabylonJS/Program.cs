namespace EventHorizon.Blazor.BabylonJS
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using MediatR;
    using EventHorizon.Blazor.BabylonJS.Pages.Testing.DITesting.Model;
    using EventHorizon.Game.Client;
    using EventHorizon.Observer.State;
    using EventHorizon.Observer.Admin.State;
    using Microsoft.AspNetCore.Builder;
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.AspNetCore.Localization;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Client;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddTransient(
                    sp => new HttpClient
                    {
                        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                    }
                );
            builder.Services
                .AddSingleton<IDIRunHandler, DIRunHandlerImplementation>();

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

            // Add ExternalServices
            // Observer State Manager 
            builder.Services.AddSingleton<GenericObserverState>()
                .AddSingleton<ObserverState>(services => services.GetService<GenericObserverState>())
                .AddSingleton<AdminObserverState>(services => services.GetService<GenericObserverState>());

            builder.Services
                .AddClientServices()
                .AddGameClient();
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
            //builder.Services
            //    .AddOptions()
            //    .AddAuthorizationCore()
            //    .AddBlazoredOpenIdConnect(options =>
            //    {
            //        options.Authority = builder.Configuration["Auth:Authority"];

            //        options.ClientId = builder.Configuration["Auth:ClientId"];
            //        options.ResponseType = builder.Configuration["Auth:ResponseType"];

            //        options.Scopes = builder.Configuration["Auth:Scope"].Split(" ");

            //        options.SignedInCallbackUri = "/signin-oidc";

            //        //options.Scope.Add("openid");
            //        //options.Scope.Add("profile");
            //        //options.Scope.Add("api");
            //    });
            //builder.Services.AddEventHorizonIdentity(
            //    new EventHorizonIdentitySettings
            //    {
            //        Authority = Configuration["Auth:Authority"],
            //        ResponseType = Configuration["Auth:ResponseType"],
            //        Scope = Configuration["Auth:Scope"],
            //        ClientId = Configuration["Auth:ClientId"],
            //    }
            //);

            builder.Services
                .AddMediatR(
                    typeof(Program).Assembly,
                    typeof(ObserverState).Assembly,
                    typeof(ClientExtensions).Assembly
                );

            // Configure Logging
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );

            await builder.Build().RunAsync();
        }
    }
}
