namespace EventHorizon.Game.Client.Engine.Input.Api;

using System;
using System.Collections.Generic;
using System.Text;

public interface IInputState
{
    IEnumerable<InputOptions> Where(Func<InputOptions, bool> predicate);
}
