namespace EventHorizon.Game.Client.Core.Exceptions
{
    using System;

    public class GameException : Exception
    {
        public string ErrorCode { get; private set; }

        public GameException(
            string errorCode,
            string message
        ) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
