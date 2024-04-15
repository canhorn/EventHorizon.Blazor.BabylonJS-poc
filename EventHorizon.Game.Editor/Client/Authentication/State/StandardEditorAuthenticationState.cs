namespace EventHorizon.Game.Editor.Client.Authentication.State;

using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.Model;

public class StandardEditorAuthenticationState : EditorAuthenticationState
{
    public string AccessToken { get; private set; } = string.Empty;

    public SessionValues Session { get; private set; } = new SessionValuesModel();

    public void SetAccessToken(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return;
        }

        AccessToken = accessToken;
    }

    public void SetSessionValues(SessionValues sessionValues)
    {
        if (sessionValues.IsNull())
        {
            return;
        }
        Session = sessionValues;
    }
}
