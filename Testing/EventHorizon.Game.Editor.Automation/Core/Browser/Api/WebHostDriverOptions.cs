namespace EventHorizon.Game.Editor.Automation.Core.Browser.Api
{
    using System.Collections.Generic;

    public interface WebHostDriverOptions
    {
        IEnumerable<string> Arguments { get; }
    }
}
