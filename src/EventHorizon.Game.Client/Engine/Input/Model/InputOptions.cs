namespace EventHorizon.Game.Client.Engine.Input.Api
{
    using System;
    using System.Collections.Generic;

    public struct InputOptions
    {
        public string Key { get; }
        public Action<InputKeyEvent> Pressed { get; }
        public Action<InputKeyEvent> Released { get; }

        public InputOptions(
            string key,
            Action<InputKeyEvent> pressed,
            Action<InputKeyEvent> released
        )
        {
            Key = key;
            Pressed = pressed;
            Released = released;
        }

        #region Generated
        public override bool Equals(object obj)
        {
            return obj is InputOptions options &&
                   Key == options.Key &&
                   EqualityComparer<Action<InputKeyEvent>>.Default.Equals(Pressed, options.Pressed) &&
                   EqualityComparer<Action<InputKeyEvent>>.Default.Equals(Released, options.Released);
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