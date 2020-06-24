using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Api
{
    public interface IClientScript : IServerScript
    {
        string Id { get; }
    }
}
