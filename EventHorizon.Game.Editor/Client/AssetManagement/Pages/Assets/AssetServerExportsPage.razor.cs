﻿namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages.Assets;

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

public class AssetServerExportsPageBase
    : ObservableComponentBase,
      ConnectedToAssetServerAdminObserver,
      AssetServerExportFinishedEventObserver,
      AssetServerExportUploadedEventObserver
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

    public async Task Handle(AssetServerExportFinishedEvent args)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(AssetServerExportUploadedEvent args)
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

        ArtifactList = result.Result.ExportList
            .OrderBy(export => export.Path)
            .Reverse()
            .Select(
                export =>
                    new AssetServerArtifactViewModel
                    {
                        Service = export.Service,
                        ReferenceId = export.ReferenceId,
                        CreatedDate = export.Created,
                        Path = $"{Settings.AssetServer}{export.Path}",
                    }
            );
    }
}
