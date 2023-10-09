namespace EventHorizon.Game.Editor.Automation.Core.Browser.Model;

using EventHorizon.Game.Editor.Automation.Core.Browser.Api;

public class WebHostDriverModel : WebHostDriver
{
    public bool IsRemote { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }
    public WebHostDriverOptionsModel Options { get; set; } =
        new WebHostDriverOptionsModel();
    WebHostDriverOptions WebHostDriver.Options => Options;
}
