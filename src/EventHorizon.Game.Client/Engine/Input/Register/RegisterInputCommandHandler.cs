namespace EventHorizon.Game.Client.Engine.Input.Register;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Input.Api;
using MediatR;

public class RegisterInputCommandHandler
    : IRequestHandler<RegisterInputCommand, CommandResult<string>>
{
    private readonly IRegisterInput _register;

    public RegisterInputCommandHandler(IRegisterInput register)
    {
        _register = register;
    }

    public Task<CommandResult<string>> Handle(
        RegisterInputCommand request,
        CancellationToken cancellationToken
    ) => new CommandResult<string>(true, _register.Register(request.InputOptions)).FromResult();
}
