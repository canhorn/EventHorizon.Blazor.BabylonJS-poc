namespace EventHorizon.Game.Client.Engine.Gui.State
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class StandardGuiControlState
        : IGuiControlState
    {
        private readonly IDictionary<string, IGuiControl> _map = new Dictionary<string, IGuiControl>();

        public string GenerateId(
            string guiId,
            string controlId
        )
        {
            return $"{controlId}_{guiId}";
        }

        public Option<IGuiControl> Get(
            string guiId,
            string controlId
        )
        {
            return Get(
                GenerateId(
                    guiId,
                    controlId
                )
            );
        }

        public Option<IGuiControl> Get(
            string id
        )
        {
            if (_map.TryGetValue(
                id,
                out var control
            ))
            {
                return control
                    .ToOption();
            }
            return new Option<IGuiControl>(
                null
            );
        }

        public void Remove(
            string guiId, 
            string controlId
        )
        {
            _map.Remove(
                GenerateId(
                    guiId,
                    controlId
                )
            );
        }

        public void Set(
            string guiId, 
            IGuiControl control
        )
        {
            if (control == null)
            {
                throw new GameException(
                    "gui_control_null",
                    "Cannot set NULL GUI Control into State"
                );
            }
            _map[GenerateId(guiId, control.Id)] = control;
        }
    }
}
