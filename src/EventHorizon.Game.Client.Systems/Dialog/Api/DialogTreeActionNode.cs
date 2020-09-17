namespace EventHorizon.Game.Client.Systems.Dialog.Api
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface DialogTreeActionNode
    {
        public string TextKey { get; }
        public IDictionary<string, object> TextData { get; }
        public string ActionType { get; }
        [MaybeNull]
        public DialogTreeActionScript Script { get; }
        [MaybeNull]
        public string NextNodeKey { get; }
    }
}