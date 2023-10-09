namespace EventHorizon.Game.Client.Core.Factory.Model;

using System;

using EventHorizon.Game.Client.Core.Factory.Api;

public class StandardFactory<T> : IFactory<T>
{
    private readonly Func<T> _builder;

    public StandardFactory(Func<T> builder)
    {
        _builder = builder;
    }

    public T Create()
    {
        return _builder();
    }
}
