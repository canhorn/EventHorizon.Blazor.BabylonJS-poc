namespace EventHorizon.Game.Client.Systems.Dialog.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.Dialog.Api;

    public class DialogTreeActionScriptModel
        : DialogTreeActionScript
    {
        public string Id { get; set; } = string.Empty;
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        IDictionary<string, object> DialogTreeActionScript.Data => Data;
    }
}