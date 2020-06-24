using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

namespace EventHorizon.Game.Client.Engine.Canvas.Api
{
    public interface ICanvas : IServiceEntity
    {
        T GetDrawingCanvas<T>() where T : class;
    }
}
