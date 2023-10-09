namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Entity.Api;

public interface IRegisterBase<T>
    where T : IClientEntity
{
    Task Register(T entity);
    Task UnRegister(T entity);
    Task Run();
    Task CleanUp();
}
