namespace EventHorizon.Game.Client.Engine.Input.Api;

using System.Collections;
using System.Collections.Generic;

public interface IKeyEvent
{
    IEnumerable<string> PressedKeys { get; }
}
