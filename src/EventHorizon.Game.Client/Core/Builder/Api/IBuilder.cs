namespace EventHorizon.Game.Client.Core.Builder.Api
{
    public interface IBuilder<T, D>
    {
        public T Build(
            D details
        );
    }
}
