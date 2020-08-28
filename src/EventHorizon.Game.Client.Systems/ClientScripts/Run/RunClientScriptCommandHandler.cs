using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Model.Scripting.Data;
using EventHorizon.Game.Client.Engine.Model.Scripting.Services;
using EventHorizon.Game.Client.Engine.Systems.Scripting.Run;
using EventHorizon.Game.Client.Systems.ClientScripts.Api;
using MediatR;

namespace EventHorizon.Game.Client.Systems.ClientScripts.Run
{
    public class RunClientScriptCommandHandler
        : IRequestHandler<RunClientScriptCommand, StandardCommandResult>
    {
        private readonly ClientScriptsState _state;
        private readonly ScriptServices _scriptServices;

        public RunClientScriptCommandHandler(
            ClientScriptsState state,
            ScriptServices scriptServices
        )
        {
            _state = state;
            _scriptServices = scriptServices;
        }

        public async Task<StandardCommandResult> Handle(
            RunClientScriptCommand request,
            CancellationToken cancellationToken
        )
        {
            var script = _state.GetScript(
                request.Id
            );

            if (request.Id == script.Id)
            {
                await script.Run(
                    _scriptServices,
                    new ScriptData(
                        request.Data
                    )
                );
            }

            return new StandardCommandResult();
        }
    }
}