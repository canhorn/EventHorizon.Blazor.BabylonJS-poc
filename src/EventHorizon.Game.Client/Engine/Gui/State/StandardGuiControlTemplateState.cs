namespace EventHorizon.Game.Client.Engine.Gui.State
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Exceptions;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class StandardGuiControlTemplateState
        : IGuiControlTemplateState
    {
        private readonly IDictionary<string, IGuiControlTemplate> _map = new Dictionary<string, IGuiControlTemplate>();

        public Option<IGuiControlTemplate> Get(
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
            return new Option<IGuiControlTemplate>();
        }

        public bool Has(
            string id
        )
        {
            return _map.ContainsKey(
                id
            );
        }

        public void Set(
            IGuiControlTemplate template
        )
        {
            if (template == null)
            {
                throw new GameException(
                    "gui_control_template_null",
                    "Cannot set Null GUI Control Template into State"
                );
            }
            _map[template.Id] = template;
        }
    }
}
