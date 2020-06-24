using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventHorizon.Game.Client.Engine.Rendering.Api
{
    public interface IBeforeRendering
    {
        string Register(Func<Task> action);
        Task UnRegister(string handle);
    }
}
