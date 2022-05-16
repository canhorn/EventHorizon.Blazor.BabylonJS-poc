namespace EventHorizon.Game.Editor.Client.DataStorage.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Client.DataStorage.Components.Modal;
    using EventHorizon.Game.Editor.Client.DataStorage.Model;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Components.Select;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using EventHorizon.Game.Editor.Zone.Services.Connection;
    using EventHorizon.Zone.Systems.DataStorage.Create;
    using EventHorizon.Zone.Systems.DataStorage.Delete;
    using EventHorizon.Zone.Systems.DataStorage.Query;
    using EventHorizon.Zone.Systems.DataStorage.Update;

    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;

    public class DataStoragePageModel
        : ObservableComponentBase,
          ZoneAdminServiceConnectedEventObserver
    {
        [Inject]
        public ILogger<DataStoragePageModel> Logger { get; set; } = null!;

        public IDictionary<string, object> DataValues { get; private set; } =
            new Dictionary<string, object>();
        public DataStorePropertiesMetadata DataStoreMetadata
        {
            get;
            private set;
        } = new DataStorePropertiesMetadata();

        public bool IsEditOpen { get; set; }
        public DataValueEditModalModel EditModalModel { get; private set; } =
            new(
                string.Empty,
                string.Empty,
                ZoneEditorPropertyType.PropertyString
            );

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var propertyTypeOptions = new List<StandardSelectOption>
            {
                new StandardSelectOption
                {
                    Text = Localizer[ZoneEditorPropertyType.PropertyBoolean],
                    Value = ZoneEditorPropertyType.PropertyBoolean,
                },
                new StandardSelectOption
                {
                    Text = Localizer[ZoneEditorPropertyType.PropertyDecimal],
                    Value = ZoneEditorPropertyType.PropertyDecimal,
                },
                new StandardSelectOption
                {
                    Text = Localizer[ZoneEditorPropertyType.PropertyLong],
                    Value = ZoneEditorPropertyType.PropertyLong,
                },
                new StandardSelectOption
                {
                    Text = Localizer[ZoneEditorPropertyType.PropertyString],
                    Value = ZoneEditorPropertyType.PropertyString,
                },
                new StandardSelectOption
                {
                    Text = Localizer[ZoneEditorPropertyType.PropertyComplex],
                    Value = ZoneEditorPropertyType.PropertyComplex,
                },
            };
            propertyTypeOptions = propertyTypeOptions
                .OrderBy(option => option.Text)
                .ToList();
            propertyTypeOptions.Insert(
                0,
                new StandardSelectOption
                {
                    Text = Localizer["Select a Type..."],
                    Value = string.Empty,
                    Disabled = true,
                }
            );

            EditModalModel = EditModalModel.UpdateOptions(
                propertyTypeOptions,
                propertyTypeOptions.First()
            );

            await Setup();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            await Setup();
        }

        public async Task Handle(ZoneAdminServiceConnectedEvent _)
        {
            await Setup();

            await InvokeAsync(StateHasChanged);
        }

        private async Task Setup()
        {
            var dataValuesResult = await Mediator.Send(
                new QueryForAllDataStoreValues()
            );
            if (!dataValuesResult)
            {
                await Mediator.Publish(
                    new ShowMessageEvent(
                        Localizer["Data Value Error"],
                        Localizer[
                            "Failed with error code: {0}",
                            dataValuesResult.ErrorCode
                        ]
                    )
                );
                return;
            }

            DataValues = dataValuesResult.Result;

            if (
                DataValues.TryGetValue(
                    DataStorePropertiesMetadata.DATA_STORE_SCHEMA_KEY,
                    out var metadataObj
                )
            )
            {
                var metadata = metadataObj.To<Dictionary<string, string>>();

                DataStoreMetadata = new DataStorePropertiesMetadata(metadata);
            }
        }

        public static bool FilterDataValues(
            KeyValuePair<string, object> dataValue
        ) => dataValue.Key != DataStorePropertiesMetadata.DATA_STORE_SCHEMA_KEY;

        public void HandleNewDataValueClicked()
        {
            EditModalModel = EditModalModel.Reset(
                isNew: true,
                string.Empty,
                string.Empty,
                ZoneEditorPropertyType.PropertyString
            );
            IsEditOpen = true;
        }

        public async Task HandleRefreshClicked()
        {
            await Setup();
        }

        public void HandleEditDataValue(string name, object value)
        {
            EditModalModel = EditModalModel.Reset(
                isNew: false,
                name,
                value,
                DataStoreMetadata.GetPropertyType(name, value)
            );
            IsEditOpen = true;
        }

        public async Task HandleDataValueEditSubmit(
            DataValueModalSubmitArgs args
        )
        {
            EditModalModel = args.Model;

            switch (args.Type)
            {
                case DataValueModalSubmitType.Override:
                case DataValueModalSubmitType.Update:
                    var updateResult = await Mediator.Send(
                        new UpdateDataStoreValueCommand(
                            args.Model.DataName,
                            args.Model.DataType,
                            args.Model.DataValue
                        )
                    );
                    if (!updateResult.Success)
                    {
                        EditModalModel = EditModalModel.UpdateErrorCode(
                            updateResult.ErrorCode
                        );
                        return;
                    }
                    IsEditOpen = false;
                    await Setup();
                    break;
                case DataValueModalSubmitType.Clone:
                case DataValueModalSubmitType.Create:
                    var createResult = await Mediator.Send(
                        new CreateDataStoreValueCommand(
                            args.Model.DataName,
                            args.Model.DataType,
                            args.Model.DataValue
                        )
                    );
                    if (!createResult.Success)
                    {
                        EditModalModel = EditModalModel.UpdateErrorCode(
                            createResult.ErrorCode
                        );
                        return;
                    }
                    IsEditOpen = false;
                    await Setup();
                    break;
            }
        }

        public bool IsDeleteConfirmOpen { get; private set; } = false;
        private string _dataValueKeyToDelete = string.Empty;

        public void HandleDeleteDataValue(string dataValueKey)
        {
            _dataValueKeyToDelete = dataValueKey;
            IsDeleteConfirmOpen = true;
        }

        public void HandleCloseDeletePrompt()
        {
            IsDeleteConfirmOpen = false;
        }

        public async Task HandleYesDelete()
        {
            var deleteResult = await Mediator.Send(
                new DeleteDataStoreValueCommand(_dataValueKeyToDelete)
            );

            if (!deleteResult.Success)
            {
                EditModalModel = EditModalModel.UpdateErrorCode(
                    deleteResult.ErrorCode
                );
                return;
            }

            IsDeleteConfirmOpen = false;
            await Setup();
        }
    }
}
