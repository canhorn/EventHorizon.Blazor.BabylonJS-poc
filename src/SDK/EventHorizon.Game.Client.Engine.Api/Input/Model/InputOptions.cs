namespace EventHorizon.Game.Client.Engine.Input.Api
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    public struct InputOptions
    {
        public string Key { get; }
        public Func<InputKeyEvent, Task> Pressed { get; }
        public Func<InputKeyEvent, Task> Released { get; }

        public InputOptions(
            string key,
            Func<InputKeyEvent, Task> pressed,
            Func<InputKeyEvent, Task> released
        )
        {
            Key = key;
            Pressed = pressed;
            Released = released;
        }

        #region Generated
        public override bool Equals([MaybeNull] object obj)
        {
            return obj is InputOptions options &&
                   Key == options.Key &&
                   EqualityComparer<Func<InputKeyEvent, Task>>.Default.Equals(Pressed, options.Pressed) &&
                   EqualityComparer<Func<InputKeyEvent, Task>>.Default.Equals(Released, options.Released);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Pressed, Released);
        }

        public static bool operator ==(InputOptions left, InputOptions right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(InputOptions left, InputOptions right)
        {
            return !(left == right);
        }
        #endregion
    }
}