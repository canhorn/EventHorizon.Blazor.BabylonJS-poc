namespace EventHorizon.Game.Editor.Automation.Core.Browser.Model
{
    using EventHorizon.Game.Editor.Automation.Core.Browser.Api;

    public class WebHostSettingsModel
        : WebHostSettings
    {
        public string BaseUrl { get; set; }
        public string Culture { get; set; }
        public WebHostDriverModel Driver { get; set; } = new WebHostDriverModel();
        WebHostDriver WebHostSettings.Driver => Driver;
    }
}
