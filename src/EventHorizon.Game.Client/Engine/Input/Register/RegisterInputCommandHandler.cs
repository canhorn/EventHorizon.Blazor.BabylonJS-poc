namespace EventHorizon.Game.Client.Engine.Input.Register
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using MediatR;

    public class RegisterInputCommandHandler
        : IRequestHandler<RegisterInputCommand, string>
    {
        private readonly IRegisterInput _register;

        public RegisterInputCommandHandler(
            IRegisterInput register
        )
        {
            _register = register;
        }

        public Task<string> Handle(
            RegisterInputCommand request, 
            CancellationToken cancellationToken
        ) => _register.Register(
            request.InputOptions
        ).FromResult();
    }
}
