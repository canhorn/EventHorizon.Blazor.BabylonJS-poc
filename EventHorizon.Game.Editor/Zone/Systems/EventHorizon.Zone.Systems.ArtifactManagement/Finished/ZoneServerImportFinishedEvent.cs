namespace EventHorizon.Zone.Systems.ArtifactManagement.Finished;

using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;

[AdminClientAction("ZONE_SERVER_IMPORT_FINISHED_ADMIN_CLIENT_ACTION")]
public struct ZoneServerImportFinishedEvent : IAdminClientAction
{
    public string ReferenceId { get; }

    public ZoneServerImportFinishedEvent(
        IAdminClientActionDataResolver resolver
    )
    {
        ReferenceId = resolver.Resolve<string>("referenceId");
    }
}

public interface ZoneServerImportFinishedEventObserver
    : ArgumentObserver<ZoneServerImportFinishedEvent> { }
