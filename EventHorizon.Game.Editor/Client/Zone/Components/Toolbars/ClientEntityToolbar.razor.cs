namespace EventHorizon.Game.Editor.Client.Zone.Components.Toolbars
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Editor.Client.Localization;
    using EventHorizon.Game.Editor.Client.Localization.Api;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Show;
    using EventHorizon.Game.Editor.Zone.Services.ClientEntity.Create;
    using MediatR;
    using Microsoft.AspNetCore.Components;

    public class ClientEntityToolbarModel
        : ComponentBase
    {
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;

        public async Task HandleNew()
        {
            var newClientEntity = new ObjectEntityDetailsModel();

            var result = await Mediator.Send(
                new CreateClientEntityCommand(
                    newClientEntity
                )
            );

            if (result.Success.IsNotTrue())
            {
                await Mediator.Publish(
                    new ShowMessageEvent(
                        Localizer["Client Entity"],
                        Localizer["Create Client Failed: {0}", result.ErrorCode],
                        MessageLevel.Error
                    )
                );
            }

            await Mediator.Publish(
                new ShowMessageEvent(
                    Localizer["Client Entity"],
                    Localizer["Created Client Entity! {0}", result.Result.Id],
                    MessageLevel.Success
                )
            );
        }
    }
}
