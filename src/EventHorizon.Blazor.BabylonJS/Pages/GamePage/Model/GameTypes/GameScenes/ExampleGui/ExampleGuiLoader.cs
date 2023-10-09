namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.ExampleGui;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using EventHorizon.Blazor.Interop;
using EventHorizon.Game.Client;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Activate;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Create;
using EventHorizon.Game.Client.Engine.Gui.Dispose;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Gui.Register;
using EventHorizon.Game.Client.Engine.Systems.Scripting.Run;
using EventHorizon.Game.Client.Engine.Testing.Events;

using MediatR;

using Microsoft.Extensions.Logging;

public class ExampleGuiLoader
{
    private readonly ILogger _logger = GameServiceProvider.GetService<
        ILogger<ExampleGuiScene>
    >();
    private readonly HttpClient _http =
        GameServiceProvider.GetService<HttpClient>();
    private readonly IMediator _mediator =
        GameServiceProvider.GetService<IMediator>();

    private string _mainMenuGuiId = string.Empty;

    public ExampleGuiLoader() { }

    public async Task Initialize()
    {
        try
        {
            // IGuiLayoutData
            var mainMenuGui = await _http.GetFromJsonAsync<GuiLayoutDataModel>(
                "test-data/example.gui.json"
            );
            if (mainMenuGui.IsNull())
            {
                throw new GameException(
                    "TEST_DATA_NOT_FOUND",
                    "Was not able to find 'test-data/example.gui.json'"
                );
            }
            await _mediator.Send(new RegisterGuiLayoutDataCommand(mainMenuGui));

            await _mediator.Send(
                new CreateGuiCommand(
                    mainMenuGui.Id,
                    mainMenuGui.Id,
                    GetControlWithData()
                )
            );

            await _mediator.Send(new ActivateGuiCommand(mainMenuGui.Id));
            _mainMenuGuiId = mainMenuGui.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
        }
    }

    private IEnumerable<IGuiControlData> GetControlWithData()
    {
        Func<Task> RefreshPageClickHandler = async () =>
            await EventHorizonBlazorInterop.RunScript(
                "refreshPage",
                "window.location.href = window.location.href;",
                new { }
            );
        Func<Task> AlertClickHandler = async () =>
            await EventHorizonBlazorInterop.RunScript(
                "alert",
                "alert('Button Clicked!')",
                new { }
            );
        Func<Task> InitScriptHandler = async () =>
        {
            var scriptId = "Actions_TestActionScript";
            var scriptData = new Dictionary<string, object>();
            await _mediator.Send(
                new RunClientScriptCommand(scriptId, scriptId, scriptData)
            );
        };
        Func<Task> TriggerScriptObserverHandler = async () =>
        {
            await _mediator.Publish(new ScriptTestingEvent());
        };

        return new List<IGuiControlData>
        {
            new GuiControlDataModel
            {
                ControlId = "example-refresh_page-button",
                Options = new GuiControlOptionsModel
                {
                    { "onClick", RefreshPageClickHandler },
                },
            },
            new GuiControlDataModel
            {
                ControlId = "example-alert-button",
                Options = new GuiControlOptionsModel
                {
                    { "onClick", AlertClickHandler },
                },
            },
            new GuiControlDataModel
            {
                ControlId = "example-init_script-button",
                Options = new GuiControlOptionsModel
                {
                    { "onClick", InitScriptHandler },
                },
            },
            new GuiControlDataModel
            {
                ControlId = "example-run_script-button",
                Options = new GuiControlOptionsModel
                {
                    { "onClick", TriggerScriptObserverHandler },
                },
            },
        };
    }

    public async Task Dispose()
    {
        await _mediator.Send(new DisposeOfGuiCommand(_mainMenuGuiId));
    }

    public Task Update()
    {
        return Task.CompletedTask;
    }

    public Task Draw()
    {
        return Task.CompletedTask;
    }
}
