namespace EventHorizon.Game.Editor.Client.Zone.ClientEntity.Components.Toolbars;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Zone.Reload;
using EventHorizon.Game.Editor.Zone.Services.ClientEntity.Create;

public class ClientEntityToolbarModel : EditorComponentBase
{
    public async Task HandleNew()
    {
        var newClientEntity = new ObjectEntityDetailsModel();

        var result = await Mediator.Send(new CreateClientEntityCommand(newClientEntity));

        if (result.Success.IsNotTrue())
        {
            await ShowMessage(
                Localizer["Client Entity"],
                Localizer["Create Client Failed: {0}", result.ErrorCode],
                MessageLevel.Error
            );
        }

        await ShowMessage(
            Localizer["Client Entity"],
            Localizer["Created Client Entity! {0}", result.Result.Id]
        );
        await Mediator.Send(new ReloadActiveZoneStateCommand());
    }
}
