namespace EventHorizon.Game.Client.Engine.Gui.Dispose
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public class DisposeOfGuiControlChildrenCommandHandler
        : IRequestHandler<DisposeOfGuiControlChildrenCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IGuiControlChildrenState _state;

        public DisposeOfGuiControlChildrenCommandHandler(
            IMediator mediator,
            IGuiControlChildrenState state
        )
        {
            _mediator = mediator;
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            DisposeOfGuiControlChildrenCommand request,
            CancellationToken cancellationToken
        )
        {
            var children = _state.GetChildren(
                request.ControlId
            );
            foreach (var childGuiId in children)
            {
                await _mediator.Send(
                    new DisposeOfGuiCommand(
                        childGuiId
                    ),
                    cancellationToken
                );
            }

            return new StandardCommandResult();
        }
    }
}
