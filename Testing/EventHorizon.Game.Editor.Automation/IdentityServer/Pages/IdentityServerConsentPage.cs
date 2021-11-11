namespace EventHorizon.Game.Editor.Automation.IdentityServer.Pages
{
    using Atata;
    using _ = IdentityServerConsentPage;

    public class IdentityServerConsentPage
        : Page<_>
    {
        public static string Url => $"/consent";

        [FindById]
        public Button<_> Yes { get; private set; }
    }
}
