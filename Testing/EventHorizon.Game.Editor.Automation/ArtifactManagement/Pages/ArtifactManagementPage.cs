namespace EventHorizon.Game.Editor.Automation.ArtifactManagement.Pages;

using Atata;

using EventHorizon.Game.Editor.Automation.Layout;

using _ = ArtifactManagementPage;

[Url("/artifact/management")]
public class ArtifactManagementPage
    : MainLayoutPage<_>
{
    public H1<_> Header { get; private set; }

    [FindByClass("page__description")]
    public Text<_> Description { get; private set; }

    public Link<_> BackupArtifactsLink { get; private set; }
    public Link<_> ExportArtifactsLink { get; private set; }
    public Link<_> ImportArtifactsLink { get; private set; }
}
