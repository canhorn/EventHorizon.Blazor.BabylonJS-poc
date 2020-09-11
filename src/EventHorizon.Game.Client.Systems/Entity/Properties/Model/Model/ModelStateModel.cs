namespace EventHorizon.Game.Client.Systems.Entity.Properties.Model.Model
{
    using System;
    using EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api;

    public class ModelStateModel
        : IModelState
    {
        public decimal? ScalingDeterminant { get; set; }

        public StandardModelMesh Mesh { get; set; } = new StandardModelMesh();
        IModelMesh IModelState.Mesh => Mesh;
    }
    public class StandardModelMesh
        : IModelMesh
    {
        public string AssetId { get; set; } = string.Empty;
    }
}
