namespace EventHorizon.Game.Client.Systems.ClientScripts.Run;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Scripting.Api;
using EventHorizon.Game.Client.Engine.Scripting.Data;
using EventHorizon.Game.Client.Engine.Scripting.Services;

using MediatR;

public record RunStartupScripts(
    IEnumerable<IStartupClientScript> StartupScripts
) : IRequest<StandardCommandResult>;

public class RunStartupScriptsHandler
    : IRequestHandler<RunStartupScripts, StandardCommandResult>
{
    private readonly ScriptServices _scriptServices;

    public RunStartupScriptsHandler(ScriptServices scriptServices)
    {
        _scriptServices = scriptServices;
    }

    public async Task<StandardCommandResult> Handle(
        RunStartupScripts request,
        CancellationToken cancellationToken
    )
    {
        foreach (var script in request.StartupScripts)
        {
            await script.Run(
                _scriptServices,
                new ScriptData(new Dictionary<string, object>())
            );
        }

        return new();
    }
}
