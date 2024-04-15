namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Cameras;
using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.ExampleGui;
using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.MainMenu;
using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.Zone;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Systems.Account.Setup;
using EventHorizon.Game.Client.Systems.Connection.Core.Start;
using EventHorizon.Game.Client.Systems.Connection.Core.Stop;
using EventHorizon.Game.Client.Systems.Local.Scenes.Create;
using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
using EventHorizon.Game.Client.Systems.Local.Scenes.Start;

public class ServerGame : GameBase
{
    public override async Task Dispose()
    {
        // Start Connection to Core Server
        await _mediator.Send(new StopCoreServerConnectionCommand());
    }

    public override Task Initialize()
    {
        return Task.CompletedTask;
    }

    public override async Task Setup()
    {
        // TODO: UseClientService.ts:useClientService()
        // Default Camera, just here while things load.
        await Register(new WorldCamera());
        // Setup Account
        await _mediator.Send(new SetupAccountCommand());
        // Start Connection to Core Server
        await _mediator.Send(new StartCoreServerConnectionCommand());

        // Setup The Scene Orchestrator
        await _mediator.Send(
            new CreateGameSceneOrchestratorCommand(
                "zone",
                new Dictionary<string, Func<GameSceneBase>>
                {
                    { "main-menu", () => new MainMenuScene() },
                    { "example-gui", () => new ExampleGuiScene() },
                    //{
                    //    "account-details",
                    //    () => new AccountDetailsScene()
                    //},
                    { "zone", () => new ZoneScene() },
                }
            )
        );
    }

    public override async Task Start()
    {
        Console.WriteLine("Start");
        await _mediator.Send(new StartDefaultSceneCommand());
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }
}
