namespace EventHorizon.Game.Editor.Automation.IdentityServer.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.IdentityServer.Components;

using _ = IdentityServerRegisterPage;

public class IdentityServerRegisterPage : Page<_>
{
    public static string Url => "/Register";

    public IdentityServerTopBarComponent<_> TopBar { get; private set; }

    public EmailInput<_> Email { get; private set; }

    [FindById("UserRegistration_Password")]
    public PasswordInput<_> Password { get; private set; }

    [FindById("UserRegistration_ConfirmPassword")]
    public PasswordInput<_> ConfirmPassword { get; private set; }

    [FindById("UserRegistration_Profile_FirstName")]
    public TextInput<_> FirstName { get; private set; }

    [FindById("UserRegistration_AcceptServiceAgreements")]
    public CheckBox<_> ServiceAgreements { get; private set; }

    public Button<_> Register { get; private set; }
}
