namespace EventHorizon.Game.Editor.Client.LiveEditor.Scenes;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Systems.Account.Api;
using EventHorizon.Game.Client.Systems.Account.Changed;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Start;
using EventHorizon.Game.Client.Systems.Connection.Zone.Player.Stop;
using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
using Microsoft.Extensions.Logging;

public class LiveEditorScene : GameSceneBase, AccountChangedEventObserver
{
    private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<LiveEditorScene>>();
    private readonly IAccountState _accountState = GameServiceProvider.GetService<IAccountState>();

    private string _serverAddress = string.Empty;

    public LiveEditorScene()
        : base("live-editor") { }

    public override async Task Initialize()
    {
        GamePlatform.RegisterObserver(this);
        await StartZoneConnection();
    }

    public override async Task Dispose()
    {
        GamePlatform.UnRegisterObserver(this);
        if (!string.IsNullOrEmpty(_serverAddress))
        {
            await _mediator.Send(new StopPlayerZoneConnectionCommand(_serverAddress));
        }
        await base.Dispose();
    }

    public override Task PostInitialize()
    {
        return base.PostInitialize();
    }

    public override Task Update()
    {
        return base.Update();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }

    public Task Handle(AccountChangedEvent args)
    {
        return StartZoneConnection();
    }

    private async Task StartZoneConnection()
    {
        if (
            _accountState.User.IsNotNull()
            && !string.IsNullOrEmpty(_accountState.User.Zone.ServerAddress)
        )
        {
            _serverAddress = _accountState.User.Zone.ServerAddress;
            _logger.LogDebug($"Started Player Connection {DateTime.UtcNow}");
            await _mediator.Send(new StartPlayerZoneConnectionCommand(_serverAddress));
        }
    }
}
