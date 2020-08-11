namespace EventHorizon.Game.Client.Core.Mapper.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.Mapper.Api;

    public class StandardMapperFactory
        : IMapperFactory
    {
        private readonly IDictionary<Type, IMapperBase> _map;
        public StandardMapperFactory()
        {

        }
        public T Map<T>(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
