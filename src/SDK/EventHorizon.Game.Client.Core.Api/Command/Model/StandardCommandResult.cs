namespace EventHorizon.Game.Client.Core.Command.Model
{
    using System;

    public class StandardCommandResult
        : CommandResult<StandardCommandResult.EmptyResult>
    {
        public StandardCommandResult(
        ) : base(new EmptyResult())
        { }

        public StandardCommandResult(
            string errorCode
        ) : base(errorCode)
        { }

        public class EmptyResult
        {
        }

        public static implicit operator StandardCommandResult(
            string errorCode
        ) => new(
            errorCode
        );
    }
}
