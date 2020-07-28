namespace EventHorizon.Game.Client.Systems.Local.Scenes.State
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Disposed;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Api;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
    using MediatR;

    public class StandardSceneOrchestratorState
        : ISceneOrchestratorState
    {
        private readonly IDictionary<string, Func<GameSceneBase>> _scenes = new Dictionary<string, Func<GameSceneBase>>();
        private string _defaultSceneId;
        private GameSceneBase _runningScene;

        public void Clear()
        {
            if (_runningScene != null)
            {
                _runningScene.Dispose();
                _runningScene = null;
            }
            _scenes.Clear();
        }

        public void Setup(
            string defaultSceneId,
            IDictionary<string, Func<GameSceneBase>> scenes
        )
        {
            // Validate that Scenes contains the default
            if (!scenes.ContainsKey(defaultSceneId))
            {
                throw new GameRuntimeException(
                    "scenes_missing_default",
                    "The Scenes provided to Scene Orchestrator does not contain the defaultSceneId."
                );
            }
            _defaultSceneId = defaultSceneId;
            _scenes.Clear();
            foreach (var scene in scenes)
            {
                _scenes.Add(scene);
            }
        }

        public Task StartDefaultScene()
        {
            return StartScene(
                _defaultSceneId
            );
        }

        public async Task StartScene(
            string sceneId
        )
        {
            if (_runningScene != null)
            {
                var mediator = GameServiceProvider.GetService<IMediator>();
                await _runningScene.Dispose();
                await mediator.Publish(
                    new EntityDisposedEvent(
                        _runningScene
                    )
                );
                _runningScene = null;
            }
            if(_scenes.TryGetValue(sceneId, out var sceneBuilder))
            {
                var mediator = GameServiceProvider.GetService<IMediator>();
                _runningScene = sceneBuilder();
                await mediator.Publish(
                    new RegisterEntityEvent(
                        _runningScene
                    )
                );
            }
        }
    }
}
