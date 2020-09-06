namespace EventHorizon.Game.Client.Engine.Systems.ClientAction.Publish
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Core.ModelResolver.Api;
    using EventHorizon.Game.Client.Engine.Debugging.Model;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class PublishClientActionCommandHandler
        : IRequestHandler<PublishClientActionCommand, StandardCommandResult>
    {
        private readonly ILogger<PublishClientActionCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly ClientActionState _state;

        public PublishClientActionCommandHandler(
            ILogger<PublishClientActionCommandHandler> logger,
            IMediator mediator,
            ClientActionState state
        )
        {
            _logger = logger;
            _mediator = mediator;
            _state = state;
        }

        public async Task<StandardCommandResult> Handle(
            PublishClientActionCommand request,
            CancellationToken cancellationToken
        )
        {
            var clientAction = _state.Get(
                request.ActionName,
                request.Data
            );
            _logger.LogDebug("Action: {ClientAction}", request.ActionName);
            if (clientAction.HasValue)
            {
                await _mediator.Publish(
                    clientAction.Value
                );
                return new StandardCommandResult();
            }
            return new StandardCommandResult(
                "not_found"
            );
        }
    }
    public class ClientActionDataResolver
        : IClientActionDataResolver
    {
        private readonly IDictionary<string, object> _data;

        public ClientActionDataResolver(
            IDictionary<string, object> data
        )
        {
            _data = data;
        }

        [return: MaybeNull]
        public T Resolve<T>(
            string argumentName
        )
        {
            if (_data.TryGetValue(
                argumentName,
                out var argument
            ))
            {
                var resolver = GameServiceProvider.GetService__UNSAFE<IModelResolver<T>>();
                if (resolver != null)
                {
                    return resolver.Resolve(
                        argument
                    );
                }
                var value = argument.Cast<T>();
                if (value != null)
                {
                    return value;
                }
            }

            return default;
        }
    }
}
