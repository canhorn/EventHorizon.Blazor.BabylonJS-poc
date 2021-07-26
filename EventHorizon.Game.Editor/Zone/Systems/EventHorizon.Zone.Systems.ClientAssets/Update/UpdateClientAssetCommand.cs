namespace EventHorizon.Zone.Systems.ClientAssets.Update
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Zone.Systems.ClientAssets.Model;
    using MediatR;

    public struct UpdateClientAssetCommand
        : IRequest<StandardCommandResult>
    {
        public ClientAsset ClientAsset { get; }

        public UpdateClientAssetCommand(
            ClientAsset clientAsset
        )
        {
            ClientAsset = clientAsset;
        }
    }
}
