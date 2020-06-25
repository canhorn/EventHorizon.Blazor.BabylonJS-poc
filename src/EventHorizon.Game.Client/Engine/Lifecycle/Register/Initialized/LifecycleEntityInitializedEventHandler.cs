namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Initialized
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using MediatR;

    public class LifecycleEntityInitializedEventHandler
        : INotificationHandler<EntityInitializedEvent>
    {

        private readonly IRegisterUpdatable _registerUpdatable;
        private readonly IRegisterDrawable _registerDrawable;

        public LifecycleEntityInitializedEventHandler(
            IRegisterUpdatable registerUpdatable,
            IRegisterDrawable registerDrawable
        )
        {
            _registerUpdatable = registerUpdatable;
            _registerDrawable = registerDrawable;
        }

        public async Task Handle(
            EntityInitializedEvent notification,
            CancellationToken cancellationToken
        )
        {
            if (notification.Entity is LifecycleEntityBase entity)
            {
                await _registerUpdatable.Register(
                    entity
                );
                await _registerDrawable.Register(
                    entity
                );
            }
        }
    }
}
