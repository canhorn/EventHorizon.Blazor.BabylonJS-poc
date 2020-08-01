namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.Zone
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Lights;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Loading.Show;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
    using Microsoft.Extensions.Logging;

    public class ZoneScene
         : GameSceneBase
    {
        private readonly IAccountState _accountState = GameServiceProvider.GetService<IAccountState>();
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<ZoneScene>>();

        public ZoneScene()
            : base("zone")
        {
        }

        public override async Task Initialize()
        {
            await Register(
                new PointLightEntity(
                    new LightSettings
                    {
                        Name = "TestingLight",
                    }
                )
            );
            // TODO: Enable Loading 
            //await _mediator.Publish(
            //    new ShowLoadingUIEvent()
            //);
            var serverAddress = _accountState.User.Zone.ServerAddress;
            _logger.LogDebug($"Started Player Connection {DateTime.UtcNow}");
            await _mediator.Send(
                new StartPlayerZoneConnectionCommand(
                    serverAddress
                )
            );

            // TODO: Testing
            new BabylonJSScreenPointerModule();
        }

        public override Task PostInitialize()
        {
            return Task.CompletedTask;
        }

        public override Task Dispose()
        {
            return Task.CompletedTask;
        }

        public override Task Update()
        {
            return Task.CompletedTask;
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }
    }
}
