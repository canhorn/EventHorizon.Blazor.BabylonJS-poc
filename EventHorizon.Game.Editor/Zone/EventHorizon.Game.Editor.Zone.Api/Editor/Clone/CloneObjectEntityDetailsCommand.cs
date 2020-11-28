namespace EventHorizon.Game.Editor.Zone.Editor.Clone
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using MediatR;

    public struct CloneObjectEntityDetailsCommand
        : IRequest<CommandResult<IObjectEntityDetails>>
    {
        public IObjectEntityDetails EntityDetails { get; }

        public CloneObjectEntityDetailsCommand(
            IObjectEntityDetails entityDetails
        )
        {
            EntityDetails = entityDetails;
        }
    }
}
