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
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class PublishClientActionCommandHandler
        : IRequestHandler<PublishClientActionCommand, StandardCommandResult>
    {
        private readonly ILogger<PublishClientActionCommandHandler> _logger;
        private readonly IMediator _mediator;
        private static IDictionary<string, Type> _actionTypes;

        public PublishClientActionCommandHandler(
            ILogger<PublishClientActionCommandHandler> logger
        )
        {
            _logger = logger;
            _mediator = GameServiceProvider.GetService<IMediator>();

            if (_actionTypes == null)
            {
                _actionTypes = new Dictionary<string, Type>();


                var clientActionType = typeof(IClientAction);
                var interfaces = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.DefinedTypes)
                    .Where(type => typeof(IClientAction).IsAssignableFrom(type));
                foreach (var item in interfaces)
                {
                    var attributes = Attribute.GetCustomAttributes(item, typeof(ClientActionAttribute));
                    if (attributes.Length > 0)
                    {
                        var ClientActionAttribute = (ClientActionAttribute)attributes.First();
                        //if (ClientActionAttribute.Name == request.ActionName)
                        //{

                        //}
                        _actionTypes.Add(
                            ClientActionAttribute.Name,
                            item
                        );
                        //Console.WriteLine(clientActionyAttribute.Name);

                        //foreach (var prop in request.Data)
                        //{
                        //    Console.WriteLine(prop.Key);
                        //}

                        //var instance = Activator.CreateInstance(
                        //    item,
                        //    new ClientActionDataResolver(
                        //        request.Data
                        //    )
                        //);
                        //if (instance != null)
                        //{
                        //    GameServiceProvider.GetService<IMediator>().Publish(
                        //        instance
                        //    );
                        //}
                    }

                }
            }
        }
        public Task<StandardCommandResult> Handle(
            PublishClientActionCommand request,
            CancellationToken cancellationToken
        )
        {
            if (_actionTypes.TryGetValue(
                request.ActionName,
                out var actionType
            ))
            {
                var instance = Activator.CreateInstance(
                    actionType,
                    new ClientActionDataResolver(
                        request.Data
                    )
                );
                if (instance != null)
                {
                    _logger.LogDebug("Action: {ClientAction}", request.ActionName);
                    _mediator.Publish(
                        instance
                    );
                }
            }

            //return new StandardCommandResult().FromResult();
            //// TODO: Lookup all IClientAction
            //var clientActionType = typeof(IClientAction);
            //var interfaces = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(x => x.DefinedTypes)
            //    .Where(type => typeof(IClientAction).IsAssignableFrom(type));
            //foreach (var item in interfaces)
            //{
            //    var attributes = Attribute.GetCustomAttributes(item, typeof(ClientActionAttribute));
            //    if (attributes.Length > 0)
            //    {
            //        var ClientActionAttribute = (ClientActionAttribute)attributes.First();
            //        if (ClientActionAttribute.Name == request.ActionName)
            //        {

            //        }
            //        //Console.WriteLine(clientActionyAttribute.Name);

            //        //foreach (var prop in request.Data)
            //        //{
            //        //    Console.WriteLine(prop.Key);
            //        //}

            //        var instance = Activator.CreateInstance(
            //            item,
            //            new ClientActionDataResolver(
            //                request.Data
            //            )
            //        );
            //        if (instance != null)
            //        {
            //            GameServiceProvider.GetService<IMediator>().Publish(
            //                instance
            //            );
            //        }
            //    }

            //}
            return new StandardCommandResult().FromResult();
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
