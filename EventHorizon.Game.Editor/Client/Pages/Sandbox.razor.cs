namespace EventHorizon.Game.Editor.Client.Pages;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Activity;
using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public partial class Sandbox
{
    #region Activity Tracking
    public async Task HandleSendActivityEvent()
    {
        await Mediator.Send(new SandboxActivityEvent());
    }

    public struct SandboxActivityEvent
        : IRequest<CommandResult<bool>>,
            TrackActivity { }

    public class SandboxActivityEventHandler
        : IRequestHandler<SandboxActivityEvent, CommandResult<bool>>
    {
        public Task<CommandResult<bool>> Handle(
            SandboxActivityEvent request,
            CancellationToken cancellationToken
        )
        {
            return new CommandResult<bool>(true).FromResult();
        }
    }
    #endregion
}
