﻿//import {
//    AbstractMesh,
//    Matrix,
//    Mesh,
//    PointLight,
//    StandardMaterial,
//    SubMesh,
//    Texture,
//} from "babylonjs";
//import { IRenderingScene } from "../../../../engine/renderer/api/IRenderingScene";
//import { createAssetLocationUrl } from "../../../../engine/system/assetServer/api/CreateAssetLocationUrl";
//import { IMapMeshMaterial } from "../../api/IMapMesh";

/**
 * This Material creates a material that will blend ground, grass, snow, sand, and rock textures.
 */
export class MapMeshMaterial extends BabylonJS.StandardMaterial {
    _settings;
    _shaderPath;

    light;
    groundTexture;
    grassTexture;
    snowTexture;
    sandTexture;
    rockTexture;
    blendTexture;
    sandLimit;
    rockLimit;
    snowLimit;

    constructor(
        settings,
        name,
        light,
        assetPath,
        shaderPath,
        scene
    ) {
        super(name, scene);
        this._settings = settings;
        this._shaderPath = shaderPath;
        if (light) {
            this.setLight(light);
        }
        //const assetPath = createAssetLocationUrl(this._settings.assetPath);

        this.groundTexture = new BabylonJS.Texture(
            this.buildAssetPath(assetPath, _settings.groundTexture.image),
            scene
        );
        this.groundTexture.uScale = _settings.groundTexture.uScale;
        this.groundTexture.vScale = _settings.groundTexture.vScale;

        this.grassTexture = new BabylonJS.Texture(
            this.buildAssetPath(assetPath, _settings.grassTexture.image),
            scene
        );
        this.grassTexture.uScale = _settings.grassTexture.uScale;
        this.grassTexture.vScale = _settings.grassTexture.vScale;

        this.snowTexture = new BabylonJS.Texture(
            this.buildAssetPath(assetPath, _settings.snowTexture.image),
            scene
        );
        this.snowTexture.uScale = _settings.snowTexture.uScale;
        this.snowTexture.vScale = _settings.snowTexture.vScale;

        this.sandTexture = new BabylonJS.Texture(
            this.buildAssetPath(assetPath, _settings.sandTexture.image),
            scene
        );
        this.sandTexture.uScale = _settings.sandTexture.uScale;
        this.sandTexture.vScale = _settings.sandTexture.vScale;

        this.rockTexture = new BabylonJS.Texture(
            this.buildAssetPath(assetPath, _settings.rockTexture.image),
            scene
        );
        this.rockTexture.uScale = _settings.rockTexture.uScale;
        this.rockTexture.vScale = _settings.rockTexture.vScale;

        this.blendTexture = new BabylonJS.Texture(
            this.buildAssetPath(assetPath, _settings.blendTexture.image),
            scene
        );
        this.blendTexture.uOffset = Math.random();
        this.blendTexture.vOffset = Math.random();
        this.blendTexture.wrapU = BabylonJS.Texture.MIRROR_ADDRESSMODE;
        this.blendTexture.wrapV = BabylonJS.Texture.MIRROR_ADDRESSMODE;

        this.sandLimit = 1;
        this.rockLimit = 5;
        this.snowLimit = 8;
    }

    setLight(light) {
        this.light = light;
    }

    needAlphaBlending() {
        return false;
    }
    needAlphaTesting() {
        return false;
    }
    isReadyForSubMesh(_, subMesh) {
        const engine = this.getScene().getEngine();

        if (!this.groundTexture.isReady()) {
            return false;
        }
        if (!this.snowTexture.isReady()) {
            return false;
        }
        if (!this.sandTexture.isReady()) {
            return false;
        }
        if (!this.rockTexture.isReady()) {
            return false;
        }
        if (!this.grassTexture.isReady()) {
            return false;
        }

        const defines = [];
        if (this.getScene().clipPlane) {
            defines.push("#define CLIPPLANE");
        }

        const join = defines.join("\n");
        //const shaderPath = createAssetLocationUrl(this._settings.shader);

        const effect =
            subMesh.effect ||
            engine.createEffect(
                this._shaderPath,
                ["position", "normal", "uv"],
                [
                    "worldViewProjection",
                    "groundMatrix",
                    "sandMatrix",
                    "rockMatrix",
                    "snowMatrix",
                    "grassMatrix",
                    "blendMatrix",
                    "world",
                    "vLightPosition",
                    "vLimits",
                    "vClipPlane",
                ],
                [
                    "groundSampler",
                    "sandSampler",
                    "rockSampler",
                    "snowSampler",
                    "grassSampler",
                    "blendSampler",
                ],
                join
            );

        subMesh.setEffect(effect);
        if (!effect.isReady()) {
            return false;
        }

        this._wasPreviouslyReady = true;

        return true;
    }

    bindForSubMesh(world, _, subMesh) {
        if (!subMesh.effect) {
            return;
        }
        subMesh.effect.setMatrix("world", world);
        subMesh.effect.setMatrix(
            "worldViewProjection",
            world.multiply(this.getScene().getTransformMatrix())
        );
        if (this.light) {
            subMesh.effect.setVector3(
                "vLightPosition",
                this.light.getAbsolutePosition()
            );
        }

        // Textures
        if (this.groundTexture) {
            subMesh.effect.setTexture("groundSampler", this.groundTexture);
            subMesh.effect.setMatrix(
                "groundMatrix",
                this.groundTexture.getTextureMatrix()
            );
        }
        if (this.sandTexture) {
            subMesh.effect.setTexture("sandSampler", this.sandTexture);
            subMesh.effect.setMatrix(
                "sandMatrix",
                this.sandTexture.getTextureMatrix()
            );
        }
        if (this.rockTexture) {
            subMesh.effect.setTexture("rockSampler", this.rockTexture);
            subMesh.effect.setMatrix(
                "rockMatrix",
                this.rockTexture.getTextureMatrix()
            );
        }
        if (this.snowTexture) {
            subMesh.effect.setTexture("snowSampler", this.snowTexture);
            subMesh.effect.setMatrix(
                "snowMatrix",
                this.snowTexture.getTextureMatrix()
            );
        }
        if (this.grassTexture) {
            subMesh.effect.setTexture("grassSampler", this.grassTexture);
            subMesh.effect.setMatrix(
                "grassMatrix",
                this.grassTexture.getTextureMatrix()
            );
        }
        if (this.blendTexture) {
            subMesh.effect.setTexture("blendSampler", this.blendTexture);
            subMesh.effect.setMatrix(
                "blendMatrix",
                this.blendTexture.getTextureMatrix()
            );
        }

        subMesh.effect.setFloat3(
            "vLimits",
            this.sandLimit,
            this.rockLimit,
            this.snowLimit
        );

        const clipPlane = this.getScene().clipPlane;
        if (clipPlane) {
            subMesh.effect.setFloat4(
                "vClipPlane",
                clipPlane.normal.x,
                clipPlane.normal.y,
                clipPlane.normal.z,
                clipPlane.d
            );
        }
    }

    dispose() {
        if (this.grassTexture) {
            this.grassTexture.dispose();
        }
        if (this.groundTexture) {
            this.groundTexture.dispose();
        }
        if (this.snowTexture) {
            this.snowTexture.dispose();
        }
        if (this.sandTexture) {
            this.sandTexture.dispose();
        }
        if (this.rockTexture) {
            this.rockTexture.dispose();
        }

        super.dispose();
    }

    buildAssetPath(assetPath, assetFile) {
        return `${assetPath}/${assetFile}`;
    }
}
