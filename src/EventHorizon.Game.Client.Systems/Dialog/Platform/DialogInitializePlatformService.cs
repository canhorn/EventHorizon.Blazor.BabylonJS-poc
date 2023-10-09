namespace EventHorizon.Game.Client.Systems.Dialog.Platform;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Model;
using EventHorizon.Game.Client.Systems.ClientAssets.Config.Register;
using EventHorizon.Game.Client.Systems.Dialog.Model;

using MediatR;

public class DialogInitializePlatformService : IServiceEntity
{
    private readonly IMediator _mediator;

    public int Priority => 1000;

    public DialogInitializePlatformService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Initialize()
    {
        await _mediator.Send(
            new RegisterClientAssetConfigTypeBuilderCommand(
                DialogTreeModel.CLIENT_ASSET_TYPE,
                new StandardClientAssetConfigTypeBuilder(
                    (data) => new DialogTreeModel(data)
                )
            )
        );
    }

    public Task Dispose()
    {
        return Task.CompletedTask;
    }
}
