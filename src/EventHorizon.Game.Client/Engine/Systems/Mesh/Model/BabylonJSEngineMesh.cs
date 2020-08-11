namespace EventHorizon.Game.Client.Engine.Systems.Mesh.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

    public class BabylonJSEngineMesh
        : IEngineMesh
    {
        public Mesh Mesh { get; }

        public IVector3 Position { get; }
        public IVector3 Rotation { get; }
        public IVector3 Scaling { get; }
        public decimal ScalingDeterminant
        {
            get
            {
                return Mesh.scalingDeterminant;
            }
            set
            {
                Mesh.scalingDeterminant = value;
            }
        }
        public MeshSystemType SystemType { get; set; }
        public long OwnerEntityId { get; set; }
        public IDictionary<string, object> MetaData { get; }

        public BabylonJSEngineMesh(
            Mesh mesh,
            MeshSystemType meshSystemType,
            long ownerEntityId
        )
        {
            Mesh = mesh;
            Position = new BabylonJSVector3(
                Mesh.position
            );
            Rotation = new BabylonJSVector3(
                Mesh.rotation
            );
            Scaling = new BabylonJSVector3(
                Mesh.scaling
            );
            SystemType = meshSystemType;
            OwnerEntityId = ownerEntityId;
            MetaData = new Dictionary<string, object>();
        }

        public BabylonJSEngineMesh(
            Mesh mesh
        ) : this(
            mesh,
            MeshSystemType.NONE,
            -1
        )
        {
        }

        public void Dispose() => Mesh.dispose();

        public void SetEnabled(
            bool value
        ) => Mesh.setEnabled(value);

        public IEngineMesh Clone(
            string identifier
        ) => new BabylonJSEngineMesh(
            Mesh.clone(
                identifier
            )
        );
    }
}
