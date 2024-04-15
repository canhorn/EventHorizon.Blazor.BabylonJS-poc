namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.EditForm;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Shared.Properties;
using EventHorizon.Zone.Systems.ClientAssets.Model;
using EventHorizon.Zone.Systems.ClientAssets.Query;
using Microsoft.AspNetCore.Components;

public class EditZoneGameAssetFormModel : EditorComponentBase
{
    [Parameter]
    public ClientAsset Model { get; set; } = null!;

    [Parameter]
    public EventCallback<ClientAsset> OnSubmit { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    protected ComponentState MessageState { get; private set; } = ComponentState.Loading;
    protected string Message { get; private set; } = string.Empty;

    #region Client Asset Edit Fields
    protected string AssetName { get; set; } = string.Empty;
    protected StandardSelectOption AssetTypeOption { get; private set; } = new();
    protected IDictionary<string, object> TypeData { get; set; } = new Dictionary<string, object>();
    #endregion

    #region Edit Field Data
    protected List<StandardSelectOption> AssetTypeOptions { get; set; } = new();
    protected ComponentState TypeDataState { get; private set; } = ComponentState.Loading;
    protected ClientAssetPropertiesMetadata TypePropertiesMetadata { get; private set; } =
        new ClientAssetPropertiesMetadata();
    #endregion

    private IEnumerable<ClientAssetTypeDetails> _clientAssetTypeDetails =
        new List<ClientAssetTypeDetails>();

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

    protected void HandleAssetTypeChanged(StandardSelectOption option)
    {
        MessageState = ComponentState.Content;
        Message = string.Empty;
        AssetTypeOption = option;

        SetDataValue();
    }

    protected async Task HandleSubmit()
    {
        MessageState = ComponentState.Loading;
        Message = string.Empty;
        if (AssetName.IsNullOrEmpty())
        {
            Message = Localizer["Name is Required."];
            MessageState = ComponentState.Error;
            return;
        }
        else if (AssetTypeOption.Value.Equals(string.Empty))
        {
            Message = Localizer["A Type is required."];
            MessageState = ComponentState.Error;
            return;
        }

        Model.Name = AssetName;
        Model.Type = AssetTypeOption.Value;

        Model.Data = ClientAssetPropertiesMetadata
            .MergeMetadataInto(TypeData, Model.Data)
            .AsDictionary();

        await OnSubmit.InvokeAsync(Model);

        MessageState = ComponentState.Content;
    }

    protected async Task HandleCancelClicked()
    {
        await OnCancel.InvokeAsync();
    }

    protected void HandleResetClicked()
    {
        AssetName = Model.Name;
        AssetTypeOption =
            AssetTypeOptions.FirstOrDefault(a => a.Value == Model.Type) ?? AssetTypeOptions.First();

        SetDataValue(
            new Dictionary<string, object>(
                Model.Data.Where(ClientAssetPropertiesMetadata.FilterOutMetadata)
            )
        );
    }

    protected void HandleTypeDataChanged(PropertiesDisplayChangedArgs args)
    {
        MessageState = ComponentState.Content;
        Message = string.Empty;
        TypeData = new Dictionary<string, object>(args.Data);
    }

    private async Task Setup()
    {
        MessageState = ComponentState.Loading;

        var result = await Mediator.Send(new QueryForAllClientAssetTypeDetails());
        if (!result)
        {
            return;
        }

        _clientAssetTypeDetails = result.Result;
        AssetTypeOptions = result
            .Result.Select(details => new StandardSelectOption
            {
                Value = details.Type,
                Text = Localizer[details.Name],
            })
            .OrderBy(option => option.Text)
            .InsertItem(
                0,
                new StandardSelectOption
                {
                    Value = string.Empty,
                    Text = Localizer["Select a Type"],
                    Hidden = true,
                    Disabled = true,
                }
            )
            .ToList();

        AssetName = Model.Name;
        AssetTypeOption =
            AssetTypeOptions.FirstOrDefault(a => a.Value == Model.Type) ?? AssetTypeOptions.First();

        MessageState = ComponentState.Content;

        SetDataValue(
            new Dictionary<string, object>(
                Model.Data.Where(ClientAssetPropertiesMetadata.FilterOutMetadata)
            )
        );
    }

    private void SetDataValue(IDictionary<string, object>? modelData = null)
    {
        TypeDataState = ComponentState.Loading;
        TypePropertiesMetadata = new ClientAssetPropertiesMetadata();
        TypeData = new Dictionary<string, object>();

        if (AssetTypeOption.Value.Equals(string.Empty))
        {
            TypeDataState = ComponentState.Error;
            return;
        }

        var typeDetails = _clientAssetTypeDetails.FirstOrDefault(asset =>
            asset.Type.Equals(AssetTypeOption.Value)
        );

        if (typeDetails.IsNull())
        {
            TypeDataState = ComponentState.Error;
            return;
        }

        TypePropertiesMetadata = new ClientAssetPropertiesMetadata(typeDetails.Metadata);
        TypeData = new Dictionary<string, object>(modelData ?? typeDetails.DefaultValue());

        TypeDataState = ComponentState.Content;
    }
}
