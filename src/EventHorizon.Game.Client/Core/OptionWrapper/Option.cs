using System;

public struct OptionFactory
{
    public static Option<T> Build<T>(T value)
    {
        return new Option<T>(value);
    }
}

public static class OptionExtensions
{
    public static Option<T> ToOption<T>(
        this T value
    ) => OptionFactory.Build(value);
}

[Serializable]
public struct Option<T>
{
    private readonly bool _hasValue;
    private readonly T _value;

    public Option(T value)
    {
        _hasValue = value != null;
        _value = value;
    }

    public bool HasValue
    {
        get
        {
            return _hasValue;
        }
    }

    public T Value
    {
        get
        {
            if (!_hasValue)
            {
                throw new InvalidOperationException("Value is not present.");
            }
            return _value;
        }
    }
}
