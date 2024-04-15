namespace EventHorizon.Game.Editor.Client.Zone.Components.Providers;

using System;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Factory.Api;
using EventHorizon.Game.Client.Core.Timer.Api;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Zone.Reload;
using Microsoft.AspNetCore.Components;

public class ReloadZoneStateProviderModel : EditorComponentBase, IDisposable
{
    private IIntervalTimerService? _intervalTimer;

    [Inject]
    public IFactory<IIntervalTimerService> IntervalTimerFactory { get; set; } = null!;

    public void Dispose()
    {
        _intervalTimer?.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _intervalTimer = IntervalTimerFactory
            .Create()
            .Setup(250, async () => await Mediator.Send(new ReloadPendingZoneStateCommand()))
            .Start();
    }
}
