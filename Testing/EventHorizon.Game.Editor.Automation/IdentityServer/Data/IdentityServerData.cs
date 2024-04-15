namespace EventHorizon.Game.Editor.Automation.IdentityServer.Data;

using EventHorizon.Game.Editor.Automation.Core.Config;
using EventHorizon.Game.Editor.Automation.IdentityServer.Models;
using Microsoft.Extensions.Configuration;

public class IdentityServerData
{
    public static string Url { get; } = string.Empty;
    public static string EmailDomain { get; } = string.Empty;
    public static string UserPassword { get; } = string.Empty;

    public static IdentityServerUser DefaultAdminUser { get; } = new IdentityServerUser();

    static IdentityServerData()
    {
        Url = TestConfiguration.Configuration["identityServer:url"];
        EmailDomain = TestConfiguration.Configuration["identityServer:emailDomain"];
        UserPassword = TestConfiguration.Configuration["identityServer:userPassword"];
        TestConfiguration.Configuration.Bind("identityServer:defaultAdminUser", DefaultAdminUser);
    }
}
