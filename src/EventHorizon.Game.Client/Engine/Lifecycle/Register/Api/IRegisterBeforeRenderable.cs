using System;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Api
{
    public interface IRegisterBeforeRenderable 
        : IRegisterBase<IBeforeRenderableEntity>
    {
    }
}
