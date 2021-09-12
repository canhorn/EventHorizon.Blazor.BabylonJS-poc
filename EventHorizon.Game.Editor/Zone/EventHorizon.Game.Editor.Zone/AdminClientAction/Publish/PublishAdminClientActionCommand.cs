namespace EventHorizon.Game.Editor.Zone.AdminClientAction.Publish
{
    using System.Collections.Generic;

    using EventHorizon.Game.Client.Core.Command.Model;

    using MediatR;

    public struct PublishAdminClientActionCommand
        : IRequest<StandardCommandResult>
    {
        public string ActionName { get; }
        public IDictionary<string, object> Data { get; }

        public PublishAdminClientActionCommand(
            string actionName,
            IDictionary<string, object> data
        )
        {
            ActionName = actionName;
            Data = data;
        }
    }
}
