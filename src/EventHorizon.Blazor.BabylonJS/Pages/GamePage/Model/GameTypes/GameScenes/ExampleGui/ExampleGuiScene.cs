namespace EventHorizon.Blazor.BabylonJS.Pages.GamePage.Model.GameTypes.GameScenes.ExampleGui
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Client;
    using EventHorizon.Game.Client.Engine.Gui.Activate;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Gui.Create;
    using EventHorizon.Game.Client.Engine.Gui.Dispose;
    using EventHorizon.Game.Client.Engine.Gui.Model;
    using EventHorizon.Game.Client.Engine.Gui.Register;
    using EventHorizon.Game.Client.Systems.Local.Scenes.Model;
    using Microsoft.Extensions.Logging;

    public class ExampleGuiScene
        : GameSceneBase
    {
        private readonly ILogger _logger = GameServiceProvider.GetService<ILogger<ExampleGuiScene>>();
        private readonly HttpClient _http = GameServiceProvider.GetService<HttpClient>();

        private string _mainMenuGuiId = string.Empty;

        public ExampleGuiScene()
            : base("example-gui")
        {
        }

        public override async Task Initialize()
        {
            try
            {

                // IGuiLayoutData
                var mainMenuGui = await _http.GetFromJsonAsync<GuiLayoutDataModel>(
                    "test-data/example.gui.json"
                );
                await _mediator.Send(
                    new RegisterGuiLayoutDataCommand(
                        mainMenuGui
                    )
                );

                await _mediator.Send(
                    new CreateGuiCommand(
                        mainMenuGui.Id,
                        mainMenuGui.Id,
                        GetControlWithData()
                    )
                );

                await _mediator.Send(
                    new ActivateGuiCommand(
                        mainMenuGui.Id
                    )
                );
                _mainMenuGuiId = mainMenuGui.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
            }
        }

        private IEnumerable<IGuiControlData> GetControlWithData()
        {
            Func<Task> RefreshPageClickHandler = async () => await EventHorizonBlazorInterop.RunScript(
                "refreshPage",
                "window.location.href = window.location.href;",
                new { }
            );
            Func<Task> AlertClickHandler = async () => await EventHorizonBlazorInterop.RunScript(
                "alert",
                "alert('Button Clicked!')",
                new { }
            );

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
            };
        }

        public override async Task Dispose()
        {
            await _mediator.Send(
                new DisposeOfGuiCommand(
                    _mainMenuGuiId
                )
            );
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
    }
}
