namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Model
{
    using System.Collections.Generic;

    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Core.Mapper.Api;
    using EventHorizon.Game.Editor.Zone.AdminClientAction.Api;
    using EventHorizon.Game.Editor.Zone.AdminClientAction.Execptions;

    public class AdminClientActionDataResolver
        : IAdminClientActionDataResolver
    {
        private readonly IDictionary<string, object> _data;

        public AdminClientActionDataResolver(
            IDictionary<string, object> data
        )
        {
            _data = data;
        }

        public T? ResolveNullable<T>(
            string argumentName
        )
        {
            if (_data.TryGetValue(
                argumentName,
                out var argument
            ))
            {
                var mapper = GameServiceProvider.GetService__UNSAFE<IMapper<T>>();
                if (mapper.IsNotNull())
                {
                    return mapper.Map(
                        argument
                    );
                }
                var value = argument.To<T>();
                if (value.IsNotNull())
                {
                    return value;
                }
            }

            return default;
        }

        public T Resolve<T>(
            string argumentName
        ) => ResolveNullable<T>(
            argumentName
        ) ?? throw new InvalidAdminClientActionArgument(
            argumentName,
            $"Could not resolve '{argumentName}'"
        );
    }
}
