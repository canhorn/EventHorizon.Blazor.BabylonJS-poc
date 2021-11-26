namespace EventHorizon.Game.Editor.Automation.Core.Browser;

using System;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser.Api;
using EventHorizon.Game.Editor.Automation.Core.Browser.Model;
using EventHorizon.Game.Editor.Automation.Core.Config;

using Microsoft.Edge.SeleniumTools;
using Microsoft.Extensions.Configuration;

using Xunit;

public class WebHost
    : IClassFixture<AutomationWebHostFixture>
{
}

public class AutomationWebHostFixture : IDisposable
{
    private static readonly WebHostSettings Settings =
        new WebHostSettingsModel();

    static AutomationWebHostFixture()
    {
        TestConfiguration.Configuration.Bind(
            "webHost",
            Settings
        );
    }

    public AutomationWebHostFixture()
    {
        AtataContext
            .Configure()
            .UseBaseUrl(Settings.BaseUrl)
            .UseCulture(Settings.Culture)
            .UseDriver(
                () =>
                {
                    EdgeOptions options =
                        new()
                        {
                            UseChromium = true,
                            LeaveBrowserRunning = true,
                        };

                    foreach (
                        var argument in Settings.Driver.Options.Arguments
                    )
                    {
                        options.AddArgument(argument);
                    }

                    return new EdgeDriver(
                        AppDomain.CurrentDomain.BaseDirectory,
                        options
                    );
                }
            )
            .Build();
    }

    public void Dispose()
    {
        AtataContext.Current?.CleanUp();
    }
}
