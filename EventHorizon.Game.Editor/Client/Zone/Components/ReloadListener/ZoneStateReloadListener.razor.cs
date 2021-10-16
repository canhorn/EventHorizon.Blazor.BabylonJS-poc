namespace EventHorizon.Game.Editor.Client.Zone.Components.ReloadListener
{
    using System.Threading.Tasks;

    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.ClientActions.Reload;
    using EventHorizon.Game.Editor.Client.Zone.Reload;

    public class ZoneStateReloadListenerBase
        : ObservableComponentBase,
        EditorSystemReloadedAdminClientActionObserver,
        ServerScriptsSystemReloadedAdminClientActionObserver
    {
        public async Task Handle(
            EditorSystemReloadedAdminClientAction args
        )
        {
            await Mediator.Send(
                new SetReloadOnZoneStateCommand(
                    true
                )
            );
        }

        public async Task Handle(
            ServerScriptsSystemReloadedAdminClientAction args
        )
        {
            await Mediator.Send(
                new SetReloadOnZoneStateCommand(
                    true
                )
            );
        }
    }
}
