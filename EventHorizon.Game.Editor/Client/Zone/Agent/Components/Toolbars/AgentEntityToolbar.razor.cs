namespace EventHorizon.Game.Editor.Client.Zone.Agent.Components.Toolbars;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Zone.Reload;
using EventHorizon.Game.Editor.Zone.Services.Agent.Create;

public class AgentEntityToolbarModel : EditorComponentBase
{
    public async Task HandleNew()
    {
        var newAgentEntity = new ObjectEntityDetailsModel { Name = "New Agent Entity", };

        var result = await Mediator.Send(new CreateAgentEntityCommand(newAgentEntity));

        if (result.Success.IsNotTrue())
        {
            await ShowMessage(
                Localizer["Agent Entity"],
                Localizer["Create Agent Failed: {0}", result.ErrorCode],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Agent Entity"],
            Localizer["Created Agent Entity! {0}", result.Result.Id],
            MessageLevel.Success
        );
        await Mediator.Send(new ReloadActiveZoneStateCommand());
    }
}
