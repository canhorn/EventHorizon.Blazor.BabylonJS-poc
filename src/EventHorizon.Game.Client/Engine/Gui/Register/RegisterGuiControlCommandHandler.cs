namespace EventHorizon.Game.Client.Engine.Gui.Register
{
    using System;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;

    public class RegisterGuiControlCommandHandler
        : IRequestHandler<RegisterGuiControlCommand, StandardCommandResult>
    {
        private readonly IGuiControlState _controlState;
        private readonly IGuiControlTemplateState _templateState;
        private readonly IGuiControlFactory _controlFactory;

        public RegisterGuiControlCommandHandler(
            IGuiControlState controlState,
            IGuiControlTemplateState templateState,
            IGuiControlFactory controlFactory
        )
        {
            _controlState = controlState;
            _templateState = templateState;
            _controlFactory = controlFactory;
        }

        public Task<StandardCommandResult> Handle(
            RegisterGuiControlCommand request,
            CancellationToken cancellationToken
        )
        {
            var storedControl = _controlState.Get(
                request.GuiId,
                request.Control.Id
            );
            if (storedControl.HasValue)
            {
                return new StandardCommandResult(
                    "gui_control_already_registered"
                ).FromResult();
            }
            var template = _templateState.Get(
                request.Control.TemplateId
            );
            if (!template.HasValue)
            {
                return new StandardCommandResult(
                    "gui_control_template_not_found"
                ).FromResult();
            }
            var builtControl = _controlFactory.Build(
                request.Control.Id,
                template.Value,
                request.Control.Options,
                request.Control.GridLocation
            );

            _controlState.Set(
                request.GuiId,
                builtControl
            );

            return new StandardCommandResult()
                .FromResult();
        }
    }
}
