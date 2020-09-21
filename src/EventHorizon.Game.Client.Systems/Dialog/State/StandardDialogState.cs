namespace EventHorizon.Game.Client.Systems.Dialog.State
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Systems.Dialog.Api;

    public class StandardDialogState
        : DialogState
    {
        private readonly IDictionary<string, DialogTree> _map = new Dictionary<string, DialogTree>();

        public void Clear()
        {
            _map.Clear();
        }

        public Option<DialogTree> Get(
            string id
        )
        {
            if (_map.TryGetValue(
                id,
                out var value
            ))
            {
                return value.ToOption();
            }
            return new Option<DialogTree>(
                null
            );
        }

        public void Set(
            string id,
            DialogTree config
        )
        {
            _map[id] = config;
        }
    }
}
