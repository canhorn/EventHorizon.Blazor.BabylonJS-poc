namespace EventHorizon.Game.Editor.Automation.Core.Config;

using Microsoft.Extensions.Configuration;

public class TestConfiguration
{
    public static IConfiguration Configuration { get; }

    static TestConfiguration()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("Config.json")
            .AddJsonFile("Config.Override.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}
