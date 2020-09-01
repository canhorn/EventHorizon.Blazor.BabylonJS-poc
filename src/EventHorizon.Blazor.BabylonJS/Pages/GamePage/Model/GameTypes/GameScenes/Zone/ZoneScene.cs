﻿namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.Zone
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.ExampleGui;
    using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Lights;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Loading.Show;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer;
    using EventHorizon.Game.Client.Systems.Local.Modules.ScreenPointer.Model;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
    using Microsoft.Extensions.Logging;

    public class ZoneScene
         : GameSceneBase
    {
        private readonly IAccountState _accountState = GameServiceProvider.GetService<IAccountState>();
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<ZoneScene>>();
        private readonly ExampleGuiLoader _exampleGui = new ExampleGuiLoader();

        public ZoneScene()
            : base("zone")
        {
        }

        public override async Task Initialize()
        {
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
            var serverAddress = _accountState.User.Zone.ServerAddress;
            _logger.LogDebug($"Started Player Connection {DateTime.UtcNow}");
            await _mediator.Send(
                new StartPlayerZoneConnectionCommand(
                    serverAddress
                )
            );
        }

        public override async Task Dispose()
        {
            await _exampleGui.Dispose();
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
    }
}
