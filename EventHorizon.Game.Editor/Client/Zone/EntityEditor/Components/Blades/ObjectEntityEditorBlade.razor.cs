namespace EventHorizon.Game.Editor.Client.Zone.EntityEditor.Components.Blades;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.Set;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Interaction;
using Microsoft.AspNetCore.Components;

public class ObjectEntityEditorBladeBase : ObservableComponentBase, ObjectEntityOpenedEventObserver
{
    [CascadingParameter]
    public SessionValues SessionValues { get; set; } = null!;

    public string EntityId { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        EntityId = SessionValues.Get("lastOpenedObjectEntity", string.Empty);
        base.OnInitialized();
    }

    public async Task Handle(ObjectEntityInteractionEvent args)
    {
        EntityId = args.ObjectEntityId;
        await Mediator.Send(new SetSessionValueCommand("lastOpenedObjectEntity", EntityId));
        await InvokeAsync(StateHasChanged);
    }

    protected async Task HandleBackToEntityList()
    {
        EntityId = string.Empty;
        await Mediator.Send(new SetSessionValueCommand("lastOpenedObjectEntity", string.Empty));
    }
}
