namespace EventHorizon.Game.Client.Engine.Systems.Mesh.Model
{
    public class MeshSystemType
    {
        public static MeshSystemType NONE = new MeshSystemType("NONE");
        public static MeshSystemType ENTITY = new MeshSystemType("ENTITY");
        public static MeshSystemType CLIENT_ENTITY = new MeshSystemType("CLIENT_ENTITY");

        public string Type { get; }

        private MeshSystemType(
            string type
        )
        {
            Type = type;
        }
    }
}
