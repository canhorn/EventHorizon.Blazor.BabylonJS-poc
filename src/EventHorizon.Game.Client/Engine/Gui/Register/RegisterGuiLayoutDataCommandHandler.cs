namespace EventHorizon.Game.Client.Engine.Gui.Register
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RegisterGuiLayoutDataCommandHandler
        : IRequestHandler<RegisterGuiLayoutDataCommand, StandardCommandResult>
    {
        private readonly IGuiLayoutDataState _state;

        public RegisterGuiLayoutDataCommandHandler(
            IGuiLayoutDataState state
        )
        {
            _state = state;
        }

        public Task<StandardCommandResult> Handle(
            RegisterGuiLayoutDataCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                _state.Set(
                    request.LayoutData
                );

                return new StandardCommandResult()
                    .FromResult();
            }
            catch (GameException ex)
            {
                GameServiceProvider.GetService<ILogger<RegisterGuiLayoutDataCommandHandler>>()
                    .LogError(
                        ex,
                        "Game Exception"
                    );

                return new StandardCommandResult(
                    ex.ErrorCode
                ).FromResult();
            }
            catch (Exception ex)
            {
                GameServiceProvider.GetService<ILogger<RegisterGuiLayoutDataCommandHandler>>()
                    .LogError(
                        ex,
                        "General Exception"
                    );

                return new StandardCommandResult(
                    "general_exception"
                ).FromResult();
            }
        }
    }
}
