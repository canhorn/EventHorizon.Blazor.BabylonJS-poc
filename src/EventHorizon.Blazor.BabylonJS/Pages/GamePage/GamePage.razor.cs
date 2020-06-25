namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage
{
    using System;
    using System.Threading.Tasks;
    using global::BabylonJS;
    using global::BabylonJS.Html;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model;
    using EventHorizon.Game.Client;
    using Microsoft.AspNetCore.Components;
    using global::BabylonJS.Cameras;
    using Microsoft.AspNetCore.Components.Web;
    using MediatR;
    using EventHorizon.Game.Client.Engine.Input.Trigger;
    using EventHorizon.Game.Client.Engine.Input.Model;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Linq;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Authorization;

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

        private string _accessToken;

        protected override async Task OnInitializedAsync()
        {
            var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            _accessToken = state.User.Claims.FirstOrDefault(a => a.Type == "access_token").Value;
        }

        public async Task HandleStartGame()
        {
            await StartGame_ByClient();
        }

        public void HandleKeyDown(
            KeyboardEventArgs args
        )
        {
            Console.WriteLine(args.Key);
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
            Console.WriteLine(args.Key);
            Mediator.Send(
                new TriggerInputCommand(
                    args.Key,
                    InputTriggerType.Released
                )
            );
        }

        public async Task StartGame_ByClient()
        {
            Startup.Setup(
                new MainGame(),
                "game-window",
                _accessToken,
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
                1.0,
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
            freeCamera.SetRotation(
                new Vector3(
                    0,
                    Math.PI,
                    0
                )
            );
            scene.SetActiveCamera(
                freeCamera
            );
            freeCamera.AttachControl(
                canvas,
                true
            );

            engine.StartRenderLoop(
                scene
            );
        }
    }
}
