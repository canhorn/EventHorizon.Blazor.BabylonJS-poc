using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using MediatR;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.Register
{
    public class RegisterClientEntityHandler
        : IRequestHandler<RegisterClientEntity>
    {
        private readonly IMediator _mediator;
        private readonly IEntityDetailsState _state;

        public RegisterClientEntityHandler(
            IMediator mediator,
            IEntityDetailsState state
        )
        {
            _mediator = mediator;
            _state = state;
        }

        public async Task<Unit> Handle(
            RegisterClientEntity request,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send(
                new RegisteringClientEntity(
                    request.EntityDetails
                )
            );
            _state.Set(
                request.EntityDetails
            );
            await _mediator.Send(
                new ClientEntityRegistered(
                    request.EntityDetails
                )
            );

            return Unit.Value;
        }
    }
}
