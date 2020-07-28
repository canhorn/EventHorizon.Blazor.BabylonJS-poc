namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
    using System.Collections.Generic;

    public interface IEntityDetailsState
    {
        IEnumerable<IObjectEntityDetails> All();
        IObjectEntityDetails Get(
            string globalId
        );
        bool Contains(
            string globalId
        );
        void Set(
            IObjectEntityDetails entityDetails
        );
        IObjectEntityDetails? Remove(
            string globalId
        );
    }
}
