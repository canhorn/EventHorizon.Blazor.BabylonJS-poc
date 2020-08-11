namespace EventHorizon.Game.Client.Systems.ClientAssets.Register
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Api;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
    using MediatR;

    public class RegisterClientAssetInstanceCommand
        : IRequest<StandardCommandResult>
    {
        public string AssetInstanceId { get; }
        public IEngineMesh Mesh { get; }
        public IVector3 Position { get; }

        public RegisterClientAssetInstanceCommand(
            string assetInstanceId,
            IEngineMesh mesh,
            IVector3 position
        )
        {
            AssetInstanceId = assetInstanceId;
            Mesh = mesh;
            Position = position;
        }
    }
}
