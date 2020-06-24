using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Core.Api
{
    public interface IIndexPool
    {
        long NextIndex();
    }
}
