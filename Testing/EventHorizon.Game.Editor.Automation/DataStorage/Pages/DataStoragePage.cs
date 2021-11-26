namespace EventHorizon.Game.Editor.Automation.DataStorage.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.Layout;

using _ = DataStoragePage;

[Url("/data-storage")]
public class DataStoragePage : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }
}
