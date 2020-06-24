using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Services.Api
{
    public interface IGameService
    {
        IGame Get();
        void Set(IGame game);
    }
}
