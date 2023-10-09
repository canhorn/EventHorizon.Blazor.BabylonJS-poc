namespace EventHorizon.Game.Client.Engine.Lifecycle.Api;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Entity.Api;

public interface IBeforeRenderableEntity : IClientEntity
{
    Task BeforeRender();
}
