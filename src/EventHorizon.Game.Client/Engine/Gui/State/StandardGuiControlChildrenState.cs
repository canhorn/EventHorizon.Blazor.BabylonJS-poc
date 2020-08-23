namespace EventHorizon.Game.Client.Engine.Gui.State
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Gui.Api;

    public class StandardGuiControlChildrenState
        : IGuiControlChildrenState
    {
        private static readonly IList<string> EMPTY_CHILD_LIST = new List<string>().AsReadOnly();
        private readonly IDictionary<string, IList<string>> _map = new Dictionary<string, IList<string>>();

        public void AddChildGuiToControl(
            string controlId,
            string childGuiId
        )
        {
            if (_map.TryGetValue(
                controlId,
                out var controlChildren
            ))
            {
                controlChildren.Add(
                    childGuiId
                );
                return;
            }
            _map.Add(
                controlId,
                new List<string>
                {
                    childGuiId,
                }
            );
        }

        public IEnumerable<string> GetChildren(
            string controlId
        )
        {
            if (_map.TryGetValue(
                controlId,
                out var controlChildren
            ))
            {
                return controlChildren;
            }
            return EMPTY_CHILD_LIST;
        }

        public IEnumerable<string> RemoveTrackingOfControl(
            string controlId
        )
        {
            if (_map.TryGetValue(
                controlId,
                out var controlChildren
            ))
            {
                _map.Remove(
                    controlId
                );
                return controlChildren;
            }
            return EMPTY_CHILD_LIST;
        }
    }
}
