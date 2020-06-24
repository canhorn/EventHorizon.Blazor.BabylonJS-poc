using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Core.Command.Api;
using EventHorizon.Game.Client.Core.Event.Api;
using EventHorizon.Game.Client.Core.Query.Api;
using EventHorizon.Game.Client.Engine.Rendering.Api;
using Microsoft.Extensions.Logging;

namespace EventHorizon.Game.Client.Engine.Systems.Scripting.Api
{
    public interface IScriptServices
    {
        ILogger Logger { get; }
        //II18nMap I18n { get; }
        IEventService EventService { get; }
        ICommandService CommandService { get; }
        IQueryService QueryService { get; }
        IEngineRenderingApi RenderingApi { get; }
    }
}
