using System;
using System.Collections.Generic;
using System.Text;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api
{
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
        IObjectEntityDetails Remove(
            string globalId
        );
    }
}
