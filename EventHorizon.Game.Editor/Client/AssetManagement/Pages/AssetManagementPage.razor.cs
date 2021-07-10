namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Api;
    using EventHorizon.Game.Editor.Client.AssetManagement.Clicked;
    using EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers;
    using EventHorizon.Game.Editor.Client.AssetManagement.Delete;
    using EventHorizon.Game.Editor.Client.AssetManagement.Load;
    using EventHorizon.Game.Editor.Client.AssetManagement.Model;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Components.TreeView.Model;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;

    public class AssetManagementPageModel
        : EditorComponentBase
    {
        private const string RootPath = "/";

        [CascadingParameter]
        public AssetManagementState State { get; set; } = null!;

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;
        [Inject]
        public AssetFileManagement AssetFileManagement { get; set; } = null!;

        public string Message { get; set; } = string.Empty;
        

        public IJSObjectReference FileUploadClickModule { get; private set; } = null!;
        public InputFile UploadInputFile { get; set; } = null!;
        public TreeViewNodeData? FileUploadTreeViewNode { get; private set; }
        public FileSystemDirectoryContent? FileUploadWorkingDirectory { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            FileUploadClickModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "import",
                "./js/FileUploadClick.js"
            );
        }

        public async Task HandleDirectoryContentClicked(
            FileSystemDirectoryContent directoryContent
        )
        {
            await Mediator.Publish(
                new AssetFileDirectoryContentClickedEvent(
                    directoryContent
                )
            );
        }

        public async Task HandleFileNodeChanged(
            TreeViewNodeData node
        )
        {
            await Mediator.Publish(
                new AssetFileNodeClickedEvent(
                    node
                )
            );
        }

        public async Task HandleDeleteFileDirectoryContent(
            FileSystemDirectoryContent directoryContent
        )
        {
            await Mediator.Publish(
                new AssetFileDeleteTriggeredEvent(
                    directoryContent
                )
            );
        }


        //#region File Upload

        //private bool IgnoreInputFileClick = false;
        //public async Task TriggerOpenForFileUpload()
        //{
        //    IgnoreInputFileClick = true;
        //    await FileUploadClickModule.InvokeVoidAsync(
        //        "openInputElement",
        //        "upload-input-file"
        //    // UploadInputFile.Element
        //    );
        //    IgnoreInputFileClick = false;
        //}

        //public void HandleInputFileClicked()
        //{
        //    if (IgnoreInputFileClick)
        //    {
        //        return;
        //    }

        //    FileUploadTreeViewNode = State.CurrentTreeViewNode;
        //    FileUploadWorkingDirectory = State.CurrentWorkingDirectory;
        //}

        //protected async Task HandleInputFileChange(
        //    InputFileChangeEventArgs args
        //)
        //{
        //    if (FileUploadTreeViewNode is null
        //        || FileUploadWorkingDirectory is null)
        //    {
        //        Message = Localizer["Invalid File Upload"];
        //        return;
        //    }

        //    await UploadFrom(
        //        args.File,
        //        FileUploadTreeViewNode,
        //        FileUploadWorkingDirectory
        //    );

        //    FileUploadTreeViewNode = null;
        //    FileUploadWorkingDirectory = null;
        //}

        //private async Task UploadFrom(
        //    IBrowserFile file,
        //    TreeViewNodeData node,
        //    FileSystemDirectoryContent directoryContent
        //)
        //{
        //    var result = await AssetFileManagement.Upload(
        //        file,
        //        BuildPath(
        //            directoryContent
        //        ),
        //        CancellationToken.None
        //    );

        //    if (result.Success)
        //    {
        //        Message = Localizer["Successfully Uploaded"];
        //        await Mediator.Send(
        //            new AssetReloadToNodeAndDirectoryContentCommand(
        //                node,
        //                directoryContent
        //            )
        //        );
        //        return;
        //    }

        //    Message = Localizer[
        //        "Failed to Upload: Code = {0} | Message = '{1}'",
        //        result.Error?.Code ?? 500,
        //        result.Error?.Message ?? Localizer["Server Exception"]
        //    ];
        //}
        //public async ValueTask DisposeAsync()
        //{
        //    await FileUploadClickModule.DisposeAsync();
        //}
        //#endregion

        private static string BuildPath(
            FileSystemDirectoryContent directoryContent
        )
        {
            var filterPath = directoryContent.FilterPath;
            if (directoryContent.IsFile)
            {
                return filterPath;
            }

            var path = directoryContent.Name;
            if (path == RootPath)
            {
                return path;
            }
            if (filterPath == RootPath)
            {
                return $"{RootPath}{path}";
            }
            return string.Join(
                "/",
                filterPath,
                path
            );
        }

    }
}
