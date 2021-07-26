namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.EditForm
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
    using EventHorizon.Game.Editor.Client.Shared.Components.Select;
    using EventHorizon.Zone.Systems.ClientAssets.Model;
    using Microsoft.AspNetCore.Components;

    public class EditZoneGameAssetFormModel
        : EditorComponentBase
    {
        [Parameter]
        public ClientAsset Model { get; set; } = null!;
        [Parameter]
        public EventCallback<ClientAsset> OnSubmit { get; set; }

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

            AssetTypeOption = AssetTypeOptions.FirstOrDefault(
                a => a.Value == Model.Type
            ) ?? AssetTypeOptions.First();

            MessageState = ComponentState.Content;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            AssetTypeOption = AssetTypeOptions.FirstOrDefault(
                a => a.Value == Model.Type
            ) ?? AssetTypeOptions.First();

            MessageState = ComponentState.Content;
        }

        protected void HandleAssetTypeChanged(
            StandardSelectOption option
        )
        {
            AssetTypeOption = option;
            Model.Type = option.Value;
        }

        protected async Task HandleSubmit()
        {
            await OnSubmit.InvokeAsync(
                Model
            );
        }
    }
}
