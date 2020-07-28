namespace EventHorizon.Game.Client.Systems.Map.Model
{
    using EventHorizon.Game.Client.Systems.Map.Api;

    public class MapMeshDetailsModel
        : IMapMeshDetails
    {
        public string Name { get; set; }
        public string HeightMapUrl { get; set; }
        public string Light { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Subdivisions { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
        public bool Updatable { get; set; }
        public bool IsPickable { get; set; }
        public MapMeshMaterialModel Material { get; set; }
        IMapMeshMaterialDetails IMapMeshDetails.Material => Material;
    }
}
