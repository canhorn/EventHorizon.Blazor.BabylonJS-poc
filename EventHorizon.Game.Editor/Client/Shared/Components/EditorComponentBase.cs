namespace EventHorizon.Game.Editor.Client.Shared.Components
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public abstract class EditorComponentBase
        : ComponentBase
    {
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;

        protected async Task ShowMessage(
            string title,
            string message,
            MessageLevel level = MessageLevel.Success
        ) => await Mediator.Publish(
            new ShowMessageEvent(
                title,
                message,
                level
            )
        );
    }
}
