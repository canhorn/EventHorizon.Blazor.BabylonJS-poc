namespace EventHorizon.Game.Client.Systems.Local.Scenes.Create;

using System;
using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
using MediatR;

public class CreateGameSceneOrchestratorCommand : IRequest
{
    public string DefaultSceneId { get; }
    public IDictionary<string, Func<GameSceneBase>> Scenes { get; }

    public CreateGameSceneOrchestratorCommand(
        string defaultSceneId,
        IDictionary<string, Func<GameSceneBase>> scenes
    )
    {
        DefaultSceneId = defaultSceneId;
        Scenes = scenes;
    }
}
