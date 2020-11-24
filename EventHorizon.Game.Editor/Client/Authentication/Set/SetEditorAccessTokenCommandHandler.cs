namespace EventHorizon.Game.Editor.Client.Authentication.Set
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Client.Authentication.Api;
    using MediatR;

    public class SetEditorAccessTokenCommandHandler
        : IRequestHandler<SetEditorAccessTokenCommand, StandardCommandResult>
    {
        private readonly EditorAuthenticationState _state;

        public SetEditorAccessTokenCommandHandler(
            EditorAuthenticationState state
        )
        {
            _state = state;
        }

        public Task<StandardCommandResult> Handle(
            SetEditorAccessTokenCommand request,
            CancellationToken cancellationToken
        )
        {
            _state.SetAccessToken(
                request.AccessToken
            );
            return new StandardCommandResult()
                .FromResult();
        }
    }
}
