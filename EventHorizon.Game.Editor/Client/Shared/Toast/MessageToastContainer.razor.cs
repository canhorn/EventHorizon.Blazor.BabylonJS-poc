namespace EventHorizon.Game.Editor.Client.Shared.Toast;

using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Shared.Toast.Show;

public class MessageToastContainerModel : ObservableComponentBase, ShowMessageEventObserver
{
    public IDictionary<string, MessageModel> MessageList { get; set; } =
        new Dictionary<string, MessageModel>();

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    public override ValueTask DisposeAsync()
    {
        return base.DisposeAsync();
    }

    public Task Handle(ShowMessageEvent args)
    {
        var message = args.Message;
        MessageList[message.Id] = message;

        return InvokeAsync(StateHasChanged);
    }

    public void HandleRemoveMessage(string messageId)
    {
        MessageList.Remove(messageId);
    }
}
