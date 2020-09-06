using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.Register
{
    public struct RegisterClientEntity 
        : IRequest
    {
        public IObjectEntityDetails EntityDetails { get; }

        public RegisterClientEntity(
            IObjectEntityDetails entityDetails
        )
        {
            EntityDetails = entityDetails;
        }
    }
}
