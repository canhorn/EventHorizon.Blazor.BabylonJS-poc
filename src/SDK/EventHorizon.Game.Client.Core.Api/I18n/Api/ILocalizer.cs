namespace EventHorizon.Game.Client.Core.I18n.Api;

using System;
using System.Collections.Generic;

public interface ILocalizer
{
    string this[string name] { get; }
    string this[string name, params object[] arguments] { get; }

    string Template(string name, IDictionary<string, object> data);
}
