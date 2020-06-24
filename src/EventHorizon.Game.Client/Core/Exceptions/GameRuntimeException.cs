using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Core.Exceptions
{
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
