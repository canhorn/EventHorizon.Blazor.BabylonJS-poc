namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;

public abstract class RegisterBase<T> : IRegisterBase<T>
    where T : IClientEntity
{
    protected IList<T> _entityList = new List<T>();
    public abstract Task Run();

    public virtual Task CleanUp()
    {
        _entityList.Clear();
        return Task.CompletedTask;
    }

    public virtual Task Register(T entity)
    {
        _entityList.Add(entity);
        return Task.CompletedTask;
    }

    public virtual Task UnRegister(T entity)
    {
        _entityList.Remove(entity);
        return Task.CompletedTask;
    }
}
