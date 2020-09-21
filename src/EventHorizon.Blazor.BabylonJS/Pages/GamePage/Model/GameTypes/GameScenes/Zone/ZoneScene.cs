namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.Zone
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.ExampleGui;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using EventHorizon.Game.Client.Systems.Account.Changed;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Stop;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
    using Microsoft.Extensions.Logging;

    public class ZoneScene
         : GameSceneBase,
        AccountChangedEventObserver
    {
        private readonly IAccountState _accountState = GameServiceProvider.GetService<IAccountState>();
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<ZoneScene>>();
        private readonly ExampleGuiLoader _exampleGui = new ExampleGuiLoader();

        private string _serverAddress = string.Empty;

        public ZoneScene()
            : base("zone")
        {
        }

        public override async Task Initialize()
        {
            GamePlatfrom.RegisterObserver(
                this
            );
            // Uncomment the line below to see a Data Generated GUI displayed when this scene is loaded.
            //await _exampleGui.Initialize();
            //await Register(
            //    new PointLightEntity(
            //        new LightSettings
            //        {
            //            Name = "TestingLight",
            //        }
            //    )
            //);
            // TODO: Enable Loading 
            //await _mediator.Publish(
            //    new ShowLoadingUIEvent()
            //);
            await StartZoneConnection();
        }

        public override async Task Dispose()
        {
            await _exampleGui.Dispose();
            GamePlatfrom.UnRegisterObserver(
                this
            );
            if (!string.IsNullOrEmpty(_serverAddress))
            {
                await _mediator.Send(
                    new StopPlayerZoneConnectionCommand(
                        _serverAddress
                    )
                );
            }
            await base.Dispose();
        }

        public override Task PostInitialize()
        {
            return base.PostInitialize();
        }

        public override Task Update()
        {
            return base.Update();
        }

        public override Task Draw()
        {
            return Task.CompletedTask;
        }

        public Task Handle(
            AccountChangedEvent args
        )
        {
            return StartZoneConnection();
        }

        private async Task StartZoneConnection()
        {
            if (_accountState.User.IsNotNull()
                && !string.IsNullOrEmpty(
                    _accountState.User.Zone.ServerAddress
                )
            )
            {
                _serverAddress = _accountState.User.Zone.ServerAddress;
                _logger.LogDebug($"Started Player Connection {DateTime.UtcNow}");
                await _mediator.Send(
                    new StartPlayerZoneConnectionCommand(
                        _serverAddress
                    )
                );
            }
        }
    }
}
