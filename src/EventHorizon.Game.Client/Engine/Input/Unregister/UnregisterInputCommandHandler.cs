namespace EventHorizon.Game.Client.Engine.Input.Unregister
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using MediatR;

    public class UnregisterInputCommandHandler
        : IRequestHandler<UnregisterInputCommand>
    {
        private readonly IUnregisterInput _unregister;

        public UnregisterInputCommandHandler(
            IUnregisterInput unregister
        )
        {
            _unregister = unregister;
        }

        public Task<Unit> Handle(
            UnregisterInputCommand request,
            CancellationToken cancellationToken
        )
        {
            _unregister.Unregister(
                request.Handle
            );

            return Unit.Task;
        }
    }
}
