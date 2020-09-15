namespace EventHorizon.Game.Server.Game.Set
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Game.Api;
    using EventHorizon.Game.Server.Game.Updated;
    using MediatR;

    public class SetGameStateCommandHandler
        : IRequestHandler<SetGameStateCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly ServerGameState _state;

        public SetGameStateCommandHandler(
            IMediator mediator,
            ServerGameState state
        )
        {
            _mediator = mediator;
            _state = state;
        }
        public async Task<StandardCommandResult> Handle(
            SetGameStateCommand request,
            CancellationToken cancellationToken
        )
        {
            _state.Set(
                request.GameState
            );
            await _mediator.Publish(
                new GameStateUpdatedEvent(),
                cancellationToken
            );

            return new StandardCommandResult();
        }
    }
}
