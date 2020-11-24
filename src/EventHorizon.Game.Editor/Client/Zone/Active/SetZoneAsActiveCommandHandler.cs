namespace EventHorizon.Game.Editor.Client.Zone.Active
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Change;
    using MediatR;

    public class SetZoneAsActiveCommandHandler
        : IRequestHandler<SetZoneAsActiveCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly ZoneStateCache _cache;

        public SetZoneAsActiveCommandHandler(
            IMediator mediator,
            ZoneStateCache cache
        )
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<StandardCommandResult> Handle(
            SetZoneAsActiveCommand request,
            CancellationToken cancellationToken
        )
        {
            // Override any existing in cache
            _cache.Set(
                request.Zone.Zone.Id,
                request.Zone
            );

            // Set ZoneState as Active
            _cache.SetActive(
                request.Zone.Zone.Id,
                () => request.Zone
            );

            // Publish Active ZoneState Changed
            await _mediator.Publish(
                new ActiveZoneStateChangedEvent(
                    request.Zone.Zone.Id
                ),
                cancellationToken
            );

            return new();
        }
    }
}
