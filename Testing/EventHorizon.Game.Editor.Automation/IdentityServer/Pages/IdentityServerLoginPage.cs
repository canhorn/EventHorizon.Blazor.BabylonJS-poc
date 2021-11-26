namespace EventHorizon.Game.Editor.Automation.IdentityServer.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.Layout.Components;

using _ = IdentityServerLoginPage;

public class IdentityServerLoginPage : Page<_>
{
    public static string Url => $"/account/login";

    public EmailInput<_> Email { get; private set; }

    public PasswordInput<_> Password
    {
        get;
        private set;
    }

    public CookieBannerComponent<_> CookieBanner
    {
        get;
        private set;
    }

    [FindById("login-button")]
    public Button<_> Login { get; private set; }

    [FindById("register-new-user-link")]
    public Link<_> RegisterNewUser { get; private set; }
}
