namespace EventHorizon.Game.Client.Systems.Dialog.Api
{
    using System.Collections.Generic;

    public interface DialogTreeActionScript
    {
        public string Id { get; }
        public IDictionary<string, object> Data { get; }
    }
}