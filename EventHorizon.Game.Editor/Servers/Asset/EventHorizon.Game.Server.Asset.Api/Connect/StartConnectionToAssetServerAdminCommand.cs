namespace EventHorizon.Game.Server.Asset.Connect
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public struct StartConnectionToAssetServerAdminCommand
        : IRequest<StandardCommandResult>
    {
        public string AccessToken { get; }

        public StartConnectionToAssetServerAdminCommand(
            string accessToken
        )
        {
            AccessToken = accessToken;
        }
    }
}
