namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Execptions;
    using EventHorizon.Observer.State;

    using MediatR;

    using Microsoft.Extensions.Logging;

    public class PublishClientActionCommandHandler
        : IRequestHandler<PublishClientActionCommand, StandardCommandResult>
    {
        private readonly ILogger<PublishClientActionCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ObserverState _observerState;
        private readonly ClientActionState _state;

        public PublishClientActionCommandHandler(
            ILogger<PublishClientActionCommandHandler> logger,
            IMediator mediator,
            ObserverState observerState,
            ClientActionState state
        )
        {
            _logger = logger;
            _mediator = mediator;
            _observerState = observerState;
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            PublishClientActionCommand request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogDebug("ActionName: {ActionName}", request.ActionName);
            var clientAction = _state.Get(request.ActionName, request.Data);
            if (clientAction.HasValue)
            {
                await _mediator.Publish(clientAction.Value, cancellationToken);
                return new StandardCommandResult();
            }

            // Send Script Client Action
            var externalActionOption = _state.GetExternal(
                request.ActionName,
                request.Data
            );
            if (externalActionOption.HasValue)
            {
                var externalAction = externalActionOption.Value;
                await _observerState.Trigger(
                    externalAction.ObserverType,
                    externalAction.ActionType,
                    externalAction.Action,
                    cancellationToken
                );
                return new StandardCommandResult();
            }

            return new StandardCommandResult("not_found");
        }
    }

    // TODO: [SDK] - Move into SDK
    public class ClientActionDataResolver : IClientActionDataResolver
    {
        private readonly IDictionary<string, object> _data;

        public ClientActionDataResolver(IDictionary<string, object> data)
        {
            _data = data;
        }

        public T? ResolveNullable<T>(string argumentName)
        {
            if (_data.TryGetValue(argumentName, out var argument))
            {
                var mapper = GameServiceProvider.GetService__UNSAFE<
                    IMapper<T>
                >();
                if (mapper.IsNotNull())
                {
                    return mapper.Map(argument);
                }
                var value = argument.To<T>();
                if (value.IsNotNull())
                {
                    return value;
                }
            }

            return default;
        }

        public T Resolve<T>(string argumentName) =>
            ResolveNullable<T>(argumentName)
            ?? throw new InvalidClientActionArgument(
                argumentName,
                $"Could not resolve '{argumentName}'"
            );
    }
}
