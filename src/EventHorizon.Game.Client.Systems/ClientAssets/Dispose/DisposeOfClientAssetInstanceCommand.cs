namespace EventHorizon.Game.Client.Systems.ClientAssets.Dispose
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public class DisposeOfClientAssetInstanceCommand
        : IRequest<StandardCommandResult>
    {
        public string AssetInstanceId { get; }

        public DisposeOfClientAssetInstanceCommand(
            string assetInstanceId
        )
        {
            AssetInstanceId = assetInstanceId;
        }
    }
}
