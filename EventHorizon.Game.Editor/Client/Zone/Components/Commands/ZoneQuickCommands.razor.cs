namespace EventHorizon.Game.Editor.Client.Zone.Components.Commands
{
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Components.Select;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Services.Command.Response;
    using EventHorizon.Game.Editor.Client.Zone.Services.Command.Send;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ZoneQuickCommandsModel
        : ObservableComponentBase,
        AdminCommandResponseEventObserver
    {
        [CascadingParameter]
        public ZoneState ZoneState { get; set; } = null!;
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public StandardSelectOption SelectedCommand { get; set; } = null!;
        public IList<StandardSelectOption> QuickCommandOptions { get; private set; } = new List<StandardSelectOption>();

        public bool IsDisabled { get; set; }
        public IList<string> QuickCommands { get; set; } = new List<string>
        {
            "reload-admin",
            "reload-system",
        };

        protected override async Task OnInitializedAsync()
        {
            SelectedCommand = new StandardSelectOption
            {
                Value = "none",
                Text = Localizer["Select a Command..."],
                Disabled = true,
                Hidden = true,
            };
            var options = QuickCommands
                .Select(command => new StandardSelectOption
                {
                    Value = command,
                    Text = command,
                }).ToList();
            options.Insert(0, SelectedCommand);
            QuickCommandOptions = options;

            await base.OnInitializedAsync();
        }

        public async Task RunCommand(
            string command
        )
        {
            IsDisabled = true;
            await Mediator.Send(
                new SendZoneAdminCommand(
                    command,
                    new { }
                )
            );
            IsDisabled = false;
        }

        public async Task HandleSelectedComandChanged(
            StandardSelectOption option
        )
        {
            option.NullCheck();
            await RunCommand(
                option.Value
            );

            // Set the SelectedCommand to the Selected option.
            // The current component state is updated right away. 
            // This then allows for the change back to the first select to be picked-up.
            SelectedCommand = option;
            await InvokeAsync(StateHasChanged);
            SelectedCommand = QuickCommandOptions.First();
        }

        public Task Handle(
            AdminCommandResponseEvent args
        )
        {
            return Mediator.Publish(
                new ShowMessageEvent(
                    Localizer["'{0}' Command Response", args.Response.CommandFunction],
                    Localizer["Response: {0}", args.Response.Message],
                    args.Response.Success ? MessageLevel.Success : MessageLevel.Error
                )
            );
        }
    }
}
