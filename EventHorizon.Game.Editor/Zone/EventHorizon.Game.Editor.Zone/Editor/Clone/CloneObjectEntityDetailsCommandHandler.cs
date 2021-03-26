namespace EventHorizon.Game.Editor.Zone.Editor.Clone
{
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;

    public class CloneObjectEntityDetailsCommandHandler
        : IRequestHandler<CloneObjectEntityDetailsCommand, CommandResult<ObjectEntityDetailsModel>>
    {
        public Task<CommandResult<ObjectEntityDetailsModel>> Handle(
            CloneObjectEntityDetailsCommand request,
            CancellationToken cancellationToken
        )
        {
            var entityDetails = JsonSerializer.Deserialize<ObjectEntityDetailsModel>(
                JsonSerializer.Serialize(
                    request.EntityDetails
                )
            );
            if(entityDetails.IsNull())
            {
                return new CommandResult<ObjectEntityDetailsModel>(
                    ZoneEditorErrorCodes.EDITOR_INVALID_ENTITY_DETAILS
                ).FromResult();
            }

            return new CommandResult<ObjectEntityDetailsModel>(
                entityDetails
            ).FromResult();
        }
    }
}
