﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Api;

namespace EventHorizon.Game.Client.Engine.Lifecycle.Api
{
    public interface IInitializableEntity : IClientEntity
    {
        Task Initialize();
        Task PostInitialize();
    }
}
