namespace EventHorizon.Game.Client.Core.Configuration;

using System;
using System.Collections.Generic;
using System.Text;

public static class Configuration
{
    public static IDictionary<string, object> VALUES =
        new Dictionary<string, object>();

    public static void SetConfig(string key, object value)
    {
        VALUES[key] = value;
    }

    public static T GetConfig<T>(string key)
    {
        return (T)VALUES[key];
    }
}
