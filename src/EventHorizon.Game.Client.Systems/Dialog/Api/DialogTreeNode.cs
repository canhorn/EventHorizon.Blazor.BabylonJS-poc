namespace EventHorizon.Game.Client.Systems.Dialog.Api
{
    using System.Collections.Generic;

    public interface DialogTreeNode
    {
        public string TitleKey { get; }
        public IDictionary<string, object> TitleData { get; }
        public string TextKey { get; }
        public IDictionary<string, object> TextData { get; }
        public IEnumerable<DialogTreeActionNode> Actions { get; }
    }
}