﻿namespace EventHorizon.Game.Client.Systems.Account.Disconnected
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using EventHorizon.Game.Client.Systems.Connection.Core.Account.Disconnected;
    using EventHorizon.Game.Client.Engine.Window.Api;
    using MediatR;

    public class AccountDisconnectedEventLoginRedirectHandler
        : INotificationHandler<AccountDisconnectedEvent>
    {
        private readonly ISystemWindow _systemWindow;
        private readonly IAccountState _state;

        public AccountDisconnectedEventLoginRedirectHandler(
            ISystemWindow systemWindow,
            IAccountState state
        )
        {
            _systemWindow = systemWindow;
            _state = state;
        }

        public Task Handle(
            AccountDisconnectedEvent notification,
            CancellationToken cancellationToken
        )
        {
            if (notification.Code == "status_code_401")
            {
                _systemWindow.NavigateTo(
                    _state.AccountLoginUrl
                );
            }
            return Task.CompletedTask;
        }
    }
}