namespace EventHorizon.Game.Editor.Client.Zone.Loading
{
    using EventHorizon.Game.Client.Core.Command.Model;

    using MediatR;

    public struct SetLoadingOnZoneStateCommand
        : IRequest<StandardCommandResult>
    {
        public bool IsLoading { get; }

        public SetLoadingOnZoneStateCommand(
            bool isLoading
        )
        {
            IsLoading = isLoading;
        }
    }
}
