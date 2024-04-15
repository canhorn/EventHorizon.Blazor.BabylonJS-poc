namespace EventHorizon.Game.Editor.Automation.IdentityServer.Components;

using System;
using Atata;

public class IdentityServerTopBarComponent<TNavigateTo> : Control<TNavigateTo>
    where TNavigateTo : PageObject<TNavigateTo>
{
    // TODO: Update AuthServer with specific
    [FindByCss(".navbar-nav .dropdown .dropdown-toggle")]
    public Text<TNavigateTo> LoginUserName { get; private set; }
}
