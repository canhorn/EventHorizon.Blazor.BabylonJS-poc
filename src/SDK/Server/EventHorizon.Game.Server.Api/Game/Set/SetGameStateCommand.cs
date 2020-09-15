namespace EventHorizon.Game.Server.Game.Set
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Game.Api;
    using MediatR;

    public struct SetGameStateCommand
        : IRequest<StandardCommandResult>
    {
        public GameState GameState { get; }

        public SetGameStateCommand(
            GameState gameState
        )
        {
            GameState = gameState;
        }
    }
}
