namespace EventHorizon.Game.Editor.Automation.AssetManagement.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.Components.Toolbar;
using EventHorizon.Game.Editor.Automation.Layout;

using _ = AssetManagementPage;

[Url("/asset/management")]
public class AssetManagementPage
    : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }
}
