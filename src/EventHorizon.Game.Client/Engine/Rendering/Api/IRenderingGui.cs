using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    public interface IRenderingGui : IServiceEntity
    {
        Task<T> GetCanvas<T>() where T : IGuiCanvas;
    }
}
