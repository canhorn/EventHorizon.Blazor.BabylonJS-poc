namespace EventHorizon.Game.Editor.Automation.Core.Browser;

using System;
using System.Linq;

using Atata;
using Atata.WebDriverSetup;

using EventHorizon.Game.Editor.Automation.Core.Browser.Api;
using EventHorizon.Game.Editor.Automation.Core.Browser.Model;
using EventHorizon.Game.Editor.Automation.Core.Config;

using Microsoft.Edge.SeleniumTools;
using Microsoft.Extensions.Configuration;

using NUnit.Framework;
using NUnit.Framework.Internal;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class WebHost
{
    [SetUp]
    public void SetUp()
    {
        AtataContext.Configure().Build();
    }

    [TearDown]
    public void TearDown()
    {
        AtataContext.Current?.CleanUp();
    }

    private static readonly WebHostSettings Settings = new WebHostSettingsModel();

    static WebHost()
    {
        TestConfiguration.Configuration.Bind(
            "webHost",
            Settings
        );
    }

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        AtataContext.GlobalConfiguration
            .UseBaseUrl(Settings.BaseUrl)
            .UseCulture(Settings.Culture)
            .UseAllNUnitFeatures()
            .UseDriver(
                () =>
                {
                    if (Settings.Driver.IsRemote)
                    {
                        return new RemoteWebDriver(
                            new Uri(
                                Settings.Driver.Url
                            ),
                            GetDriverOptions()
                        );
                    }
                    else if (Settings.Driver.Type == "edge")
                    {
                        return new EdgeDriver(
                            BuildEdgeOptions()
                        );
                    }
                    else if (Settings.Driver.Type == "chrome")
                    {
                        return new ChromeDriver(
                            BuildChromeOptions()
                        );
                    }
                    else if (Settings.Driver.Type == "firefox")
                    {
                        return new FirefoxDriver(
                            BuildFirefoxOptions()
                        );
                    }

                    throw new ArgumentException("Driver Type was not Valid");
                }
            );


        if (!Settings.Driver.IsRemote)
        {
            DriverSetup.AutoSetUp(Settings.Driver.Type.ToUpperFirstLetter());
        }

        if (Settings.SlowMo)
        {
            AtataContext.GlobalConfiguration
                .Attributes.Global.Add(
                   new WaitAttribute(Settings.SlowMoDelay)
                   {
                       On = TriggerEvents.BeforeAccess,
                   }
               );
        }
    }

    private static DriverOptions GetDriverOptions()
    {
        var driverOptions = Settings.Driver.Type switch
        {
            "edge" => BuildEdgeOptions(),
            "chrome" => BuildChromeOptions(),
            "firefox" => BuildFirefoxOptions(),
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
