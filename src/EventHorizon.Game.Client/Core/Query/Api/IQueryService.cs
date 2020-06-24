using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventHorizon.Game.Client.Core.Query.Api
{
    public interface IQueryService
    {
        Task<R> Query<T, R>(T queryEvent) where T : IQueryEvent<R>;
    }
}
