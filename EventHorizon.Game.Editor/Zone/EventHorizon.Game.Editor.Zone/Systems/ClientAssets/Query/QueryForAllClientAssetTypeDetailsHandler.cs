namespace EventHorizon.Game.Editor.Zone.Systems.ClientAssets.Query
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Properties.Model;
    using EventHorizon.Zone.Systems.ClientAssets.Model;
    using EventHorizon.Zone.Systems.ClientAssets.Query;

    using MediatR;

    public class QueryForAllClientAssetTypeDetailsHandler
        : IRequestHandler<QueryForAllClientAssetTypeDetails, CommandResult<IEnumerable<ClientAssetTypeDetails>>>
    {
        public Task<CommandResult<IEnumerable<ClientAssetTypeDetails>>> Handle(
            QueryForAllClientAssetTypeDetails request,
            CancellationToken cancellationToken
        )
        {
            // TODO: Query this from the Zone Server
            return new CommandResult<IEnumerable<ClientAssetTypeDetails>>(
                new List<ClientAssetTypeDetails>
                {
                    new()
                    {
                        Type = "SCRIPT:JavaScript",
                        Name = "JavaScript Script (Compiled)",
                        Metadata = new Dictionary<string, string>
                        {
                            ["script"] = PropertyType.String,
                            ["branchSize"] = PropertyType.Decimal,
                            ["trunkSize"] = PropertyType.Decimal,
                            ["radius"] = PropertyType.Decimal,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["script"] = string.Empty,
                            ["branchSize"] = 4m,
                            ["trunkSize"] = 4m,
                            ["radius"] = 1m,
                        },
                    },
                    new()
                    {
                        Type = "MESH:BOX",
                        Name = "Mesh Box",
                        Metadata = new Dictionary<string, string>
                        {
                            ["size"] = PropertyType.Decimal,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["size"] = 2m,
                        },
                    },
                    new()
                    {
                        Type = "MESH:SPHERE",
                        Name = "Mesh Sphere",
                        Metadata = new Dictionary<string, string>
                        {
                            ["segments"] = PropertyType.Decimal,
                            ["diameter"] = PropertyType.Decimal,
                            ["heightOffset"] = PropertyType.Decimal,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["segments"] = 16m,
                            ["diameter"] = 2m,
                            ["heightOffset"] = 0m,
                        },
                    },
                    new()
                    {
                        Type = "MESH:GLTF",
                        Name = "Mesh glTF",
                        Metadata = new Dictionary<string, string>
                        {
                            ["path"] = PropertyType.String,
                            ["file"] = PropertyType.String,
                            ["fileAssetPath"] = PropertyType.AssetServerFile,
                            ["heightOffset"] = PropertyType.Decimal,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["path"] = string.Empty,
                            ["file"] = string.Empty,
                            ["fileAssetPath"] = string.Empty,
                            ["heightOffset"] = 0m,
                        },
                    },
                    new()
                    {
                        Type = "MESH:MAP",
                        Name = "Map Mesh",
                        Metadata = new Dictionary<string, string>
                        {
                            ["heightMapUrl"] = PropertyType.String,
                            ["heightMapAssetPath"] = PropertyType.AssetServerFile,
                            ["width"] = PropertyType.Decimal,
                            ["height"] = PropertyType.Decimal,
                            ["subdivisions"] = PropertyType.Decimal,
                            ["minHeight"] = PropertyType.Decimal,
                            ["maxHeight"] = PropertyType.Decimal,
                            ["updatable"] = PropertyType.Boolean,
                            ["isPickable"] = PropertyType.Boolean,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["heightMapUrl"] = string.Empty,
                            ["heightMapAssetPath"] = string.Empty,
                            ["width"] = 32,
                            ["height"] = 32,
                            ["subdivisions"] = 100,
                            ["minHeight"] = 0,
                            ["maxHeight"] = 12,
                            ["updatable"] = true,
                            ["isPickable"] = true,
                        },
                    },
                    new()
                    {
                        Type = "MATERIAL:MAP",
                        Name = "Map Material",
                        Metadata = new Dictionary<string, string>
                        {
                            ["assetPath"] = PropertyType.AssetServerPath,
                            ["shaderId"] = PropertyType.Decimal,
                            ["shader"] = PropertyType.Decimal,
                            ["lightName"] = PropertyType.Decimal,
                            ["groundTexture"] = PropertyType.Complex,
                            ["snowTexture"] = PropertyType.Complex,
                            ["sandTexture"] = PropertyType.Complex,
                            ["rockTexture"] = PropertyType.Complex,
                            ["blendTexture"] = PropertyType.Complex,
                            ["sandLimit"] = PropertyType.Decimal,
                            ["rockLimit"] = PropertyType.Decimal,
                            ["snowLimit"] = PropertyType.Decimal,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["assetPath"] = string.Empty,
                            ["shaderId"] = string.Empty,
                            ["shader"] = string.Empty,
                            ["lightName"] = string.Empty,
                            ["groundTexture"] = new ComplexProperty
                            {
                                ["image"] = string.Empty,
                                ["uScale"] = 1m,
                                ["vScale"] = 1m,
                            },
                            ["snowTexture"] = new ComplexProperty
                            {
                                ["image"] = string.Empty,
                                ["uScale"] = 1m,
                                ["vScale"] = 1m,
                            },
                            ["sandTexture"] =  new ComplexProperty
                            {
                                ["image"] = string.Empty,
                                ["uScale"] = 1m,
                                ["vScale"] = 1m,
                            },
                            ["rockTexture"] =  new ComplexProperty
                            {
                                ["image"] = string.Empty,
                                ["uScale"] = 1m,
                                ["vScale"] = 1m,
                            },
                            ["blendTexture"] =  new ComplexProperty
                            {
                                ["image"] = string.Empty,
                            },
                            ["sandLimit"] = 1,
                            ["rockLimit"] = 5,
                            ["snowLimit"] = 8,
                        },
                    },
                    new()
                    {
                        Type = "IMAGE:URL",
                        Name = "Image",
                        Metadata = new Dictionary<string, string>
                        {
                            ["url"] = PropertyType.String, 
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["url"] = string.Empty,
                        },
                    },
                    new()
                    {
                        Type = "DIALOG",
                        Name = "Dialog",
                        Metadata = new Dictionary<string, string>
                        {
                            ["root"] = PropertyType.Complex,
                        },
                        DefaultValue = () => new Dictionary<string, object>
                        {
                            ["root"] = new ComplexProperty(),
                        },
                    },
                }
            ).FromResult();
        }
    }
}
