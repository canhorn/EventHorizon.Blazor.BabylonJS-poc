using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace EventHorizon.Game.Client.Systems.Connection.Core.Start
{
    public class StartCoreServerConnectionCommand 
        : IRequest<bool>
    {
    }
}
