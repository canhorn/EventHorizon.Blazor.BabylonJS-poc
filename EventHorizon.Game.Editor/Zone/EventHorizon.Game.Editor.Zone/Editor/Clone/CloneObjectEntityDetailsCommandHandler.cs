namespace EventHorizon.Game.Editor.Zone.Editor.Clone
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using MediatR;

    public class CloneObjectEntityDetailsCommandHandler
        : IRequestHandler<CloneObjectEntityDetailsCommand, CommandResult<IObjectEntityDetails>>
    {
        public Task<CommandResult<IObjectEntityDetails>> Handle(
            CloneObjectEntityDetailsCommand request,
            CancellationToken cancellationToken
        )
        {
            return new CommandResult<IObjectEntityDetails>(
                JsonSerializer.Deserialize<ObjectEntityDetailsModel>(
                    JsonSerializer.Serialize(
                        request.EntityDetails
                    )
                )
            ).FromResult();
        }
    }
}
