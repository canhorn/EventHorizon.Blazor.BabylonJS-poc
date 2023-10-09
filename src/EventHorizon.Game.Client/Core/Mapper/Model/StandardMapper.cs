namespace EventHorizon.Game.Client.Core.Mapper.Model;

using EventHorizon.Game.Client.Core.Mapper.Api;

public class StandardMapper<T, D> : IMapper<T>
    where D : T
{
    public T? Map(object obj)
    {
        return obj.To<D>();
    }
}
