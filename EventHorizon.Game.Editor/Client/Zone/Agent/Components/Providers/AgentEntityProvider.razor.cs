namespace EventHorizon.Game.Editor.Client.Zone.Agent.Components.Providers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Editor.Client.Shared.Components;
    using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
    using EventHorizon.Game.Editor.Client.Zone.Api;
    using EventHorizon.Game.Editor.Client.Zone.Model;
    using EventHorizon.Game.Editor.Client.Zone.Reload;
    using EventHorizon.Game.Editor.Zone.Services.Agent.Delete;
    using EventHorizon.Game.Editor.Zone.Services.Agent.Save;
    using Microsoft.AspNetCore.Components;

    public class AgentEntityProviderModel
        : EditorComponentBase
    {
        [MaybeNull]
        [CascadingParameter]
        public IObjectEntityDetails AgentEntity { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        public EntityEditorState EntityEditorState => new EntityEditorStateModel
        {
            OnSave = HandleSave,
            ShowDelete = true,
            OnDelete = HandleDelete,
        };

        private async Task HandleSave(
            IObjectEntityDetails entity
        )
        {
            await ShowMessage(
                Localizer["Agent Entity"],
                Localizer["Saving Agent Entity..."]
            );
            // Copy all Data into Raw data for the Agent
            if (entity is ObjectEntityDetailsModel objectEntity)
            {
                // TODO: Look into this, it might be removing cross Game Server Data
                objectEntity.RawData = objectEntity.Data;
            }
            var result = await Mediator.Send(
                new SaveAgentEntityCommand(
                    entity
                )
            );
            if (result.Success.IsNotTrue())
            {
                await ShowMessage(
                    Localizer["Agent Entity"],
                    Localizer["Failed to Save Agent Entity: {0} | {1}", result.ErrorCode, entity.GlobalId],
                    MessageLevel.Error
                );
                return;
            }
            await ShowMessage(
                Localizer["Agent Entity"],
                Localizer["Saved Agent Entity."],
                MessageLevel.Success
            );

            await Mediator.Send(
                new ReloadActiveZoneStateCommand()
            );
        }

        private async Task HandleDelete(
            IObjectEntityDetails entity
        )
        {
            await ShowMessage(
                Localizer["Agent Entity"],
                Localizer["Deleting Agent Entity..."]
            );
            var result = await Mediator.Send(
                new DeleteAgentEntityCommand(
                    entity.GlobalId
                )
            );
            if (result.Success.IsNotTrue())
            {
                await ShowMessage(
                    Localizer["Agent Entity"],
                    Localizer["Failed to Delete Agent Entity: {0} | {1}", result.ErrorCode, entity.GlobalId],
                    MessageLevel.Error
                );
                return;
            }
            await ShowMessage(
                Localizer["Agent Entity"],
                Localizer["Deleted Agent Entity."],
                MessageLevel.Success
            );

            NavigationManager.NavigateTo("zone/entity");

            await Mediator.Send(
                new ReloadActiveZoneStateCommand()
            );
        }
    }
}
