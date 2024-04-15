namespace EventHorizon.Game.Editor.Zone.Systems.Wizard.Run;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.Wizard.Model;
using EventHorizon.Zone.Systems.Wizard.Run;
using MediatR;

public class RunWizardScriptProcessorCommandHandler
    : IRequestHandler<RunWizardScriptProcessorCommand, CommandResult<WizardData>>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public RunWizardScriptProcessorCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<WizardData>> Handle(
        RunWizardScriptProcessorCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.Wizard.RunScriptProcessor(
            request.WizardId,
            request.WizardStepId,
            request.ProcessorScriptId,
            request.WizardData,
            cancellationToken
        );
        if (result.Success.IsNotTrue())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
