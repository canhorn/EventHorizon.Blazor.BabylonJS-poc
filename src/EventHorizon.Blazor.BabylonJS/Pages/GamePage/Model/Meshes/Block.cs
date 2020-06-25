using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabylonJS;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Rendering.Model;

namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Meshes
{
    public class Block : LifecycleEntityBase
    {
        private readonly IRenderingScene _renderingScene; 
        private Mesh _mesh;


        public Block()
            : base()
        {
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public override Task Dispose()
        {
            _mesh.Dispose();
            return Task.CompletedTask;
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public override Task Initialize()
        {
            _mesh = Mesh.CreateBox(
                "b1",
                1.0,
                _renderingScene.GetBabylonJSScene()
            );

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
