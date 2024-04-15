namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Breadcrumb;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Load;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using Microsoft.AspNetCore.Components;

public class AssetManagementBreadcrumbModel : EditorComponentBase
{
    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;
    public string CurrentLocation { get; set; } = FileSystemDirectoryContent.PATH_SEPARATOR;
    public List<BreadcrumbItem> BreadcrumbItemList { get; private set; } =
        new List<BreadcrumbItem>();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Setup();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Setup();
    }

    protected async Task HandleNavigateToPath(string filterPath)
    {
        await Mediator.Send(new LoadAssetServerFilterPathCommand(filterPath));
    }

    private void Setup()
    {
        var path = State
            .CurrentWorkingDirectory.FilterPath.Split(
                FileSystemDirectoryContent.PATH_SEPARATOR,
                StringSplitOptions.RemoveEmptyEntries
            )
            .ToList();
        var currentLocation = State.CurrentWorkingDirectory.Name;

        if (State.CurrentWorkingDirectory.IsFile)
        {
            currentLocation = path.LastOrDefault() ?? string.Empty;
            path = path.Take(path.Count - 1).ToList();
        }

        var filterPath = string.Empty;
        var breadcrumbItemList = path.Select(a => new BreadcrumbItem
            {
                FilterPath = filterPath += $"/{a}",
                Text = a,
            })
            .ToList();

        if (
            currentLocation.IsNotNullOrEmpty()
            && currentLocation != FileSystemDirectoryContent.PATH_SEPARATOR
        )
        {
            CurrentLocation = Localizer["/ {0}", currentLocation];
            breadcrumbItemList.Insert(
                0,
                new BreadcrumbItem { FilterPath = State.RootPath, Text = Localizer["Server Root"], }
            );
        }
        else
        {
            CurrentLocation = Localizer["{0} Server Root", State.RootPath];
        }

        BreadcrumbItemList = breadcrumbItemList;
    }
}

public class BreadcrumbItem
{
    public string FilterPath { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
