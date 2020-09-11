namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    using System.Collections.Generic;

    public interface IObjectEntityDetails
    {
        public static long DEFAULT_ID => -1;

        long Id { get; }
        string Name { get; }
        string GlobalId { get; }
        string Type { get; }
        IServerTransform Transform { get; }
        IList<string> TagList { get; }
        IDictionary<string, object> Data { get; }
    }
}
