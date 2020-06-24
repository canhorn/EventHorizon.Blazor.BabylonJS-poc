using System.Collections.Generic;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    public interface IObjectEntityDetails
        : IDictionary<string, object>
    {
        public long Id { get; }
        public string Name { get; }
        public string GlobalId { get; }
        public string Type { get; }
        public IServerTransform MyProperty { get; }
        public IList<string> TagList { get; }
        public IDictionary<string, object> Data { get; }
    }
}
