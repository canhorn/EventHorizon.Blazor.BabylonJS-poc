namespace EventHorizon.Zone.System.Server.Scripts.Model
{
    using global::System.Collections.Generic;

    public class ServerScriptsErrorDetailsResponse
    {
        public bool HasErrors { get; set; }
        public string ErrorCode { get; set; } = string.Empty;
        public List<GeneratedServerScriptErrorDetailsModel> ScriptErrorDetailsList { get; set; } = new();
    }
}
