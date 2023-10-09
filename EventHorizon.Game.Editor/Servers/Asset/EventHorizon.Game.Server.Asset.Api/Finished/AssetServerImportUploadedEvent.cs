namespace EventHorizon.Game.Server.Asset.Finished;

using EventHorizon.Observer.Model;

using MediatR;

public struct AssetServerImportUploadedEvent : INotification
{
    public string Service { get; }
    public string ImportPath { get; }

    public AssetServerImportUploadedEvent(string referenceId, string importPath)
    {
        Service = referenceId;
        ImportPath = importPath;
    }
}

public interface AssetServerImportUploadedEventObserver
    : ArgumentObserver<AssetServerImportUploadedEvent> { }
