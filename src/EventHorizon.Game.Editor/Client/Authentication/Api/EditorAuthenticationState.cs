namespace EventHorizon.Game.Editor.Client.Authentication.Api
{
    public interface EditorAuthenticationState
    {
        string AccessToken { get; }

        void SetAccessToken(
            string accessToken
        );
    }
}
