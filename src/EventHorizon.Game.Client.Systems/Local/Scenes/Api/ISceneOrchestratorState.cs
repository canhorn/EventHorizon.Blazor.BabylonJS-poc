namespace EventHorizon.Game.Client.Systems.Local.Scenes.Api;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Systems.Local.Scenes.Model;

public interface ISceneOrchestratorState
{
    void Setup(
        string defaultSceneId,
        IDictionary<string, Func<GameSceneBase>> scenes
    );
    void Clear();
    Task StartScene(string sceneId);
    Task StartDefaultScene();
}
