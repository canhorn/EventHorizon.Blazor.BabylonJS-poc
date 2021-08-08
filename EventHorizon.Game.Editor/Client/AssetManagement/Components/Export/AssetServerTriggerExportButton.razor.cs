namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Export
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Api;
    using EventHorizon.Game.Editor.Client.AssetManagement.Trigger;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using Microsoft.AspNetCore.Components;

    public class AssetServerTriggerExportButtonModel
        : EditorComponentBase
    {
        [CascadingParameter]
        public AssetManagementState State { get; set; } = null!;

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; } = null!;

        public async Task HandleTriggerExport()
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
    }
}
