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
                    Value = "SCRIPT:JavaScript",
                    Text = Localizer["JavaScript Script (Compiled)"],
                },
                new StandardSelectOption
                {
                    Value = "MESH:BOX",
                    Text = Localizer["Mesh Box"],
                },
                new StandardSelectOption
                {
                    Value = "MESH:SPHERE",
                    Text = Localizer["Mesh Sphere"],
                },
                new StandardSelectOption
                {
                    Value = "MESH:GLTF",
                    Text = Localizer["Mesh glTF"],
                },
                new StandardSelectOption
                {
                    Value = "MESH:MAP",
                    Text = Localizer["Map Mesh"],
                },
                new StandardSelectOption
                {
                    Value = "MATERIAL:MAP",
                    Text = Localizer["Map Material"],
                },
                new StandardSelectOption
                {
                    Value = "IMAGE:URL",
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
