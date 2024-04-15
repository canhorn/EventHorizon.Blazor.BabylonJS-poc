namespace EventHorizon.Game.Client.Core.Event.Api;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

public interface IEventService
{
    Task Publish<T>(T publishedEvent)
        where T : IPublishedEvent;
}
