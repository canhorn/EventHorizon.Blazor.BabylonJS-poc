namespace EventHorizon.Game.Editor.Automation.Core.Browser.Api;

public interface WebHostDriver
{
    bool IsRemote { get; }
    string Url { get; }
    string Type { get; }
    WebHostDriverOptions Options { get; }
}
