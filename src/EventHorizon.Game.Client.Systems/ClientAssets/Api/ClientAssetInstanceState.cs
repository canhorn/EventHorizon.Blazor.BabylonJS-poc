﻿namespace EventHorizon.Game.Client.Systems.ClientAssets.Api;

using System.Collections.Generic;

public interface ClientAssetInstanceState
{
    Option<ClientAssetInstance> Get(string id);
    void Set(ClientAssetInstance clientAsset);
    void Remove(string id);
    void Clear();
}
