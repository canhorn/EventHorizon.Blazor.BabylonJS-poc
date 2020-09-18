namespace EventHorizon.Game.Client.Engine.Gui.Update
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public class UpdateGuiControlCommandHandler
        : IRequestHandler<UpdateGuiControlCommand, StandardCommandResult>
    {
        private readonly IGuiControlState _state;

        public UpdateGuiControlCommandHandler(
            IGuiControlState state
        )
        {
            _state = state;
        }

        public Task<StandardCommandResult> Handle(
            UpdateGuiControlCommand request,
            CancellationToken cancellationToken
        )
        {
            var controlResult = _state.Get(
                request.GuiId,
                request.Control.ControlId
            );
            if (!controlResult.HasValue)
            {
                return new StandardCommandResult(
                    "gui_control_was_not_found"
                ).FromResult();
            }
            var control = controlResult.Value;
            if (request.Control.IsVisible.HasValue)
            {
                control.IsVisible = request.Control.IsVisible.Value;
            }
            if (request.Control.Options != null)
            {
                control.Update(
                    request.Control.Options
                );
            }
            if (request.Control.LinkWith != null)
            {
                control.LinkWith(
                    request.Control.LinkWith
                );
            }

            return new StandardCommandResult()
                .FromResult();
        }
    }
}
