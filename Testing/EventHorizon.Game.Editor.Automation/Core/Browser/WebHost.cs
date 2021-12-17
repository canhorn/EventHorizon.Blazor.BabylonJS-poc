namespace EventHorizon.Game.Editor.Automation.Core.Browser;

using System;
using System.Linq;

using Atata;

using EventHorizon.Game.Editor.Automation.Core.Browser.Api;
using EventHorizon.Game.Editor.Automation.Core.Browser.Model;
using EventHorizon.Game.Editor.Automation.Core.Config;

using Microsoft.Edge.SeleniumTools;
using Microsoft.Extensions.Configuration;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

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
                    if (Settings.Driver.IsRemote)
                    {
                        return new RemoteWebDriver(
                            new Uri(
                                "http://localhost:4445"
                            ),
                            GetDriverOptions()
                        );
                    }
                    else if (Settings.Driver.Type == "edge")
                    {
                        return new EdgeDriver(
                            AppDomain.CurrentDomain.BaseDirectory,
                            BuildEdgeOptions()
                        );
                    }
                    else if (Settings.Driver.Type == "chrome")
                    {
                        return new ChromeDriver(
                            AppDomain.CurrentDomain.BaseDirectory,
                            BuildChromeOptions()
                        );
                    }
                    else if (Settings.Driver.Type == "firefox")
                    {
                        return new FirefoxDriver(
                            AppDomain.CurrentDomain.BaseDirectory,
                            BuildFirefoxOptions()
                        );
                    }

                    throw new ArgumentException("Driver Type was not Valid");
                }
            )
            .Build();
    }

    public void Dispose()
    {
        AtataContext.Current?.CleanUp();
    }

    private static DriverOptions GetDriverOptions()
    {
        var driverOptions = Settings.Driver.Type switch
        {
            "edge" => BuildEdgeOptions(),
            "chrome" => BuildChromeOptions(),
            _ => default(DriverOptions),
        };

        return driverOptions;
    }

    private static EdgeOptions BuildEdgeOptions()
    {
        var options = new EdgeOptions
        {
            UseChromium = true,
        };
        options.AddArguments(
            Settings.Driver.Options.Arguments
                .Where(a => !string.IsNullOrWhiteSpace(a))
        );

        return options;
    }

    private static ChromeOptions BuildChromeOptions()
    {
        var options = new ChromeOptions
        {
        };
        options.AddArguments(
            Settings.Driver.Options.Arguments
                .Where(a => !string.IsNullOrWhiteSpace(a))
        );

        return options;
    }

    private static FirefoxOptions BuildFirefoxOptions()
    {
        var options = new FirefoxOptions
        {
        };
        options.AddArguments(
            Settings.Driver.Options.Arguments
                .Where(a => !string.IsNullOrWhiteSpace(a))
        );

        return options;
    }
}
