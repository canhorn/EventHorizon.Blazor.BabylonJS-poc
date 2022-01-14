namespace EventHorizon.Game.Editor.Client.Localization;

using EventHorizon.Game.Editor.Client.Localization.Api;

public class StringLocalizerMock<T>
    : Localizer<SharedResource>
{
    public string this[string name] => $"{name}";
    public string this[string name, params object[] arguments] =>
        string.Format(name, arguments);
}
