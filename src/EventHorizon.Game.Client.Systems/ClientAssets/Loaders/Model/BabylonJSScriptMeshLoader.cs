namespace EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BabylonJS;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Systems.Mesh.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api.ConfigTypes;
    using EventHorizon.Game.Client.Systems.ClientAssets.Loaders.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model.Mesh;
    using EventHorizon.Game.Client.Systems.ClientAssets.Resolve;
    using EventHorizon.Game.Client.Systems.Scripting.TESTING_SCRIPTS;
    using MediatR;

    public class BabylonJSScriptMeshLoader
        : ClientAssetLoader
    {
        private readonly IMediator _mediator;
        private readonly IRenderingScene _renderingScene;

        public BabylonJSScriptMeshLoader()
        {
            _mediator = GameServiceProvider.GetService<IMediator>();
            _renderingScene = GameServiceProvider.GetService<IRenderingScene>();
        }

        public ClientAssetDetails CreateDetails(
            string assetInstanceId,
            ClientAsset clientAsset,
            IVector3 position
        )
        {
            return new ClientAssetMeshDetails(
                assetInstanceId,
                clientAsset,
                position
            );
        }

        public async Task Load(
            ClientAssetDetails details,
            ClientAsset clientAsset
        )
        {
            if (clientAsset.Config is ClientAssetScriptConfig config)
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
                // TODO: [TESTING] Move this into the Script when Moving into Server script
                if (mesh.IsNotNull()
                    && details is ClientAssetMeshDetails typedDetails)
                {
                    await _mediator.Send(
                        new ResolveClientAssetMeshCommand(
                            typedDetails,
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
