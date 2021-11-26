namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Components;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.ArtifactManagement.Components.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Query;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

using Microsoft.AspNetCore.Components;

public class ArtifactBackupsTableBase
    : ArtifactComponentBase
{
    [Parameter]
    public string ServiceFilter { get; set; } = string.Empty;

    protected ComponentState DisplayState { get; set; } = ComponentState.Loading;
    protected IEnumerable<ArtifactViewModel> ArtifactList { get; set; } =
        new List<ArtifactViewModel>();

    protected override ArtifactType ArtifactType { get; } = ArtifactType.Backup;

    protected override async Task HandleChange()
    {
        if (!ArtifactList.Any())
        {
            DisplayState = ComponentState.Loading;
        }

        var result = await Sender.Send(
            new QueryForAllArtifactBackups()
        );

        if (!result)
        {
            await ShowMessage(
                Localizer["Artifact Backup"],
                Localizer["Failed to find any Backups."],
                MessageLevel.Error
            );
            DisplayState = ComponentState.Error;
            return;
        }

        ArtifactList = result.Result;
        if(ServiceFilter.IsNotNullOrEmpty())
        {
            ArtifactList = ArtifactList.Where(
                a => a.Service == ServiceFilter
            );
        }

        DisplayState = ComponentState.Content;
    }
}
