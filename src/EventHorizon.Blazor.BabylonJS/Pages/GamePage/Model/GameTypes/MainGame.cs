namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Cameras;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Lights;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Meshes;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using EventHorizon.Game.Client.Engine.Input.Register;

    public class MainGame : GameBase
    {
        public override Task Dispose()
        {
            return Task.CompletedTask;
        }

        public override Task Initialize()
        {
            return Task.CompletedTask;
        }

        public override async Task Setup()
        {
            await Register(
                new WorldCamera()
            );
            await Register(
                new Block()
            );
            await Register(
                new PointLightEntity(
                    new LightSettings
                    {
                        Name = "TestingLight",
                    }
                )
            );
            await _mediator.Send(
                new RegisterInputCommand(
                    new InputOptions(
                        "w",
                        HandleKeyPressed,
                        HandleKeyReleased
                    )
                )
            );
        }

        private Task HandleKeyReleased(
            InputKeyEvent obj
        )
        {
            Console.WriteLine(obj.Key);

            return Task.CompletedTask;
        }

        private Task HandleKeyPressed(
            InputKeyEvent obj
        )
        {
            Console.WriteLine(obj.Key);

            return Task.CompletedTask;
        }

        public override Task Start()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }
    }
}
