namespace EventHorizon.Game.Client.Systems.ClientAssets.Builder.Loader
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Model.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Builder;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api.Configs;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using EventHorizon.Game.Client.Systems.Scripting.TESTING_SCRIPTS;
    using MediatR;

    public class BabylonJSScriptMeshLoader
        : IClientAssetLoader
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;

        public string Id => "SCRIPT:JavaScript";

        public BabylonJSScriptMeshLoader()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public async Task Load(
            ClientAssetMeshDetails details,
            IClientAsset clientAsset
        )
        {
            if (clientAsset.Config is IClientAssetScriptConfig config)
            {
                var mesh = default(Mesh);
                Action<Mesh> handleResolveMesh = (resolvedMesh) =>
               {
                   mesh = resolvedMesh;
               };
                await new CreateTreeScript().Run(
                    new ScriptData(
                        new Dictionary<string, object>
                        {
                            { "id", clientAsset.Id },
                            { "scene", _renderingScene.GetBabylonJSScene().Scene },
                            { "config", config },
                            { "resolve", handleResolveMesh }
                        }
                    )
                );
                // TODO: Move this into the Script when Moving into Server script
                if (mesh != null)
                {
                    await _mediator.Send(
                        new ResolveClientAssetMeshCommand(
                            details,
                            new BabylonJSEngineMesh(
                                mesh
                            )
                        )
                    );
                }
            }
        }
    }
}
