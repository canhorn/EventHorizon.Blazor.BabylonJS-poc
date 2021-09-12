namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor
{
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Client.Zone.Query;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Query;

    using MediatR;

    using Microsoft.AspNetCore.Components;

    public class FileEditorProviderModel
        : ComponentBase
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;

        [Parameter]
        public string EncodedFileNodeId { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        public IMediator Mediator { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public bool IsLoading { get; set; } = true;
        public EditorFile? EditorFile { get; set; }
        public EditorNode? EditorNode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

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
            IsLoading = true;
            ErrorMessage = string.Empty;
            EditorNode = null;
            EditorFile = null;
            var editorNodeResult = await Mediator.Send(
                new QueryForEditorNodeById(
                    EncodedFileNodeId.Base64Decode()
                )
            );
            if (!editorNodeResult
                && (editorNodeResult.ErrorCode != ZoneClientEditorErrorCodes.ZONE_STATE_PENDING_RELOAD
                    || editorNodeResult.ErrorCode != ZoneClientEditorErrorCodes.ZONE_STATE_IS_LOADING
                )
            )
            {
                return;
            }
            else if (!editorNodeResult)
            {
                ErrorMessage = Localizer["File was not found."];
                IsLoading = false;
                return;
            }

            EditorNode = editorNodeResult.Result;
            var result = await Mediator.Send(
                new QueryForEditorFile(
                    EditorNode.Path,
                    EditorNode.Name
                )
            );
            if (!result)
            {
                ErrorMessage = Localizer[
                    "Editor File Content retrieval failed with Error Code: {0}",
                    result.ErrorCode
                ];
                IsLoading = false;
                return;
            }
            EditorFile = result.Result;
            IsLoading = false;
        }
    }
}
