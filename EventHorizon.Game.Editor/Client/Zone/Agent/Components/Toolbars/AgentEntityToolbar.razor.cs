namespace EventHorizon.Game.Editor.Client.Zone.Agent.Components.Toolbars
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

    public class AgentEntityToolbarModel
        : ComponentBase
    {
        [Inject]
        public Localizer<SharedResource> Localizer { get; set; } = null!;
        [Inject]
        public IMediator Mediator { get; set; } = null!;

        public async Task HandleNew()
        {
            var newAgentEntity = new ObjectEntityDetailsModel();

            // TODO: Create new Agent Entity
            //var result = await Mediator.Send(
            //    new CreateAgentEntityCommand(
            //        newAgentEntity
            //    )
            //);

            //if (result.Success.IsNotTrue())
            //{
            //    await Mediator.Publish(
            //        new ShowMessageEvent(
            //            Localizer["Agent Entity"],
            //            Localizer["Create Agent Failed: {0}", result.ErrorCode],
            //            MessageLevel.Error
            //        )
            //    );
            //}

            //await Mediator.Publish(
            //    new ShowMessageEvent(
            //        Localizer["Agent Entity"],
            //        Localizer["Created Agent Entity! {0}", result.Result.Id],
            //        MessageLevel.Success
            //    )
            //);
        }
    }
}
