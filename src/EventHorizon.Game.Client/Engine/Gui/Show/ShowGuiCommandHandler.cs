namespace EventHorizon.Game.Client.Engine.Gui.Show
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public class ShowGuiCommandHandler
        : IRequestHandler<ShowGuiCommand, StandardCommandResult>
    {
        private readonly IGuiDefinitionState _state;

        public ShowGuiCommandHandler(
            IGuiDefinitionState state
        )
        {
            _state = state;
        }

        public Task<StandardCommandResult> Handle(
            ShowGuiCommand request, 
            CancellationToken cancellationToken
        )
        {
            var gui = _state.Get(
                request.Id
            );
            if (gui.HasValue)
            {
                gui.Value.Hide();
            }

            return new StandardCommandResult()
                .FromResult();
        }
    }
}
