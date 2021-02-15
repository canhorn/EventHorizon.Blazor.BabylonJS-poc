namespace EventHorizon.Game.Editor.Client.Shared.Components
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.Register;
    using EventHorizon.Observer.Unregister;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public abstract class ObservableComponentBase
        : ComponentBase,
        ObserverBase,
        IAsyncDisposable
    {
        [Inject]
        public IMediator Mediator { get; set; } = null!;

        protected bool _isRegistered;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await Mediator.Send(
                new RegisterObserverCommand(this)
            );
            _isRegistered = true;
        }

        public virtual async ValueTask DisposeAsync()
        {
            await Mediator.Send(
                new UnregisterObserverCommand(this)
            );
            _isRegistered = false;
        }
    }
}
