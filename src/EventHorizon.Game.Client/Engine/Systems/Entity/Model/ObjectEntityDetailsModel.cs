namespace EventHorizon.Game.Client.Engine.Systems.Entity.Model
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

    public class ObjectEntityDetailsModel
        : IObjectEntityDetails
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GlobalId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public ServerTransform Transform { get; set; } = new ServerTransform();
        IServerTransform IObjectEntityDetails.Transform => Transform;
        public IList<string> TagList { get; set; } = new List<string>();
        public IDictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public bool IsReadOnly { get; set; }
    }
}
