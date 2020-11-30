namespace EventHorizon.Game.Editor.Zone.Editor.Clone
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using MediatR;

    public class CloneObjectEntityDetailsCommandHandler
        : IRequestHandler<CloneObjectEntityDetailsCommand, CommandResult<ObjectEntityDetailsModel>>
    {
        public Task<CommandResult<ObjectEntityDetailsModel>> Handle(
            CloneObjectEntityDetailsCommand request,
            CancellationToken cancellationToken
        )
        {
            return new CommandResult<ObjectEntityDetailsModel>(
                JsonSerializer.Deserialize<ObjectEntityDetailsModel>(
                    JsonSerializer.Serialize(
                        request.EntityDetails
                    )
                )
            ).FromResult();
        }
    }
}
