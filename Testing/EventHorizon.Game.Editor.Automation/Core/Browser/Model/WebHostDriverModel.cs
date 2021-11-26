namespace EventHorizon.Game.Editor.Automation.Core.Browser.Model;

using EventHorizon.Game.Editor.Automation.Core.Browser.Api;

public class WebHostDriverModel
    : WebHostDriver
{
    public string Type { get; set; }
    public WebHostDriverOptionsModel Options { get; set; } = new WebHostDriverOptionsModel();
    WebHostDriverOptions WebHostDriver.Options => Options;
}
