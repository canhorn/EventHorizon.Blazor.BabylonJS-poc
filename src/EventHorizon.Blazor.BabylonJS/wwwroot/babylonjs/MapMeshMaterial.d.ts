/**
 * This Material creates a material that will blend ground, grass, snow, sand, and rock textures.
 */
export class MapMeshMaterial extends BABYLON.StandardMaterial {
    private _settings: IMapMeshMaterial;

    public light: BABYLON.PointLight;
    public groundTexture: BABYLON.Texture;
    public grassTexture: BABYLON.Texture;
    public snowTexture: BABYLON.Texture;
    public sandTexture: BABYLON.Texture;
    public rockTexture: BABYLON.Texture;
    public blendTexture: BABYLON.Texture;
    public sandLimit: number;
    public rockLimit: number;
    public snowLimit: number;

    constructor(
        settings: IMapMeshMaterial,
        name: string,
        light: PointLight,
        assetPath: string,
        shaderPath,
        scene: Scene
    );
}
