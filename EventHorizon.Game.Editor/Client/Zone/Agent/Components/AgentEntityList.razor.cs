namespace EventHorizon.Game.Editor.Client.Zone.Agent.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Editor.Client.Zone.Agent.Selected;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Interaction;
using EventHorizon.Game.Editor.Client.Zone.Model;
using MediatR;
using Microsoft.AspNetCore.Components;

public class AgentEntityListModel : ComponentBase
{
    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public IEnumerable<IObjectEntityDetails> EntityList =>
        ZoneState.ZoneInfo.EntityList.Where(a => a.Type == "AGENT");

    public async Task HandleEntityClicked(IObjectEntityDetails entity)
    {
        await Mediator.Publish(new AgentEntitySelectedEvent(entity));
        await Mediator.Publish(
            new ObjectEntityInteractionEvent(
                entity.GlobalId,
                EntityInteractionAction.SELECTED_FROM_LIST
            )
        );
    }
}
