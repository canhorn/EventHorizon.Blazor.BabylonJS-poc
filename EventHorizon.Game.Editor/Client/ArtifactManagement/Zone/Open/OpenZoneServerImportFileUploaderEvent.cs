namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Open;

using EventHorizon.Observer.Model;
using MediatR;

public struct OpenZoneServerImportFileUploaderEvent : INotification { }

public interface OpenZoneServerImportFileUploaderEventObserver
    : ArgumentObserver<OpenZoneServerImportFileUploaderEvent> { }
