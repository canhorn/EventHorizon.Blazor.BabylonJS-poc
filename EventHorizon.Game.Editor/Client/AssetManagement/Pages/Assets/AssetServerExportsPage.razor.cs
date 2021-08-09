namespace EventHorizon.Game.Editor.Client.AssetManagement.Pages.Assets
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Open;
    using EventHorizon.Game.Editor.Client.AssetManagement.Trigger;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Model;
    using EventHorizon.Game.Server.Asset.Connection;
    using EventHorizon.Game.Server.Asset.Finished;
    using EventHorizon.Game.Server.Asset.Query;
    using Microsoft.AspNetCore.Components;

    public class AssetServerExportsPageModel
        : ObservableComponentBase,
        ConnectedToAssetServerAdminObserver,
        AssetServerExportFinishedEventObserver
    {
        [Inject]
        public GamePlatformServiceSettings Settings { get; set; } = null!;

        public IEnumerable<ExportArtifactViewModel> ArtifactList { get; set; } = new List<ExportArtifactViewModel>();

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

        public async Task Handle(
            ConnectedToAssetServerAdmin args
        )
        {
            await Setup();
            await InvokeAsync(StateHasChanged);
        }

        public async Task Handle(
            AssetServerExportFinishedEvent args
        )
        {
            await Setup();
            await InvokeAsync(StateHasChanged);
        }

        public async Task HandleTriggerExportClicked()
        {
            var result = await Mediator.Send(
                new TriggerAssetServerExportCommand()
            );

            if (!result)
            {
                await ShowMessage(
                    Localizer["Asset Server Export"],
                    Localizer[
                        "Failed to Trigger Asset Server Export. ErrorCode = {0}",
                        result.ErrorCode
                    ],
                    MessageLevel.Error
                );
                return;
            }
        }

        public async Task HandleImportClicked()
        {
            await Mediator.Publish(
                new OpenAssetServerImportFileUploaderEvent()
            );
        }

        private async Task Setup()
        {
            var result = await Mediator.Send(
                new QueryForExportArtifactList()
            );

            string ReferenceIdFromPath(
                string path
            )
            {
                return Path.GetFileName(
                    path
                ).Split(
                    '.'
                ).Skip(
                    2
                ).FirstOrDefault() ?? "reference-id";
            }
            string DateTimeFromPath(
                string path
            )
            {
                var ticksAsString = Path.GetFileName(
                    path
                ).Split(
                    '.'
                ).Skip(
                    1
                ).FirstOrDefault() ?? "0";
                var found = long.TryParse(
                    ticksAsString,
                    out var ticks
                );
                if (!found)
                {
                    return DateTimeOffset.MinValue.ToString("G");
                }

                return new DateTimeOffset(
                    ticks,
                    DateTimeOffset.Now.Offset
                ).ToString();
            }

            if (result)
            {
                ArtifactList = result.Result.OrderBy(
                    export => export.Path
                ).Reverse().Select(
                    export => new ExportArtifactViewModel
                    {
                        ReferenceId = ReferenceIdFromPath(
                            export.Path
                        ),
                        FormattedCreatedDate = DateTimeFromPath(
                            export.Path
                        ),
                        Path = $"{Settings.AssetServer}{export.Path}",
                    }
                );
            }
        }

        public class ExportArtifactViewModel
        {
            public string ReferenceId { get; set; } = string.Empty;
            public string FormattedCreatedDate { get; set; } = string.Empty;
            public string Path { get; set; } = string.Empty;
        }
    }
}
