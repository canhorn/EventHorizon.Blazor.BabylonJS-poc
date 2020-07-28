using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BabylonJS;
using EventHorizon.Game.Client.Engine.Rendering.Api;

namespace EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer
{
    public class ScreenPointerModule
    {
        private readonly IRenderingScene _renderingScene;

        public ScreenPointerModule()
        {
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
            _renderingScene.GetBabylonJSScene().Scene.onPointerObservable.add(HandlePointerObservable);
        }

        private Task HandlePointerObservable(
            PointerInfo eventData, 
            EventState eventState
        )
        {
            // TODO: Figure out what '2' is
            var mesh = eventData.pickInfo.pickedMesh;
            var pickPoint = eventData.pickInfo.pickedPoint;
            var type = eventData.type;
            if (eventData.type == 2)
            {
                var name = mesh.name;
                var guid = mesh.___guid;
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
