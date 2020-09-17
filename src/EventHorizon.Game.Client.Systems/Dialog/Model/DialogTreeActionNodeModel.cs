namespace EventHorizon.Game.Client.Systems.Dialog.Model
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Systems.Dialog.Api;

    public class DialogTreeActionNodeModel
        : DialogTreeActionNode
    {
        public string TextKey { get; set; } = string.Empty;
        public Dictionary<string, object> TextData { get; set; } = new Dictionary<string, object>();
        IDictionary<string, object> DialogTreeActionNode.TextData => TextData;
        public string ActionType { get; set; } = string.Empty;
        [MaybeNull]
        public DialogTreeActionScriptModel Script { get; set; }
        [MaybeNull]
        DialogTreeActionScript DialogTreeActionNode.Script => Script;
        [MaybeNull]
        public string NextNodeKey { get; set; }
    }
}