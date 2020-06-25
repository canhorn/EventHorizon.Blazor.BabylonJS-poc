namespace EventHorizon.Game.Client.Engine.Input.Trigger
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Input.Api;
    using MediatR;

    public class TriggerInputCommandHandler
        : IRequestHandler<TriggerInputCommand>
    {
        private readonly IInputState _inputState;

        public TriggerInputCommandHandler(
            IInputState inputState
        )
        {
            _inputState = inputState;
        }

        public Task<Unit> Handle(
            TriggerInputCommand request,
            CancellationToken cancellationToken
        )
        {
            var options = _inputState.Where(
                a => a.Key == request.Key
            );
            foreach (var option in options)
            {
                if (request.TriggerType == Model.InputTriggerType.Pressed)
                {
                    option.Pressed(
                        new InputKeyEvent(
                            request.Key
                        )
                    );
                }
                else if (request.TriggerType == Model.InputTriggerType.Released)
                {
                    option.Released(
                        new InputKeyEvent(
                            request.Key
                        )
                    );
                }
            }

            return Unit.Task;
        }
    }
}
