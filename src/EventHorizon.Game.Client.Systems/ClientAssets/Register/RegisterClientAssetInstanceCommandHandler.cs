namespace EventHorizon.Game.Client.Systems.ClientAssets.Register
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.ClientAssets.Api;
    using EventHorizon.Game.Client.Systems.ClientAssets.Model;
    using MediatR;

    public class RegisterClientAssetInstanceCommandHandler
        : IRequestHandler<RegisterClientAssetInstanceCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IClientAssetInstanceStore _store;

        public RegisterClientAssetInstanceCommandHandler(
            IMediator mediator,
            IClientAssetInstanceStore store
        )
        {
            _mediator = mediator;
            _store = store;
        }

        public async Task<StandardCommandResult> Handle(
            RegisterClientAssetInstanceCommand request,
            CancellationToken cancellationToken
        )
        {
            var clientAssetInstance = new ClientAssetInstance(
                request.AssetInstanceId,
                request.Mesh,
                request.Position
            );
            _store.Set(
                clientAssetInstance
            );
            await _mediator.Publish(
                new ClientAssetInstanceRegisteredEvent(
                    clientAssetInstance
                )
            );

            return new StandardCommandResult();
        }
    }
}
