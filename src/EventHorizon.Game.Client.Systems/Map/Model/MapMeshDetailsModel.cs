namespace EventHorizon.Game.Client.Systems.Map.Model
{
    using EventHorizon.Game.Client.Systems.Map.Api;

    public class MapMeshDetailsModel
        : IMapMeshDetails
    {
        public string Name { get; set; } = string.Empty;
        public string HeightMapUrl { get; set; } = string.Empty;
        public string Light { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Subdivisions { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
        public bool Updatable { get; set; }
        public bool IsPickable { get; set; }
        public MapMeshMaterialModel Material { get; set; } = new MapMeshMaterialModel();
        IMapMeshMaterialDetails IMapMeshDetails.Material => Material;
    }
}
