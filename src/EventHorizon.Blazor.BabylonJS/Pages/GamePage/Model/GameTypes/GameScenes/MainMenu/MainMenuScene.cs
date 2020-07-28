namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.MainMenu
{
    using System;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Meshes;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Start;
    using global::BabylonJS;
    using global::BabylonJS.GUI;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class MainMenuScene
        : GameSceneBase
    {
        AdvancedDynamicTexture _advancedTexture;

        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<MainMenuScene>>();

        public MainMenuScene() 
            : base("main-menu")
        {
        }

        public override async Task Initialize()
        {
            _advancedTexture = AdvancedDynamicTexture.CreateFullscreenUI("UI");

            // TODO: [I18N] : Work to be done here.
            var startGameButton = Button.CreateSimpleButton("start-game-button", "Start Game");
            startGameButton.width = "130px";
            startGameButton.height = "35px";
            startGameButton.color = "white";
            startGameButton.background = "green";
            startGameButton.onPointerClickObservable.add(
                async (Vector2WithInfo arg1, EventState state) =>
                {
                    await _mediator.Send(
                        new StartSceneCommand("zone")
                    );
                }
            );

            var accountDetailsButton = Button.CreateSimpleButton("account-details-button", "Account Details");
            accountDetailsButton.width = "130px";
            accountDetailsButton.height = "35px";
            accountDetailsButton.color = "white";
            accountDetailsButton.background = "green";
            accountDetailsButton.onPointerClickObservable.add(
                async (Vector2WithInfo arg1, EventState state) =>
                {
                    await _mediator.Send(
                        new StartSceneCommand("account-details")
                    );
                }
            );

            var grid = new Grid("main-grid");
            grid.addColumnDefinition(1);
            grid.addColumnDefinition(1);
            grid.addColumnDefinition(1);

            grid.addRowDefinition(1);
            grid.addRowDefinition(1);
            grid.addRowDefinition(1);
            grid.background = "black";
            grid.paddingBottom = "15px";
            grid.paddingTop = "15px";
            grid.paddingLeft = "15px";
            grid.paddingRight = "15px";

            _advancedTexture.addControl(grid);

            var stack = new StackPanel("stack-panel");
            grid.addControl(
                stack,
                1,
                1
            );
            stack.addControl(
                startGameButton
            );
            stack.addControl(
                new Rectangle("Spacer")
                {
                    height = "10px",
                    width = "10px",
                    thickness = 0,
                    isHitTestVisible = false,
                }
            );
            stack.addControl(
                accountDetailsButton
            );
        }

        public override Task Dispose()
        {
            _advancedTexture.dispose();

            return Task.CompletedTask;
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public override Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }
    }
}
