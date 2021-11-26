namespace EventHorizon.Game.Editor.Automation.Layout.Components;

using System;

using Atata;

public class CookieBannerComponent<TNavigateTo>
    : Control<TNavigateTo>
        where TNavigateTo : PageObject<TNavigateTo>
{
    [FindByCss(".cookie-banner__button")]
    public Button<TNavigateTo> AcceptAndClose { get; private set; }
}
