namespace EventHorizon.Game.Editor.Client.Zone.ClientEntity.Components.Providers;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Model;
using EventHorizon.Game.Editor.Client.Zone.Pages;
using EventHorizon.Game.Editor.Client.Zone.Reload;
using EventHorizon.Game.Editor.Zone.Services.ClientEntity.Delete;
using EventHorizon.Game.Editor.Zone.Services.ClientEntity.Save;

using Microsoft.AspNetCore.Components;

public class ClientEntityProviderModel : EditorComponentBase
{
    [MaybeNull]
    [CascadingParameter]
    public IObjectEntityDetails ClientEntity { get; set; }

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public EntityEditorState EntityEditorState =>
        new EntityEditorStateModel
        {
            OnSave = HandleSave,
            ShowDelete = true,
            OnDelete = HandleDelete,
        };

    private async Task HandleSave(IObjectEntityDetails entity)
    {
        await ShowMessage(
            Localizer["Client Entity"],
            Localizer["Saving Client Entity..."]
        );
        var result = await Mediator.Send(
            new SaveClientEntityCommand(entity)
        );
        if (result.Success.IsNotTrue())
        {
            await ShowMessage(
                Localizer["Client Entity"],
                Localizer[
                    "Failed to Save Client Entity: {0} | {1}",
                    result.ErrorCode,
                    entity.GlobalId
                ],
                MessageLevel.Error
            );
            return;
        }
        await ShowMessage(
            Localizer["Client Entity"],
            Localizer["Saved Client Entity."]
        );
        await Mediator.Send(new ReloadActiveZoneStateCommand());
    }

    private async Task HandleDelete(IObjectEntityDetails entity)
    {
        await ShowMessage(
            Localizer["Client Entity"],
            Localizer["Deleting Client Entity..."]
        );
        var result = await Mediator.Send(
            new DeleteClientEntityCommand(entity.GlobalId)
        );
        if (result.Success.IsNotTrue())
        {
            await ShowMessage(
                Localizer["Client Entity"],
                Localizer[
                    "Failed to Delete Client Entity: {0} | {1}",
                    result.ErrorCode,
                    entity.GlobalId
                ],
                MessageLevel.Error
            );
            return;
        }
        await ShowMessage(
            Localizer["Client Entity"],
            Localizer["Deleted Client Entity."]
        );

        NavigationManager.NavigateTo(ZoneEntityListPage.Route);

        await Mediator.Send(new ReloadActiveZoneStateCommand());
    }
}
