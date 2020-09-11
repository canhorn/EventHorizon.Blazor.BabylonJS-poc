namespace EventHorizon.Game.Client.Core.Mapper.Model
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using EventHorizon.Game.Client.Core.Mapper.Api;

    public class StandardMapper<T, D>
        : IMapper<T> where D : T
    {
        [return: MaybeNull]
        public T Map(
            object obj
        )
        {
            return (T)obj.Cast<D>();
        }
    }
}
