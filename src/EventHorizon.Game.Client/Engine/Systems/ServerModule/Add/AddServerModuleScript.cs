namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Add
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ServerModule.Model;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct AddServerModuleScript : INotification
    {
        public ServerModuleScripts Scripts { get; }

        public AddServerModuleScript(
            ServerModuleScripts scripts
        )
        {
            Scripts = scripts;
        }
    }

    public interface AddServerModuleScriptObserver
        : ArgumentObserver<AddServerModuleScript>
    {
    }

    public class AddServerModuleScriptHandler
        : INotificationHandler<AddServerModuleScript>
    {
        private readonly ObserverState _observer;

        public AddServerModuleScriptHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            AddServerModuleScript notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<AddServerModuleScriptObserver, AddServerModuleScript>(
            notification,
            cancellationToken
        );
    }
}
