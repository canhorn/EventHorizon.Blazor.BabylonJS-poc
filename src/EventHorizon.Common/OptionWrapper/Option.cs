using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public static class OptionExtensions
{
    public static Option<T> ToOption<T>(this T? value) => new(value);
}

[Serializable]
public readonly struct Option<T>(T? value) : IEquatable<Option<T>>
{
    private readonly bool _hasValue = value != null;
    private readonly T _value = value!;

    [MemberNotNullWhen(true, nameof(Value))]
    public readonly bool HasValue
    {
        get { return _hasValue; }
    }

    [MemberNotNullWhen(true, nameof(Value))]
    public readonly T? Value
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

    public static implicit operator Option<T>(T? result) => new(result);

    public static implicit operator bool(Option<T> result) => result.HasValue;

    #region Generated
    public override bool Equals(object? obj)
    {
        return obj is Option<T> option && Equals(option);
    }

    public bool Equals(Option<T> other)
    {
        return _hasValue == other._hasValue
            && EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_hasValue, _value);
    }

    public static bool operator ==(Option<T> left, Option<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Option<T> left, Option<T> right)
    {
        return !(left == right);
    }

    public Option<T> ToOption() => new(Value);

    public bool ToBoolean() => HasValue;

    #endregion
}
