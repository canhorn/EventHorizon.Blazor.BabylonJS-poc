namespace EventHorizon.Platform.LogProvider.Model
{
    using System.Collections.Generic;
    using EventHorizon.Platform.LogProvider.Connection.Model;

    public struct PlatformLogMessage
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Args { get; set; }
    }

    public static class PlatformLogMessageExtensions
    {
        public static ClientLogMessage ToClientLogMessage(
            this PlatformLogMessage platformMessage
        )
        {
            return new ClientLogMessage
            {
                Level = platformMessage.Level,
                Message = platformMessage.Message,
                Args = platformMessage.Args,
            };
        }
    }
}
