namespace EventHorizon.Game.Editor.Services.Model.Command;

public class AdminCommandResponse
{
    public string CommandFunction { get; set; } = "invalid";
    public string RawCommand { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
