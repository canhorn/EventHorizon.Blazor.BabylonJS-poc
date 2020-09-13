namespace EventHorizon.Game.Client.Engine.Gui.ClientAction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Gui.Register;
    using EventHorizon.Game.Client.Engine.Gui.Reload;
    using MediatR;

    public class ClientActionGuiSystemReloadedEventHandler
        : INotificationHandler<ClientActionGuiSystemReloadedEvent>
    {
        private readonly IMediator _mediator;

        public ClientActionGuiSystemReloadedEventHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ClientActionGuiSystemReloadedEvent notification,
            CancellationToken cancellationToken
        )
        {
            foreach (var guiLayout in notification.GuiLayoutList)
            {
                await _mediator.Send(
                    new RegisterGuiLayoutDataCommand(
                        guiLayout
                    ),
                    cancellationToken
                );
            }

            await _mediator.Publish(
                new GuiSystemFinishedReloadingEvent(),
                cancellationToken
            );
        }
    }
}
