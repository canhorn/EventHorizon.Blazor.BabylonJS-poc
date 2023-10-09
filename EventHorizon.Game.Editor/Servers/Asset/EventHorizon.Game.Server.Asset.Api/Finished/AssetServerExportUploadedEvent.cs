namespace EventHorizon.Game.Server.Asset.Finished;

using EventHorizon.Observer.Model;

using MediatR;

public struct AssetServerExportUploadedEvent : INotification
{
    public string Service { get; }
    public string ExportPath { get; }

    public AssetServerExportUploadedEvent(string service, string exportPath)
    {
        Service = service;
        ExportPath = exportPath;
    }
}

public interface AssetServerExportUploadedEventObserver
    : ArgumentObserver<AssetServerExportUploadedEvent> { }
