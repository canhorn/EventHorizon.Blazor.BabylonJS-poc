using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Input.Api
{
    public interface IInputState
    {
        IEnumerable<InputOptions> Where(
            Func<InputOptions, bool> predicate
        );
    }
}
