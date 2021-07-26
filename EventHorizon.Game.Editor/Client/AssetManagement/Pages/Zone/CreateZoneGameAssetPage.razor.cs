namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages.Zone
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
    using EventHorizon.Game.Editor.Client.Shared.Components.Select;
    using EventHorizon.Zone.Systems.ClientAssets.Create;
    using EventHorizon.Zone.Systems.ClientAssets.Model;
    using Microsoft.AspNetCore.Components;

    public class CreateZoneGameAssetPageModel
        : EditorComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        protected ClientAsset Model { get; private set; } = new ClientAsset();
        protected StandardSelectOption AssetTypeOption { get; private set; } = new();

        protected ComponentState MessageState { get; private set; } = ComponentState.Loading;
        protected string Message { get; private set; } = string.Empty;

        protected ElementReference AssetNameInput { get; set; }

        protected List<StandardSelectOption> AssetTypeOptions { get; set; } = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            AssetTypeOptions = new List<StandardSelectOption>
            {
                new StandardSelectOption
                {
                    Value = string.Empty,
                    Text = Localizer["Select a Type"],
                    Hidden = true,
                    Disabled = true,
                },
                new StandardSelectOption
                {
                    Value = "SCRIPT",
                    Text = Localizer["Script"],
                },
                new StandardSelectOption
                {
                    Value = "MESH",
                    Text = Localizer["Mesh"],
                },
                new StandardSelectOption
                {
                    Value = "IMAGE",
                    Text = Localizer["Image"],
                },
                new StandardSelectOption
                {
                    Value = "DIALOG",
                    Text = Localizer["Dialog"],
                },
            };

            AssetTypeOption = AssetTypeOptions.First();

            MessageState = ComponentState.Content;
        }

        protected void HandleAssetTypeChanged(
            StandardSelectOption option
        )
        {
            AssetTypeOption = option;
            Model.Type = option.Value;
            // TODO: Default Data based on Type selected
        }

        protected async Task HandleSave()
        {
            MessageState = ComponentState.Loading;
            var result = await Mediator.Send(
                new CreateClientAssetCommand(
                    Model
                )
            );
            if (!result)
            {
                Message = Localizer[
                    "Failed to Create Game Asset: {0}",
                    result.ErrorCode 
                        ?? "ERROR"
                ];
                MessageState = ComponentState.Error;
                return;
            }

            await ShowMessage(
                Localizer["Game Asset Update"],
                Localizer["Successfully created Game Asset, navigating back to Asset List Page."]
            );
            NavigationManager.NavigateTo(
                "/asset/management/zone"
            );
        }
    }
}
