namespace EventHorizon.Game.Client.Systems.ClientAssets.Config.Api;

using System.Collections.Generic;
using EventHorizon.Game.Client.Systems.ClientAssets.Api;

public interface ClientAssetConfigTypeBuilder
{
    ClientAssetConfig Build(IDictionary<string, object> data);
}
