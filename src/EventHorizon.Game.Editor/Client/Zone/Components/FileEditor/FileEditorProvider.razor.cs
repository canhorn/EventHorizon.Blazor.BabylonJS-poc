namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
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

        [MaybeNull]
        public EditorFile EditorFile { get; set; }
        [MaybeNull]
        public EditorNode EditorNode { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await Setup();
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            await Setup();
            await base.OnParametersSetAsync();
        }

        private async Task Setup()
        {
            ErrorMessage = string.Empty;
            EditorNode = ZoneState.EditorState.GetNode(
                EncodedFileNodeId.Base64Decode()
            );
            if (EditorNode.IsNull())
            {
                ErrorMessage = Localizer["File was not found."];
                return;
            }
            var result = await Mediator.Send(
                new QueryForEditorFile(
                    EditorNode.Path,
                    EditorNode.Name
                )
            );
            if (result.Success.IsNotTrue())
            {
                ErrorMessage = Localizer[
                    "Editor File Content retrieval failed with Error Code: {0}",
                    result.ErrorCode
                ];
                return;
            }
            EditorFile = result.Result;
        }
    }
}
