namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.MainMenu;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Activate;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Create;
using EventHorizon.Game.Client.Engine.Gui.Dispose;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Gui.Register;
using EventHorizon.Game.Client.Engine.Gui.Update;
using EventHorizon.Game.Client.Systems.Account.Api;
using EventHorizon.Game.Client.Systems.Account.Changed;
using EventHorizon.Game.Client.Systems.Account.Query;
using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
using EventHorizon.Game.Client.Systems.Local.Scenes.Start;
using EventHorizon.Game.Client.Systems.Zone.Changed;

using Microsoft.Extensions.Logging;

public class MainMenuScene
    : GameSceneBase,
        AccountChangedEventObserver,
        ZoneChangedEventObserver
{
    private readonly ILogger _logger = GameServiceProvider.GetService<
        ILogger<MainMenuScene>
    >();
    private readonly HttpClient _http =
        GameServiceProvider.GetService<HttpClient>();

    private string _mainMenuGuiId = string.Empty;

    public MainMenuScene()
        : base("main-menu") { }

    public override async Task Initialize()
    {
        GamePlatfrom.RegisterObserver(this);
        var accountInfoResult = await _mediator.Send(new QueryForAccountInfo());

        // IGuiLayoutData
        var mainMenuGui = await _http.GetFromJsonAsync<GuiLayoutDataModel>(
            // TODO: Move into gui/main-menu.gui.json
            "test-data/main-menu.gui.json"
        );
        if (mainMenuGui.IsNull())
        {
            throw new GameException(
                "TEST_DATA_NOT_FOUND",
                "Was not able to find 'test-data/main-menu.gui.json'"
            );
        }
        await _mediator.Send(new RegisterGuiLayoutDataCommand(mainMenuGui));

        await _mediator.Send(
            new CreateGuiCommand(
                mainMenuGui.Id,
                mainMenuGui.Id,
                GetControlWithData(accountInfoResult)
            )
        );

        await _mediator.Send(new ActivateGuiCommand(mainMenuGui.Id));
        _mainMenuGuiId = mainMenuGui.Id;
    }

    private IEnumerable<IGuiControlData> GetControlWithData(
        CommandResult<IAccountInfo> accountInfoResult
    )
    {
        var accountDetailsDisabled = accountInfoResult.Success;
        var zoneDetailsAvailable = accountInfoResult.Success;

        Func<Task> StartZoneSceneClickHandler = () =>
            _mediator.Send(new StartSceneCommand("zone"));
        Func<Task> StartAccountDetailsSceneClickHandler = () =>
            _mediator.Send(new StartSceneCommand("account-details"));
        Func<Task> StartExampleGuiSceneClickHandler = () =>
            _mediator.Send(new StartSceneCommand("example-gui"));

        return new List<IGuiControlData>
        {
            new GuiControlDataModel
            {
                ControlId = "main_menu-start_game-button",
                Options = new GuiControlOptionsModel
                {
                    { "isDisabled", !zoneDetailsAvailable },
                    { "onClick", StartZoneSceneClickHandler },
                },
            },
            new GuiControlDataModel
            {
                ControlId = "main_menu-account_details-button",
                Options = new GuiControlOptionsModel
                {
                    { "isDisabled", !accountDetailsDisabled },
                    { "onClick", StartAccountDetailsSceneClickHandler },
                },
            },
            new GuiControlDataModel
            {
                ControlId = "main_menu-example_gui-button",
                Options = new GuiControlOptionsModel
                {
                    { "onClick", StartExampleGuiSceneClickHandler },
                },
            },
        };
    }

    public override async Task Dispose()
    {
        GamePlatfrom.UnRegisterObserver(this);
        await _mediator.Send(new DisposeOfGuiCommand(_mainMenuGuiId));
        await base.Dispose();
    }

    public override Task Update()
    {
        return base.Update();
    }

    public override Task Draw()
    {
        return Task.CompletedTask;
    }

    public async Task Handle(AccountChangedEvent args)
    {
        await _mediator.Send(
            new UpdateGuiControlCommand(
                _mainMenuGuiId,
                new GuiControlDataModel
                {
                    ControlId = "main_menu-account_details-button",
                    Options = new GuiControlOptionsModel
                    {
                        { "isDisabled", false },
                    },
                }
            )
        );
    }

    public async Task Handle(ZoneChangedEvent args)
    {
        await _mediator.Send(
            new UpdateGuiControlCommand(
                _mainMenuGuiId,
                new GuiControlDataModel
                {
                    ControlId = "main_menu-start_game-button",
                    Options = new GuiControlOptionsModel
                    {
                        { "isDisabled", false },
                    },
                }
            )
        );
    }
}
