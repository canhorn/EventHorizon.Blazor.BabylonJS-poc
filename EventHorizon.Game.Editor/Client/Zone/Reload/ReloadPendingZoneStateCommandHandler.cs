namespace EventHorizon.Game.Editor.Client.Zone.Reload
{
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Zone.Query;

    using MediatR;

    public class ReloadPendingZoneStateCommandHandler
        : IRequestHandler<ReloadPendingZoneStateCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;

        public ReloadPendingZoneStateCommandHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<StandardCommandResult> Handle(
            ReloadPendingZoneStateCommand request,
            CancellationToken cancellationToken
        )
        {
            var zoneStateResult = await _mediator.Send(
                new QueryForActiveZone(),
                cancellationToken
            );
            if (!zoneStateResult)
            {
                return zoneStateResult.ErrorCode;
            }

            var zoneState = zoneStateResult.Result;
            if (zoneState.IsPendingReload)
            {
                await _mediator.Send(
                    new ReloadActiveZoneStateCommand(),
                    cancellationToken
                );
            }

            return new();
        }
    }
}
