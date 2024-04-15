namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;

using System;
using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;

public class StandardClientAssetConfigTypeBuilder : ClientAssetConfigTypeBuilder
{
    private readonly Func<IDictionary<string, object>, ClientAssetConfig> _build;

    public StandardClientAssetConfigTypeBuilder(
        Func<IDictionary<string, object>, ClientAssetConfig> build
    )
    {
        _build = build;
    }

    public ClientAssetConfig Build(IDictionary<string, object> data)
    {
        return _build(data);
    }
}
