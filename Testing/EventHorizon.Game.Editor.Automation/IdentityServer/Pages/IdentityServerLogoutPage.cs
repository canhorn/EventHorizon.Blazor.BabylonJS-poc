namespace EventHorizon.Game.Editor.Automation.IdentityServer.Pages;

using Atata;
using _ = IdentityServerLogoutPage;

public class IdentityServerLogoutPage : Page<_>
{
    public static string Url => "/Account/Logout";

    [FindById("logout-confirm-button")]
    public Button<_> Yes { get; set; }
}
