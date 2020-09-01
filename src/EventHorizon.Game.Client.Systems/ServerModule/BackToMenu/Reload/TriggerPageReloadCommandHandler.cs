namespace EventHorizon.Game.Client.Systems.ServerModule.BackToMenu.Reload
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.Interop;
    using EventHorizon.Game.Server.ServerModule.BackToMenu.Reload;
    using MediatR;

    public class TriggerPageReloadCommandHandler
        : IRequestHandler<TriggerPageReloadCommand>
    {
        public Task<Unit> Handle(
            TriggerPageReloadCommand request, 
            CancellationToken cancellationToken
        )
        {
            EventHorizonBlazorInterop.RunScript(
                "reload_window",
                "window.location.reload();",
                new { }
            );

            return Unit.Task;
        }
    }
}
