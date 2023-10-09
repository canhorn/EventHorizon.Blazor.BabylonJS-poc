namespace EventHorizon.Game.Editor.Client.DataStorage.Components.Modal;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.DataStorage.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Components.Select;
using EventHorizon.Game.Editor.Client.Shared.Properties;

using Microsoft.AspNetCore.Components;

public class DataValueEditModalBase : EditorComponentBase
{
    [Parameter]
    public bool IsOpen { get; set; }

    [Parameter]
    public DataStorePropertiesMetadata DataStoreMetadata { get; set; } = null!;

    [Parameter]
    public DataValueEditModalModel Model { get; set; }

    [Parameter]
    public Func<string, bool> ContainsName { get; set; } = _ => false;

    [Parameter]
    public EventCallback OnClose { get; set; }

    [Parameter]
    public EventCallback<DataValueModalSubmitArgs> OnSubmit { get; set; }

    public ElementReference DataNameInput { get; set; }

    public DataValueEditModalModel EditingModel { get; private set; } =
        new DataValueEditModalModel();

    public ComponentState MessageState { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public ComponentState DisplayPropertyEditState { get; private set; }
    public string DataTypeCssModifier { get; private set; } = string.Empty;
    public string DataTypeTitle { get; private set; } = string.Empty;
    public bool DataTypeDisabled { get; private set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        EditingModel = Model;

        CheckMessageState();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (EditingModel.Equals(Model).IsNotTrue())
        {
            EditingModel = Model;
        }

        CheckMessageState();
    }

    public async Task HandleShown()
    {
        await DataNameInput.FocusAsync();
    }

    public void HandleDataNameChanged(ChangeEventArgs args)
    {
        EditingModel = EditingModel.UpdateName(
            args.Value?.ToString() ?? string.Empty
        );

        CheckMessageState();
    }

    public void HandleValueTypeChanged(StandardSelectOption option)
    {
        var dataType = option.Value;
        EditingModel = EditingModel.ChangeValueType(
            option.Value,
            DataStoreMetadata.GetDefaultValueForPropertyType(dataType)
        );

        CheckMessageState();
    }

    public void HandlePropertyChanged(PropertyChangedArgs args)
    {
        EditingModel = EditingModel.UpdateValue(
            args.PropertyName,
            args.Property
        );

        CheckMessageState();
    }

    public async Task HandleSubmit(DataValueModalSubmitType type)
    {
        await OnSubmit.InvokeAsync(
            new DataValueModalSubmitArgs(type, EditingModel)
        );
    }

    private void CheckMessageState()
    {
        MessageState = ComponentState.Content;
        if (EditingModel.MessageCode == DataValueMessageCodes.Loading)
        {
            MessageState = ComponentState.Loading;
        }
        else if (!string.IsNullOrWhiteSpace(EditingModel.MessageCode))
        {
            MessageState = ComponentState.Error;
        }

        Message = Localizer["Valid Data Value"];
        DataTypeDisabled = false;
        DataTypeCssModifier = string.Empty;
        DataTypeTitle = Localizer["Select a Type for your Data Value."];
        DisplayPropertyEditState = ComponentState.Content;
        switch (EditingModel.MessageCode)
        {
            case DataValueMessageCodes.Loading:
                Message = Localizer["Loading..."];
                break;
            case DataValueErrorCodes.InvalidDataName:
                Message = Localizer["A Name is required for the Data Value."];
                DataTypeCssModifier = "--disabled";
                DataTypeTitle = Localizer[
                    "Please correct the Data Value Name."
                ];
                DataTypeDisabled = true;
                DisplayPropertyEditState = ComponentState.Error;
                break;
            default:
                if (!string.IsNullOrWhiteSpace(EditingModel.MessageCode))
                {
                    Message = Localizer[
                        "Received Message Code of '{0}'",
                        EditingModel.MessageCode
                    ];
                    DataTypeCssModifier = "--disabled";
                    DataTypeTitle = Localizer[
                        "Please check the Message Code for more details."
                    ];
                    DataTypeDisabled = true;
                    DisplayPropertyEditState = ComponentState.Error;
                }
                break;
        }
    }
}
