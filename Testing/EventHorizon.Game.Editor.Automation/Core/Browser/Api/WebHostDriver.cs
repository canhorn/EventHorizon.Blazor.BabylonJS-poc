namespace EventHorizon.Game.Editor.Automation.Core.Browser.Api
{
    public interface WebHostDriver
    {
        string Type { get; }
        WebHostDriverOptions Options { get; }
    }
}
