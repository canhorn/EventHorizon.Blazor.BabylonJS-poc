namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage
{
    using System;
    using global::BabylonJS;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model;
    using EventHorizon.Game.Client;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using MediatR;
    using EventHorizon.Game.Client.Engine.Input.Trigger;
    using EventHorizon.Game.Client.Engine.Input.Model;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Authorization;
    using EventHorizon.Html.Interop;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Player.Model;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using System.Net.Http;
    using EventHorizon.Game.Client.Core.I18n.Model;
    using System.Net.Http.Json;
    using Microsoft.Extensions.Logging;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Core.I18n.Set;

    [Authorize]
    public class GamePageModel : ComponentBase
    {
        [Inject]
        public IStartup Startup { get; set; }
        [Inject]
        public IMediator Mediator { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IConfiguration Configuration { get; set; }
        [Inject]
        IAccessTokenProvider TokenProvider { get; set; }

        protected override async Task OnAfterRenderAsync(
            bool firstRender
        )
        {
            if (firstRender)
            {
                await StartGame_ByClient();
            }
        }

        public async Task HandleStartGame()
        {
            await StartGame_ByClient();
        }

        public void HandleKeyDown(
            KeyboardEventArgs args
        )
        {
            Mediator.Send(
                new TriggerInputCommand(
                    args.Key,
                    InputTriggerType.Pressed
                )
            );
        }
        public void HandleKeyUp(
            KeyboardEventArgs args
        )
        {
            Mediator.Send(
                new TriggerInputCommand(
                    args.Key,
                    InputTriggerType.Released
                )
            );
        }

        public async Task StartGame_ByClient()
        {
            await LoadInDefaultI18nBundle();

            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //var accessToken = state.User.Claims.FirstOrDefault(a => a.Type == "access_token").Value;
            var accessTokenResult = await TokenProvider.RequestAccessToken();
            var accessToken = string.Empty;

            if (accessTokenResult.TryGetToken(out var token))
            {
                accessToken = token.Value;
            }
            var playerId = state.User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
            Startup.Setup(
                new ServerGame(),
                "game-window",
                new StandardPlayerDetails(
                    playerId,
                    accessToken
                ),
                "/login?returnUrl=/game",
                Configuration["Game:CoreServer"],
                Configuration["Game:AssetServer"],
                ""
            );
            await Startup.Run();
        }

        public void HandleStartGame_DirectBabylonJS()
        {
            var canvas = Canvas.Create(
                "game-window"
            );
            var engine = new Engine(
                canvas
            );
            var scene = new Scene(
                engine
            );
            var light0 = new PointLight(
                "Omni",
                new Vector3(
                    0,
                    2,
                    8
                ),
                scene
            );
            var box1 = Mesh.CreateBox(
                "b1",
                1.0m,
                scene
            );
            var freeCamera = new FreeCamera(
                "FreeCamera",
                new Vector3(
                    0,
                    0,
                    5
                ),
                scene
            );
            freeCamera.rotation = new Vector3(
                0,
                (decimal)System.Math.PI,
                0
            );
            scene.activeCamera = freeCamera;
            freeCamera.attachControl(
                canvas,
                true
            );

            engine.runRenderLoop(() => Task.Run(() => scene.render()));
            //engine.StartRenderLoop(
            //    scene
            //);
        }

        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public ILogger<GamePageModel> Logger { get; set; }

        private async Task LoadInDefaultI18nBundle()
        {
            // TODO: Grab this from User Claims
            var locale = "en-us"; 
            II18nBundle resourceBundle;
            try
            {
                resourceBundle = await HttpClient.GetFromJsonAsync<I18nBundleModel>(
                    $"i18n/default.{locale}.json"
                );
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
                await Mediator.Send(
                    new SetI18nBundleCommand(
                        resourceBundle
                    )
                );
            }
        }

        public async Task<II18nBundle> GetDefaultBundle()
        {

            try
            {
                var bundle = await HttpClient.GetFromJsonAsync<I18nBundleModel>(
                    $"i18n/default.json"
                );
                return bundle;
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    ex,
                    "Failed Default Bundle I18n Request"
                );
                throw;
            }
        }
    }
}
