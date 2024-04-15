namespace EventHorizon.Game.Editor.Client.Shared.Properties;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Zone.Services.Model;
using EventHorizon.Game.Server.Asset.Query;
using Microsoft.AspNetCore.Components;

public class AssetServerPathPropertyControlModel : PropertyControlBase
{
    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    protected ZoneInfo ZoneInfo => ZoneState.ZoneInfo;

    protected string AssetServerFullName = string.Empty;

    protected StandardSelectOption? SelectedAssetOption { get; private set; }
    protected List<StandardSelectOption> AssetOptions { get; private set; } =
        new List<StandardSelectOption>();

    protected override object Parse(object value)
    {
        AssetServerFullName = value.To<string>() ?? string.Empty;
        SetSelectedAssetOption();
        return value.To<string>() ?? string.Empty;
    }

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

    private async Task Setup()
    {
        var result = await Mediator.Send(new QueryForFileManagementAssets());
        if (!result)
        {
            return;
        }

        var options = result
            .Result.PathList.Select(clientAsset => new StandardSelectOption
            {
                Value = clientAsset.FullName,
                Text = clientAsset.FullName,
            })
            .OrderBy(a => a.Text)
            .InsertItem(
                0,
                new StandardSelectOption
                {
                    Value = "none",
                    Text = Localizer["Select an Asset Server Path..."],
                    Disabled = true,
                    Hidden = true,
                }
            )
            .ToList();
        AssetServerFullName = Property.ToString() ?? string.Empty;
        AssetOptions = options;
        SetSelectedAssetOption();
    }

    private void SetSelectedAssetOption()
    {
        if (AssetOptions.Exists(a => a.Value == AssetServerFullName))
        {
            SelectedAssetOption = AssetOptions.First(a => a.Value == AssetServerFullName);
        }
        else
        {
            SelectedAssetOption = AssetOptions.First();
        }
    }

    public async Task HandleAssetValueChanged(StandardSelectOption option)
    {
        option.NullCheck(nameof(option));

        SelectedAssetOption = option;

        var fullName = option.Value;

        await HandleChange(new ChangeEventArgs { Value = fullName });
    }
}
