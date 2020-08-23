namespace EventHorizon.Game.Client.Engine.Gui.State
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class StandardGuiLayoutDataState
        : IGuiLayoutDataState
    {
        private readonly IDictionary<string, IGuiLayoutData> _map = new Dictionary<string, IGuiLayoutData>();

        public Option<IGuiLayoutData> Get(
            string id
        )
        {
            if (_map.TryGetValue(
                id,
                out var layout
            ))
            {
                return layout
                    .ToOption();
            }
            return new Option<IGuiLayoutData>();
        }

        public void Set(
            IGuiLayoutData layout
        )
        {
            if (layout == null)
            {
                throw new GameException(
                    "gui_layout_null",
                    "Cannot set Null GUI Layout into State"
                );
            }
            _map[layout.Id] = layout;
        }
    }
}
