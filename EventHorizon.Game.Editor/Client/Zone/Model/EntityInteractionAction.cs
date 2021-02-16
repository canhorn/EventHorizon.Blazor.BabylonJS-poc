namespace EventHorizon.Game.Editor.Client.Zone.Model
{
    using System;


    public sealed class EntityInteractionAction
    {
        public static readonly EntityInteractionAction SELECTED_FROM_LIST = new EntityInteractionAction(
            "selected-from-list"
        );

        public string Action { get; }

        private EntityInteractionAction(
            string action
        )
        {
            Action = action;
        }
    }
}
