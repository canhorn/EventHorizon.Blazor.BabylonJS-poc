namespace EventHorizon.Game.Editor.Client.Authentication.Api
{
    public interface EditorAuthenticationState
    {
        string AccessToken { get; }

        SessionValues Session { get; }

        void SetAccessToken(
            string accessToken
        );

        void SetSessionValues(
            SessionValues sessionValues
        );
    }
}
