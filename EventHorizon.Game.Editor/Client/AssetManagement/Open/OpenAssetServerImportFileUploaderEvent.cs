namespace EventHorizon.Game.Editor.Client.AssetManagement.Open;

using EventHorizon.Observer.Model;

using MediatR;

public struct OpenAssetServerImportFileUploaderEvent : INotification { }

public interface OpenAssetServerImportFileUploaderEventObserver
    : ArgumentObserver<OpenAssetServerImportFileUploaderEvent> { }
