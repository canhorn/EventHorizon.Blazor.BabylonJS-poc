using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace EventHorizon.Game.Client.Core.Event.Api
{
    public interface IEventService
    {
        Task Publish<T>(T publishedEvent) where T : IPublishedEvent;
    }
}
