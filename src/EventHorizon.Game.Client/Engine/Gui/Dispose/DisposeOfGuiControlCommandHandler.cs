namespace EventHorizon.Game.Client.Engine.Gui.Dispose
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public class DisposeOfGuiControlCommandHandler
        : IRequestHandler<DisposeOfGuiControlCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IGuiControlState _controlState;

        public DisposeOfGuiControlCommandHandler(
            IMediator mediator,
            IGuiControlState controlState
        )
        {
            _mediator = mediator;
            _controlState = controlState;
        }

        public Task<StandardCommandResult> Handle(
            DisposeOfGuiControlCommand request,
            CancellationToken cancellationToken
        )
        {
            var control = _controlState.Get(
                request.GuiId,
                request.ControlId
            );
            if (control.HasValue)
            {
                _mediator.Send(
                    new DisposeOfGuiControlChildrenCommand(
                        control.Value.Id
                    )
                );
                control.Value.Dispose();
                _controlState.Remove(
                    request.GuiId,
                    control.Value.Id
                );
            }

            return new StandardCommandResult()
                .FromResult();
        }
    }
}
