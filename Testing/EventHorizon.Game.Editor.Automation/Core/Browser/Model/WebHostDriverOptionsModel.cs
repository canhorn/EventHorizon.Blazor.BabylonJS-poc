namespace EventHorizon.Game.Editor.Automation.Core.Browser.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Editor.Automation.Core.Browser.Api;

    public class WebHostDriverOptionsModel
        : WebHostDriverOptions
    {
        public List<string> Arguments { get; set; } = new List<string>();
        IEnumerable<string> WebHostDriverOptions.Arguments => Arguments;
    }
}
