namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using MediatR;

    public class BuildClientAssetInstanceCommand
        : IRequest<StandardCommandResult>
    {
        public string AssetInstanceId { get; }
        public IClientAsset ClientAsset { get; }
        public IVector3 Position { get; }

        public BuildClientAssetInstanceCommand(
            string assetInstanceId,
            IClientAsset clientAsset,
            IVector3 position
        )
        {
            AssetInstanceId = assetInstanceId;
            ClientAsset = clientAsset;
            Position = position;
        }
    }
}
