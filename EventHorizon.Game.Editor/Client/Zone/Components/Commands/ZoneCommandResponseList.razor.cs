namespace EventHorizon.Game.Editor.Client.Zone.Components.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Zone.Services.Command.Response;
    using EventHorizon.Game.Editor.Services.Model.Command;
    using Microsoft.AspNetCore.Components;

    public class ZoneCommandResponseListModel
        : ObservableComponentBase,
        AdminCommandResponseEventObserver
    {
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;

        public List<CommandResponse> CommandResponseList = new List<CommandResponse>();

        public Task Handle(
            AdminCommandResponseEvent args
        )
        {
            CommandResponseList.Insert(
                0,
                new CommandResponse
                {
                    Response = args.Response,
                }
            );
            return InvokeAsync(StateHasChanged);
        }
    }

    public class CommandResponse
    {
        public string Key { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        public AdminCommandResponse Response { get; set; }
    }
}
