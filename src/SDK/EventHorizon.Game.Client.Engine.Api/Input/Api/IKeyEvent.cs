using System.Collections;
using System.Collections.Generic;

namespace EventHorizon.Game.Client.Engine.Input.Api
{
    public interface IKeyEvent
    {
        IEnumerable<string> PressedKeys { get; }
    }
}