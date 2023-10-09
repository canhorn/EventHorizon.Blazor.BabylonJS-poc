namespace EventHorizon.Game.Editor.Automation.Layout;

using Atata;

using EventHorizon.Game.Editor.Automation.Components.Toast;
using EventHorizon.Game.Editor.Automation.Layout.Components;

public class MainLayoutPage<TOwner> : Page<TOwner>
    where TOwner : Page<TOwner>
{
    public MenuBarComponent<TOwner> TopBar { get; private set; }

    public NavigationSideBarComponent<TOwner> SideBar { get; private set; }

    public H1<TOwner> Header { get; private set; }

    //AlphaBanner<TOwner> AlphaBanner { get; }
    // TODO: Add CookieBanner
    //CookieBannerComponent<TOwner> CookieBanner { get; }

    public ToastComponent<TOwner> Toast { get; private set; }
}
