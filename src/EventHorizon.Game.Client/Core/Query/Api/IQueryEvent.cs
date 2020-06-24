using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace EventHorizon.Game.Client.Core.Query.Api
{
    public interface IQueryEvent<T> : IRequest<T>
    {
    }
}
