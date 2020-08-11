namespace EventHorizon.Game.Client.Core.Exceptions
{
    using System;

    public class GameRuntimeException : Exception
    {
        public string ErrorCode { get; private set; }

        public GameRuntimeException(
            string errorCode,
            string message
        ) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
