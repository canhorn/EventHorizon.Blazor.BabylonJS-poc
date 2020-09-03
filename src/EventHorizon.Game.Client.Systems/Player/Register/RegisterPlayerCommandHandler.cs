namespace EventHorizon.Game.Client.Systems.Player.Register
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;
    using EventHorizon.Game.Client.Systems.Player.Api;
    using EventHorizon.Game.Client.Systems.Player.Changed;
    using EventHorizon.Game.Client.Systems.Player.Model;
    using MediatR;

    public class RegisterPlayerCommandHandler
        : IRequestHandler<RegisterPlayerCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IPlayerState _state;

        public RegisterPlayerCommandHandler(
            IMediator mediator,
            IPlayerState playerState
        )
        {
            _mediator = mediator;
            _state = playerState;
        }

        public async Task<StandardCommandResult> Handle(
            RegisterPlayerCommand request,
            CancellationToken cancellationToken
        )
        {
            if (_state.Player.HasValue)
            {
                await _mediator.Send(
                    new DisposeOfEntityCommand(
                        _state.Player.Value
                    )
                );
            }
            var player = new StandardPlayerEntity(
                request.Player
            );

            await _mediator.Publish(
                new RegisterEntityEvent(
                    player
                ),
                cancellationToken
            );

            _state.Set(
                player
            );

            await _mediator.Publish(
                new PlayerDetailsChangedEvent()
            );

            return new StandardCommandResult();
        }
    }
}
