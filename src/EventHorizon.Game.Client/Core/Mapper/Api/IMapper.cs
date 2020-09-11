namespace EventHorizon.Game.Client.Core.Mapper.Api
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IMapperBase
    {
    }

    public interface IMapper<T>
    {
        [return: MaybeNull]
        T Map(
            object obj
        );
    }
}
