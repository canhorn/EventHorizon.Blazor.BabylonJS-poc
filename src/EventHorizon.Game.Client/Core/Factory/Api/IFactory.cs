namespace EventHorizon.Game.Client.Core.Factory.Api
{
    using System;

    public interface IFactory<T>
    {
        T Create();
    }
}
