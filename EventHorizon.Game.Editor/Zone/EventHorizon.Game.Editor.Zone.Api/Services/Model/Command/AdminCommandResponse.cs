namespace EventHorizon.Game.Editor.Services.Model.Command
{
    public class AdminCommandResponse
    {
        public string CommandFunction { get; set; }
        public string RawCommand { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
