using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.Cameras;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model
{
    public class MainGame : GameBase
    {
        public WorldCamera _startupCamera;

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
