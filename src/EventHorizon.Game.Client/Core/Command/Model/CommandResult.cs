namespace EventHorizon.Game.Client.Core.Command.Model
{
    using System;

    public class CommandResult<T>
    {
        public bool Success { get; }
        public T Result { get; }
        public string ErrorCode { get; }

        public CommandResult(T result)
        {
            Success = true;
            Result = result;
            ErrorCode = string.Empty;
        }

        public CommandResult(
            string errorCode
        )
        {
            Success = false;
            ErrorCode = errorCode;
#pragma warning disable CS8601 // Possible null reference assignment.
            Result = default;
#pragma warning restore CS8601 // Possible null reference assignment.
        }
    }
}
