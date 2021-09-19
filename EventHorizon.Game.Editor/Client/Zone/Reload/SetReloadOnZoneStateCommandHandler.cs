namespace EventHorizon.Game.Editor.Client.Zone.Reload
{
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Builder.Api;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Zone.Active;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Client.Zone.Query;

    using MediatR;

    public class SetReloadOnZoneStateCommandHandler
        : IRequestHandler<SetReloadOnZoneStateCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IBuilder<ZoneStateModel, ZoneState> _modelBuilder;

        public SetReloadOnZoneStateCommandHandler(
            IMediator mediator,
            IBuilder<ZoneStateModel, ZoneState> modelBuilder
        )
        {
            _mediator = mediator;
            _modelBuilder = modelBuilder;
        }

        public async Task<StandardCommandResult> Handle(
            SetReloadOnZoneStateCommand request,
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
            var newState = _modelBuilder.Build(
                zoneState
            );
            newState.IsPendingReload = request.Reload;

            // Cache Zone State
            var setActiveZoneResult = await _mediator.Send(
                new SetZoneAsActiveCommand(
                    newState
                ),
                cancellationToken
            );
            if (setActiveZoneResult.Success.IsNotTrue())
            {
                return setActiveZoneResult;
            }

            await _mediator.Publish(
                new ActiveZoneStateChangedEvent(
                    newState.Zone.Id
                ),
                cancellationToken
            );

            return new();
        }
    }
}
