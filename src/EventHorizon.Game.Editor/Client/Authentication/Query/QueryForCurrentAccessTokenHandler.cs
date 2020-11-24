namespace EventHorizon.Game.Editor.Client.Authentication.Query
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using MediatR;

    public class QueryForCurrentAccessTokenHandler
        : IRequestHandler<QueryForCurrentAccessToken, CommandResult<string>>
    {
        private readonly EditorAuthenticationState _cache;

        public QueryForCurrentAccessTokenHandler(
            EditorAuthenticationState cache
        )
        {
            _cache = cache;
        }

        public Task<CommandResult<string>> Handle(
            QueryForCurrentAccessToken request,
            CancellationToken cancellationToken
        )
        {
            return new CommandResult<string>(
                !string.IsNullOrWhiteSpace(
                    _cache.AccessToken
                ),
                _cache.AccessToken
            ).FromResult();
        }
    }
}
