namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Api;
    using EventHorizon.Game.Editor.Client.AssetManagement.Open;
    using EventHorizon.Game.Editor.Client.AssetManagement.Reload;
    using EventHorizon.Game.Editor.Client.Authentication.Model;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;

    public class AssetImportFileUploadProviderModel
        : ObservableComponentBase,
        OpenAssetServerImportFileUploaderEventObserver
    {
        [CascadingParameter]
        public AccessTokenModel AccessToken { get; set; } = null!;

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;
        [Inject]
        public AssetServerService AssetServerService { get; set; } = null!;


        public string UploadFileId { get; } = "asset-server-import-upload-input-file";
        public IJSObjectReference FileUploadClickModule { get; private set; } = null!;
        public InputFile UploadInputFile { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            FileUploadClickModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./js/FileUploadClick.js"
            );
        }

        public async Task TriggerOpenForFileUpload()
        {
            await FileUploadClickModule.InvokeVoidAsync(
                "openInputElement",
                UploadFileId
            );
        }

        public async Task Handle(
            OpenAssetServerImportFileUploaderEvent _
        )
        {
            await TriggerOpenForFileUpload();
        }

        protected async Task HandleInputFileChange(
            InputFileChangeEventArgs args
        )
        {
            await UploadFrom(
                args.File
            );
        }

        private async Task UploadFrom(
            IBrowserFile file
        )
        {
            if (AccessToken.IsFilled.IsNotTrue())
            {
                return;
            }

            var result = await AssetServerService.Upload(
                AccessToken.AccessToken,
                file,
                System.Threading.CancellationToken.None
            );

            if (result.Success)
            {
                await Mediator.Publish(
                    new ForceReloadAssetManagementStateEvent()
                );

                await ShowMessage(
                    Localizer["Asset Server Import"],
                    Localizer["Successfully Import new Assets."]
                );
                return;
            }

            await ShowMessage(
                Localizer["Asset Server Import"],
                Localizer[
                    "Failed to Import Assets: ErrorCode = {0}",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await FileUploadClickModule.DisposeAsync();
        }
    }
}
