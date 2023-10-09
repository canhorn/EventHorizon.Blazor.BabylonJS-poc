namespace EventHorizon.Game.Client.Engine.Gui.Api;

using System;
using System.Collections.Generic;

public interface IGuiControlOptions : IDictionary<string, object>
{
    Option<T> GetValue<T>(string key);
    public T GetValue<T>(string key, Func<T> defaultValue);
    public void HasValueCallback<T>(string key, Action<T> callback);
}
