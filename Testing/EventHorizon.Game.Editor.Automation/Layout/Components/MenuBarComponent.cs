namespace EventHorizon.Game.Editor.Automation.Layout.Components;

using Atata;

public class MenuBarComponent<TNavigateTo>
    : Control<TNavigateTo>
    where TNavigateTo : PageObject<TNavigateTo>
{
    [TestSelector("login-link", Timeout = 30)]
    public Link<TNavigateTo> LoginLink
    {
        get;
        private set;
    }

    [TestSelector("logout-link", Timeout = 30)]
    public Link<TNavigateTo> LogoutLink
    {
        get;
        private set;
    }
}
