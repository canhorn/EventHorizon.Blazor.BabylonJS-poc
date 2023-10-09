namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using BlazorPro.BlazorSize;

using EventHorizon.Blazor.BabylonJS.Authentication.Set;
using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Core.I18n.Api;
using EventHorizon.Game.Client.Core.I18n.Model;
using EventHorizon.Game.Client.Core.I18n.Set;
using EventHorizon.Game.Client.Engine.Input.Model;
using EventHorizon.Game.Client.Engine.Input.Trigger;
using EventHorizon.Game.Client.Engine.Particle.Add;
using EventHorizon.Game.Client.Engine.Particle.Api;
using EventHorizon.Game.Client.Engine.Particle.Model;
using EventHorizon.Game.Client.Engine.Systems.Player.Model;
using EventHorizon.Game.Client.Engine.Window.Resize;
using EventHorizon.Html.Interop;
using EventHorizon.Platform.LogProvider.Api;

using global::BabylonJS;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[Authorize]
public class GamePageModel : ComponentBase, IAsyncDisposable
{
    [Inject]
    public IStartup Startup { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; } =
        null!;

    [Inject]
    public IConfiguration Configuration { get; set; } = null!;

    [Inject]
    IAccessTokenProvider TokenProvider { get; set; } = null!;

    [Inject]
    ResizeListener ResizeListener { get; set; } = null!;

    [Inject]
    public ClientDetailsEnrichmentService ClientEnrichmentService { get; set; } =
        null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            ResizeListener.OnResized += WindowResized;
            await StartGame_ByClient();
        }
    }

    public async ValueTask DisposeAsync()
    {
        await Startup.Stop();
        ResizeListener.OnResized -= WindowResized;
    }

    public async Task HandleStartGame()
    {
        await StartGame_ByClient();
    }

    public void HandleKeyDown(KeyboardEventArgs args)
    {
        Mediator.Send(
            new TriggerInputCommand(args.Key, InputTriggerType.Pressed)
        );
    }

    public void HandleKeyUp(KeyboardEventArgs args)
    {
        Mediator.Send(
            new TriggerInputCommand(args.Key, InputTriggerType.Released)
        );
    }

    public async Task StartGame_ByClient()
    {
        await LoadInTestingData();

        var state =
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
        //var accessToken = state.User.Claims.FirstOrDefault(a => a.Type == "access_token").Value;
        var accessTokenResult = await TokenProvider.RequestAccessToken();
        var accessToken = string.Empty;
        var playerId = state.User
            ?.Claims?.FirstOrDefault(a => a.Type == "sub")
            ?.Value;

        if (string.IsNullOrWhiteSpace(playerId))
        {
            throw new GameRuntimeException(
                "INVALID_PLAYER_ID",
                "PlayerId is not valid to start the game."
            );
        }

        if (accessTokenResult.TryGetToken(out var token))
        {
            accessToken = token.Value;
            await Mediator.Publish(new AccessTokenSetEvent(accessToken));

            ClientEnrichmentService.EnrichWith(
                "Client.AuthenticatedUserId",
                playerId
            );

            ClientEnrichmentService.EnrichWith("Client.PlayerId", playerId);

            ClientEnrichmentService.EnrichWith(
                "Client.DeploymentDetails.UserId",
                Configuration["DeploymentDetails:UserId"]
            );
        }
        Startup.Setup(
            new ServerGame(),
            "game-window",
            new StandardPlayerDetails(playerId, accessToken),
            "/login?returnUrl=/game",
            Configuration["Game:CoreServer"],
            Configuration["Game:AssetServer"],
            ""
        );
        await Startup.Run();
    }

    public void HandleStartGame_DirectBabylonJS()
    {
        var canvas = Canvas.Create("game-window");
        var engine = new Engine(canvas);
        var scene = new Scene(engine);
        var light0 = new PointLight("Omni", new Vector3(0, 2, 8), scene);
        var box1 = Mesh.CreateBox("b1", 1.0m, scene);
        var freeCamera = new FreeCamera(
            "FreeCamera",
            new Vector3(0, 0, 5),
            scene
        );
        freeCamera.rotation = new Vector3(0, (decimal)System.Math.PI, 0);
        scene.activeCamera = freeCamera;
        freeCamera.attachControl(canvas, true);

        engine.runRenderLoop(() => Task.Run(() => scene.render()));
        //engine.StartRenderLoop(
        //    scene
        //);
    }

    [Inject]
    public HttpClient HttpClient { get; set; } = null!;

    [Inject]
    public ILogger<GamePage> Logger { get; set; } = null!;

    private async Task LoadInTestingData()
    {
        await LoadInDefaultI18nBundle();
        // Particle
        //await LoadInParticleTemplates();
    }

    private async Task LoadInDefaultI18nBundle()
    {
        // TODO: Grab this from User Claims
        var locale = "en-us";
        II18nBundle resourceBundle;
        try
        {
            resourceBundle =
                await HttpClient.GetFromJsonAsync<I18nBundleModel>(
                    $"i18n/default.{locale}.json"
                ) ?? new I18nBundleModel();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Try to pull in i18n/default.json
                resourceBundle = await GetDefaultBundle();
            }
            else
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex,
                "Failed I18n Bundle Request for Locale: {Locale}",
                locale
            );
            throw;
        }

        if (resourceBundle != default)
        {
            await Mediator.Send(new SetI18nBundleCommand(resourceBundle));
        }
    }

    public async Task<II18nBundle> GetDefaultBundle()
    {
        try
        {
            var bundle = await HttpClient.GetFromJsonAsync<I18nBundleModel>(
                $"i18n/default.json"
            );
            return bundle ?? new I18nBundleModel();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed Default Bundle I18n Request");
            throw;
        }
    }

    private async Task LoadInParticleTemplates()
    {
        var templateList = new List<string>
        {
            "Bomb.json",
            "Flame.json",
            "SelectedIndicator.json",
        };
        foreach (var templateFileName in templateList)
        {
            var template = await GetParticleTemplate(templateFileName);
            await Mediator.Send(new AddParticleTemplateCommand(template));
        }
    }

    public async Task<ParticleTemplate> GetParticleTemplate(
        string templateFileName
    )
    {
        try
        {
            var bundle =
                await HttpClient.GetFromJsonAsync<ParticleTemplateModel>(
                    $"game-data/test-particles/{templateFileName}"
                );
            return bundle ?? new ParticleTemplateModel();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed Particle Template Request");
            throw;
        }
    }

    private void WindowResized(object? _, BrowserWindowSize __)
    {
        Mediator.Publish(new SystemWindowResizedEvent());
    }
}
