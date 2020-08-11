namespace EventHorizon.Game.Client.Core.Mapper.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IMapperBase
    {
    }

    public interface IMapper<T>
    {
        T Map(
            object obj
        );
    }
}
