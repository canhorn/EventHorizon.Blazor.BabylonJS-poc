namespace EventHorizon.Game.Client.Systems.ClientScripts.Get;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Scripting.Api;
using EventHorizon.Game.Client.Engine.Scripting.Get;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;

using MediatR;

public class QueryForClientScriptByIdHandler
    : IRequestHandler<QueryForClientScriptById, CommandResult<IClientScript>>
{
    private readonly ClientScriptsState _state;

    public QueryForClientScriptByIdHandler(
        ClientScriptsState state
    )
    {
        _state = state;
    }

    public Task<CommandResult<IClientScript>> Handle(
        QueryForClientScriptById request,
        CancellationToken cancellationToken
    )
    {
        var script = _state.GetScript(
            request.Id
        );
        if (script.HasValue)
        {
            return new CommandResult<IClientScript>(
                script.Value
            ).FromResult();
        }
        return new CommandResult<IClientScript>(
            "client_script_not_found"
        ).FromResult();
    }
}
