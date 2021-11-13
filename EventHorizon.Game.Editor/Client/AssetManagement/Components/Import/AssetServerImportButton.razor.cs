namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Import
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Client.AssetManagement.Open;
    using EventHorizon.Game.Editor.Client.Shared.Components;

    using Microsoft.AspNetCore.Components;

    public class AssetServerImportButtonModel
        : EditorComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<
            string,
            object
        > Attributes { get; set; } = null!;

        public async Task HandleOpenFileUpload()
        {
            await Mediator.Publish(
                new OpenAssetServerImportFileUploaderEvent()
            );
        }
    }
}
