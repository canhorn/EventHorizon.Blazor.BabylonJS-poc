namespace EventHorizon.Zone.Systems.ClientAssets.Query
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Zone.Systems.ClientAssets.Model;
    using MediatR;

    public struct QueryForClientAssetById
        : IRequest<CommandResult<ClientAsset>>
    {
        public string Id { get; }

        public QueryForClientAssetById(
            string id
        )
        {
            Id = id;
        }
    }
}
