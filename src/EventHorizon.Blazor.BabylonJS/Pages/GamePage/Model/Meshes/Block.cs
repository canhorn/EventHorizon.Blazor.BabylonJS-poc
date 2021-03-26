using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabylonJS;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Lifecycle.Model;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using EventHorizon.Game.Client.Engine.Rendering.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Meshes
{
    public class Block : LifecycleEntityBase
    {
        private readonly IRenderingScene _renderingScene; 
        private Mesh? _mesh;

        public Block()
            : base(
                  new ObjectEntityDetailsModel
                  { 
                      Name = "b1",
                      Type = "BLOCK",
                  }
            )
        {
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public override Task Dispose()
        {
            _mesh?.dispose();
            return base.Dispose();
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public override Task Initialize()
        {
            _mesh = Mesh.CreateBox(
                "b1",
                1.0m,
                _renderingScene.GetBabylonJSScene().Scene
            );

            return Task.CompletedTask;
        }

        public override Task PostInitialize()
        {
            return base.PostInitialize();
        }

        public override Task Update()
        {
            return base.Update();
        }
    }
}
