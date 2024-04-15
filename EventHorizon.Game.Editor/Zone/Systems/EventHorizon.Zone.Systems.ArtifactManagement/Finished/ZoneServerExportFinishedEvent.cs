namespace EventHorizon.Zone.Systems.ArtifactManagement.Finished;

using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
using EventHorizon.Game.Editor.Zone.AdminClientAction.Attributes;
using EventHorizon.Observer.Model;

[AdminClientAction("ZONE_SERVER_EXPORT_FINISHED_ADMIN_CLIENT_ACTION")]
public struct ZoneServerExportFinishedEvent : IAdminClientAction
{
    public string ReferenceId { get; }
    public string ExportUrl { get; }

    public ZoneServerExportFinishedEvent(IAdminClientActionDataResolver resolver)
    {
        ReferenceId = resolver.Resolve<string>("referenceId");
        ExportUrl = resolver.Resolve<string>("exportUrl");
    }
}

public interface ZoneServerExportFinishedEventObserver
    : ArgumentObserver<ZoneServerExportFinishedEvent> { }
