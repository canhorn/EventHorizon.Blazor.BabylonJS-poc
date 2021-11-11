namespace EventHorizon.Game.Editor.Automation.IdentityServer.Components
{
    using Atata;

    public class LoginComponent<TPage>
        : Control<TPage> where TPage : PageObject<TPage>
    {
        [FindByAttribute("data-test-selector", "login-link")]
        public Link<TPage> LoginLink { get; private set; }
    }
}
