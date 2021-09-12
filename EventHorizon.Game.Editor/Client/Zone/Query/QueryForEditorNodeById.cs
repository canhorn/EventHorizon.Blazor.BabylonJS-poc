namespace EventHorizon.Game.Editor.Client.Zone.Query
{

    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

    using MediatR;

    public struct QueryForEditorNodeById
        : IRequest<CommandResult<EditorNode>>
    {
        public string Id { get; }

        public QueryForEditorNodeById(
            string id
        )
        {
            Id = id;
        }
    }
}
