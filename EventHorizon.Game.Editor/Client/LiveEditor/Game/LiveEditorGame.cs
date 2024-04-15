namespace EventHorizon.Game.Editor.Client.LiveEditor.Game;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Systems.Account.Setup;
using EventHorizon.Game.Client.Systems.Connection.Core.Start;
using EventHorizon.Game.Client.Systems.Connection.Core.Stop;
using EventHorizon.Game.Client.Systems.Local.Scenes.Create;
using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
using EventHorizon.Game.Client.Systems.Local.Scenes.Start;
using EventHorizon.Game.Editor.Client.LiveEditor.Model.Cameras;
using EventHorizon.Game.Editor.Client.LiveEditor.Scenes;

public class LiveEditorGame : GameBase
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
        // Default Camera, just here while things load.
        await Register(new WorldCamera());
        // Setup Account
        await _mediator.Send(new SetupAccountCommand());
        // Start Connection to Core Server
        await _mediator.Send(new StartCoreServerConnectionCommand());

        // Setup The Scene Orchestrator
        await _mediator.Send(
            new CreateGameSceneOrchestratorCommand(
                "live-editor",
                new Dictionary<string, Func<GameSceneBase>>
                {
                    { "live-editor", () => new LiveEditorScene() },
                }
            )
        );
    }

    public override async Task Start()
    {
        await _mediator.Send(new StartDefaultSceneCommand());
    }

    public override Task Update()
    {
        return Task.CompletedTask;
    }
}
