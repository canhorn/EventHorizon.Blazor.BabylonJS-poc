namespace EventHorizon.Game.Editor.Automation.Core.Browser.Api
{
    public interface WebHostSettings
    {
        string BaseUrl { get; }
        string Culture { get; }
        WebHostDriver Driver { get; }
    }
}
