namespace EventHorizon.Game.Editor.Client.Zone.Components.Commands;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Services.Command.Response;
using EventHorizon.Game.Editor.Services.Model.Command;

public class ZoneCommandResponseListModel
    : ObservableComponentBase,
        AdminCommandResponseEventObserver
{
    public List<CommandResponse> CommandResponseList = new();

    public Task Handle(AdminCommandResponseEvent args)
    {
        CommandResponseList.Insert(
            0,
            new CommandResponse { Response = args.Response, }
        );
        return InvokeAsync(StateHasChanged);
    }
}

public class CommandResponse
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
    public AdminCommandResponse Response { get; set; } =
        new AdminCommandResponse();
}
