namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages.Assets;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Pages.Assets.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Model;
using EventHorizon.Game.Server.Asset.Connection;
using EventHorizon.Game.Server.Asset.Finished;
using EventHorizon.Game.Server.Asset.Query;

using Microsoft.AspNetCore.Components;

public class AssetServerBackupsPageBase
    : ObservableComponentBase,
      ConnectedToAssetServerAdminObserver,
      AssetServerBackupFinishedEventObserver,
      AssetServerBackupUploadedEventObserver
{
    [Inject]
    public GamePlatformServiceSettings Settings { get; set; } = null!;

    public IEnumerable<AssetServerArtifactViewModel> ArtifactList { get; set; } =
        new List<AssetServerArtifactViewModel>();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Setup();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        await Setup();
    }

    public async Task Handle(ConnectedToAssetServerAdmin args)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerBackupFinishedEvent args)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerBackupUploadedEvent args)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    private async Task Setup()
    {
        var result = await Mediator.Send(
            new QueryForAssetServerArtifacts()
        );

        if (!result)
        {
            return;
        }

        ArtifactList = result.Result.BackupList
            .OrderBy(backup => backup.Path)
            .Reverse()
            .Select(
                backup =>
                    new AssetServerArtifactViewModel
                    {
                        Service = backup.Service,
                        ReferenceId = backup.ReferenceId,
                        CreatedDate = backup.Created,
                        Path = $"{Settings.AssetServer}{backup.Path}",
                    }
            );
    }
}
