namespace EventHorizon.Platform.LogProvider.Connection.Model
{
    using System;
    using System.Collections.Generic;

    public class ClientLogMessage
    {
        public string Level { get; set; } = "Information";
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, object> Args { get; set; } = new Dictionary<string, object>();
    }
}
