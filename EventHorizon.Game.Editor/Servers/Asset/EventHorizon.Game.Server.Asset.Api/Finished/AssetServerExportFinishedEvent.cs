namespace EventHorizon.Game.Server.Asset.Finished
{
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct AssetServerExportFinishedEvent
        : INotification
    {
        public string ReferenceId { get; }
        public string ExportPath { get; }

        public AssetServerExportFinishedEvent(
            string referenceId,
            string exportPath
        )
        {
            ReferenceId = referenceId;
            ExportPath = exportPath;
        }
    }

    public interface AssetServerExportFinishedEventObserver
        : ArgumentObserver<AssetServerExportFinishedEvent>
    {
    }
}
