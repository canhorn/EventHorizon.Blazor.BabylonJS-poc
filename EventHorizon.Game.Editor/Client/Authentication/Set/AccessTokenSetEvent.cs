namespace EventHorizon.Game.Editor.Client.Authentication.Set
{
    using MediatR;

    public struct AccessTokenSetEvent
        : INotification
    {
        public string AccessToken { get; }

        public AccessTokenSetEvent(
            string accessToken
        )
        {
            AccessToken = accessToken;
        }
    }
}
