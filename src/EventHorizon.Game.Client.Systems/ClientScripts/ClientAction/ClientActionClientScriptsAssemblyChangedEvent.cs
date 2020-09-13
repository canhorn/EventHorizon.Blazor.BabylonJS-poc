namespace EventHorizon.Game.Client.Systems.ClientScripts.Scripting.ClientAction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    [ClientAction("CLIENT_SCRIPTS_ASSEMBLY_CHANGED_CLIENT_ACTION_EVENT")]
    public struct ClientActionClientScriptsAssemblyChangedEvent
        : IClientAction
    {
        public string Hash { get; }
        public string ScriptAssembly { get; }

        public ClientActionClientScriptsAssemblyChangedEvent(
            IClientActionDataResolver resolver
        )
        {
            Hash = resolver.Resolve<string>("hash");
            ScriptAssembly = resolver.Resolve<string>("scriptAssembly");
        }
    }

    public interface ClientActionClientScriptsAssemblyChangedEventObserver
        : ArgumentObserver<ClientActionClientScriptsAssemblyChangedEvent>
    {
    }

    public class ClientActionClientScriptsAssemblyChangedEventObserverHandler
        : INotificationHandler<ClientActionClientScriptsAssemblyChangedEvent>
    {
        private readonly ObserverState _observer;

        public ClientActionClientScriptsAssemblyChangedEventObserverHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            ClientActionClientScriptsAssemblyChangedEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<ClientActionClientScriptsAssemblyChangedEventObserver, ClientActionClientScriptsAssemblyChangedEvent>(
            notification,
            cancellationToken
        );
    }
}
