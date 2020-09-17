namespace EventHorizon.Game.Client.Systems.Dialog.Open
{
    using System;
    using EventHorizon.Game.Client.Core.Command.Model;
    using MediatR;

    public class OpenDialogTreeCommand
        : IRequest<StandardCommandResult>
    {
        public string DialogTreeId { get; }
        public long PlayerId { get; }
        public long NpcId { get; }

        public OpenDialogTreeCommand(
            string dialogTreeId,
            long playerId,
            long npcId
        )
        {
            DialogTreeId = dialogTreeId;
            PlayerId = playerId;
            NpcId = npcId;
        }
    }
}
