namespace EventHorizon.Game.Editor.Automation.IdentityServer.Pages;

using Atata;
using _ = IdentityServerHomePage;

public class IdentityServerHomePage : Page<_>
{
    public static string Url => $"/";

    [FindById("home-page-title")]
    public H1<_> Header { get; private set; }
}
