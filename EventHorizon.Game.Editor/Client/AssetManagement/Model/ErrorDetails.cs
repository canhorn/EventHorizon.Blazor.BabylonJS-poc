namespace EventHorizon.Game.Editor.Client.AssetManagement.Model
{
    public class ErrorDetails
    {
        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? ErrorCode { get; set; }
    }
}
