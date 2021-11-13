namespace EventHorizon.Game.Editor.Automation.Core.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class WaitForMessageException
        : Exception
    {
        public WaitForMessageException() { }
        public WaitForMessageException(
            string message
        ) : base(message) { }
        public WaitForMessageException(
            string message,
            Exception inner
        ) : base(message, inner) { }
        protected WaitForMessageException(
          SerializationInfo info,
          StreamingContext context
        ) : base(info, context) { }
    }
}
