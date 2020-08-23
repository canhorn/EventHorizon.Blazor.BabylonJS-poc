namespace EventHorizon.Game.Client.Engine.Gui.Dispose
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Unregister;
    using MediatR;

    public class DisposeOfGuiCommandHandler
        : IRequestHandler<DisposeOfGuiCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IGuiDefinitionState _state;

        public DisposeOfGuiCommandHandler(
            IMediator mediator,
            IGuiDefinitionState state
        )
        {
            _mediator = mediator;
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            DisposeOfGuiCommand request,
            CancellationToken cancellationToken
        )
        {
            var gui = _state.Get(
                request.GuiId
            );
            if (gui.HasValue)
            {
                await _mediator.Send(
                    new DisposeOfEntityCommand(
                        gui.Value
                    )
                );
                _state.Remove(
                    gui.Value.GuiId
                );
            }

            return new StandardCommandResult();
        }
    }
}
