namespace EventHorizon.Game.Client.Systems.ClientScripts.Get
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using MediatR;

    public struct QueryForClientScriptById
        : IRequest<CommandResult<IClientScript>>
    {
        public string Id { get; }

        public QueryForClientScriptById(
            string id
        )
        {
            Id = id;
        }
    }
}
