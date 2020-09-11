namespace EventHorizon.Game.Client.Systems.Entity.Properties.Model.Api
{
    using System;

    public interface IModelState
    {
        public static string NAME => "modelState";

        decimal? ScalingDeterminant { get; }
        IModelMesh Mesh { get; }
    }

    public interface IModelMesh
    {
        string AssetId { get; }
    }
}
