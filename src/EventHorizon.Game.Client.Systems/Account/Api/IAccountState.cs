namespace EventHorizon.Game.Client.Systems.Account.Api
{
    using EventHorizon.Game.Client.Systems.Connection.Core.Model;

    public interface IAccountState
    {
        string? AccessToken { get; }
        string? AccountLoginUrl { get; }
        void Setup(
            string accessToken,
            string accountLoginUrl
        );
        IAccountInfo? User { get; }
        void SetAccountUser(
            IAccountInfo user
        );
        void Reset();
    }
}
