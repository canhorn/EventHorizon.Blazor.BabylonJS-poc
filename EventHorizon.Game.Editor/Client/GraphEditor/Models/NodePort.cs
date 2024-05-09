namespace EventHorizon.Game.Editor.Client.GraphEditor.Models;

using System;
using System.Diagnostics.CodeAnalysis;

public class NodePort<T> : NodePort
{
    protected override bool CanStore(object o)
    {
        return o is T;
    }

    public override Type GetStorageType() => typeof(T);

    public override bool CanStore(Type t)
    {
        return typeof(T).IsAssignableFrom(t);
    }
}

public class NodePort
{
    public required string Id;
    public string Name = string.Empty;

    [MemberNotNullWhen(true, nameof(_value))]
    public bool HasValue { get; private set; }
    private object? _value;

    protected virtual bool CanStore(object value)
    {
        return true;
    }

    public virtual Type GetStorageType() => typeof(object);

    public virtual bool CanStore(Type t)
    {
        return true;
    }

    public void Store(object value)
    {
        if (CanStore(value))
        {
            _value = value;
            HasValue = true;
        }
    }

    [MemberNotNullWhen(true, nameof(_value))]
    public object? Fetch()
    {
        return _value;
    }

    public R? Fetch<R>()
    {
        if (!HasValue)
        {
            return default;
        }

        if (_value is R rValue)
        {
            return rValue;
        }

        return default;
    }

    public void Clear()
    {
        HasValue = false;
        _value = null;
    }
}
