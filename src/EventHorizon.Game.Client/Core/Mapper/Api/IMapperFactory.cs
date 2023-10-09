namespace EventHorizon.Game.Client.Core.Mapper.Api;

using System;

public interface IMapperFactory
{
    public T Map<T>(object obj);
}
