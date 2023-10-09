namespace EventHorizon.Game.Client.Systems.Map.Api;

public interface IMapMeshDetails
{
    string HeightMapUrl { get; }
    string Light { get; }
    int Width { get; }
    int Height { get; }
    int Subdivisions { get; }
    int MinHeight { get; }
    int MaxHeight { get; }
    bool Updatable { get; }
    bool IsPickable { get; }
    IMapMeshMaterialDetails Material { get; }
}
