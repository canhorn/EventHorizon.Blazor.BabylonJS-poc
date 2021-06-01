namespace EventHorizon.Game.Editor.Client.Shared.Components
{
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public abstract class EditorComponentBase
        : ComponentBase
    {
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;
    }
}
