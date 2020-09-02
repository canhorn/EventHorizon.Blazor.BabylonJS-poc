namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    using System.Collections.Generic;

    public interface IObjectEntityDetails
    {
        public long Id { get; }
        public string Name { get; }
        public string GlobalId { get; }
        public string Type { get; }
        public IServerTransform Transform { get; }
        public IList<string> TagList { get; }
        public IDictionary<string, object> Data { get; }
    }
}
