namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Add
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using MediatR;

    public class AddServerModuleScriptToStateHandler
        : INotificationHandler<AddServerModuleScript>
    {
        private readonly IIndexPool _indexPool;
        private readonly IServerModuleState _state;
        private readonly IRegisterInitializable _registerInitializable;
        private readonly IRegisterUpdatable _registerUpdatable;

        public AddServerModuleScriptToStateHandler(
            IIndexPool indexPool,
            IServerModuleState state,
            IRegisterInitializable registerInitializable,
            IRegisterUpdatable registerUpdatable
        )
        {
            _indexPool = indexPool;
            _state = state;
            _registerInitializable = registerInitializable;
            _registerUpdatable = registerUpdatable;
        }

        public async Task Handle(
            AddServerModuleScript notification, 
            CancellationToken cancellationToken
        )
        {
            var serverModule = new StandardServerModule(
                _indexPool.NextIndex(),
                notification.Scripts
            );
            await _registerInitializable.Register(
                serverModule
            );
            await _registerUpdatable.Register(
                serverModule
            );
            _state.Set(
                serverModule
            );
        }
    }
}
