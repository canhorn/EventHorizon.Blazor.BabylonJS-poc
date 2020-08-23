namespace EventHorizon.Game.Client.Core.I18n.Api
{
    using System;

    public interface ILocalizer
    {
        string this[string name] { get; }
        string this[string name, params object[] arguments] { get; }
    }
}
