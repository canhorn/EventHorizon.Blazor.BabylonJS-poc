namespace EventHorizon.Game.Client.Systems.Dialog.Api
{
    using System;

    public interface DialogState
    {
        Option<DialogTree> Get(
            string id
        );
        void Set(
            string id, 
            DialogTree config
        );
    }
}
