namespace EventHorizon.Game.Editor.Automation.Core.Browser
{
    using System;

    using Atata;

    using EventHorizon.Game.Editor.Automation.Authentication.Pages;
    using EventHorizon.Game.Editor.Automation.IdentityServer.Data;
    using EventHorizon.Game.Editor.Automation.IdentityServer.Models;
    using EventHorizon.Game.Editor.Automation.IdentityServer.Pages;
    using EventHorizon.Game.Editor.Automation.Layout;

    public static class IdentityServerTestingExtensions
    {
        public static TOwner Login<TOwner>(
            this WebHost _,
            IdentityServerUser user = null
        ) where TOwner : MainLayoutPage<TOwner>
        {
            if (user == null)
            {
                user = IdentityServerData.DefaultAdminUser;
            }

            Go.To<TOwner>()
                .Wait(0.1)
                .Do(
                    currentPage =>
                    {
                        // Check if we are already on the login page,
                        // this can happen if the Go.To<TOwner> page redirect was to an
                        // Authorized page, and so the platform will auto redirect to the login page.
                        if (
                            currentPage.PageUrl.Value.Contains(
                                IdentityServerLoginPage.Url,
                                StringComparison.InvariantCultureIgnoreCase
                            )
                        )
                        {
                            return currentPage;
                        }

                        // Current page, a Layout Page.
                        // Click LoginLink on TopBar Component
                        return currentPage.TopBar.LoginLink.Click();
                    }
                );

            return Go.To<IdentityServerLoginPage>(
                navigate: false
            )
                .CookieBanner.AcceptAndClose.Click()
                .Email.Set(user.Email)
                .Password.Set(user.Password)
                .Login.Click()
                .Do(
                    currentPage =>
                    {
                        if (
                            currentPage.PageUrl.Value.Contains(
                                IdentityServerConsentPage.Url,
                                StringComparison.InvariantCultureIgnoreCase
                            )
                        )
                        {
                            return Go.To<IdentityServerConsentPage>(
                                navigate: false
                            )
                                .Yes.ClickAndGo<TOwner>();
                        }

                        // Validate LogoutLink IsVisible
                        Go.To<TOwner>(navigate: false)
                            .TopBar.LogoutLink.IsVisible.Should.BeTrue();

                        // Navigate to the page
                        return Go.To<TOwner>();
                    }
                );
        }

        public static TOwner Logout<TOwner>(
            this PageObject<TOwner> _
        ) where TOwner : MainLayoutPage<TOwner>
        {
            Go.To<LogoutPage>();

            return Go.To<TOwner>();
        }

        public static TOwner NewUserRegister<TOwner>(
            this WebHost _,
            out IdentityServerUser user
        ) where TOwner : MainLayoutPage<TOwner>
        {
            var userId = Guid.NewGuid();
            user = new IdentityServerUser
            {
                Email =
                    $"user_{userId}@{IdentityServerData.EmailDomain}",
                Password = IdentityServerData.UserPassword,
                FirstName = $"User ({userId})",
            };

            // Should Redirect to Identity Server Login Page
            Go.To<IdentityServerLoginPage>(
                url: $"{IdentityServerData.Url}{IdentityServerLoginPage.Url}"
            )
                .RegisterNewUser.ClickAndGo<IdentityServerRegisterPage>()
                .Email.Set(user.Email)
                .Password.Set(user.Password)
                .ConfirmPassword.Set(user.Password)
                .FirstName.Set(user.FirstName)
                .ServiceAgreements.Check()
                .Register.Click()
                .TopBar.LoginUserName.IsVisible.Should.BeTrue();

            Go.To<IdentityServerLogoutPage>(
                url: $"{IdentityServerData.Url}{IdentityServerLogoutPage.Url}"
            )
                .Yes.Click();

            return Login<TOwner>(_, user);
        }
    }
}
