﻿namespace EventHorizon.Game.Editor.Client.Wizard.Components.Provider;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Wizard.Api;
using EventHorizon.Game.Editor.Client.Zone.Services.Command.Response;
using EventHorizon.Game.Editor.Zone.Services.Connection;
using EventHorizon.Zone.Systems.Wizard.Query;
using Microsoft.AspNetCore.Components;

public class WizardStateProviderModel
    : ObservableComponentBase,
        ZoneAdminServiceConnectedEventObserver,
        AdminCommandResponseEventObserver
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public WizardState State { get; set; } = null!;

    public override ValueTask DisposeAsync()
    {
        State.OnChange -= HandleStateChanged;
        return base.DisposeAsync();
    }

    public async Task Handle(ZoneAdminServiceConnectedEvent _)
    {
        var result = await Mediator.Send(new QueryForAllZoneWizards());

        await State.SetWizardList(result.Result);
    }

    public async Task Handle(AdminCommandResponseEvent args)
    {
        if (args.Response.Message != "wizard_system_reloaded")
        {
            return;
        }

        var result = await Mediator.Send(new QueryForAllZoneWizards());

        if (result)
        {
            await State.SetWizardList(result.Result);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        State.OnChange += HandleStateChanged;

        var result = await Mediator.Send(new QueryForAllZoneWizards());

        if (result)
        {
            await State.SetWizardList(result.Result);
        }
    }

    private Task HandleStateChanged(WizardStateChangeArgs args)
    {
        return InvokeAsync(StateHasChanged);
    }
}
