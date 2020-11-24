namespace EventHorizon.Game.Editor.Client.Authentication.State
{
    using EventHorizon.Game.Editor.Client.Authentication.Api;

    public class StandardEditorAuthenticationState
        : EditorAuthenticationState
    {
        public string AccessToken { get; private set; } = string.Empty;

        public void SetAccessToken(
            string accessToken
        )
        {
            AccessToken = accessToken;
        }
    }
}
