namespace EventHorizon.Game.Editor.Client.Shared.Components;

using System;
using System.Threading.Tasks;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.Register;
using EventHorizon.Observer.Unregister;

public abstract class ObservableComponentBase : EditorComponentBase, ObserverBase, IAsyncDisposable
{
    protected bool _isRegistered;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Mediator.Send(new RegisterObserverCommand(this));
        _isRegistered = true;
    }

    public virtual async ValueTask DisposeAsync()
    {
        await Mediator.Send(new UnregisterObserverCommand(this));
        _isRegistered = false;
    }
}
