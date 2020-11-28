namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Create
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using MediatR;

    public struct CreateClientEntityCommand
        : IRequest<CommandResult<IObjectEntityDetails>>
    {
        public IObjectEntityDetails Entity { get; }

        public CreateClientEntityCommand(
            IObjectEntityDetails entity
        )
        {
            Entity = entity;
        }
    }
}
