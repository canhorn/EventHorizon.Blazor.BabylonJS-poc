using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

namespace EventHorizon.Game.Client.Engine.Systems.Entity.State
{
    public class StandardEntityDetailsState
        : IEntityDetailsState
    {
        private readonly IDictionary<string, IObjectEntityDetails> _detailMap = new ConcurrentDictionary<string, IObjectEntityDetails>();

        public IEnumerable<IObjectEntityDetails> All()
        {
            return _detailMap.Values;
        }

        public bool Contains(
            string globalId
        )
        {
            return _detailMap.ContainsKey(
                globalId
            );
        }

        public IObjectEntityDetails Get(
            string globalId
        )
        {
            return _detailMap[globalId];
        }

        public IObjectEntityDetails Remove(
            string globalId
        )
        {
            if (_detailMap.Remove(
                globalId,
                out var entityDetails
            ))
            {
                return entityDetails;
            }
            return null;
        }

        public void Set(
            IObjectEntityDetails entityDetails
        )
        {
            _detailMap.TryAdd(
                entityDetails.GlobalId,
                entityDetails
            );
        }
    }
}
