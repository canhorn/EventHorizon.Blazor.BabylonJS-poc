namespace EventHorizon.Game.Client.Core.Builder.Api
{
    public interface IBuilder<TResult, TData>
    {
        public TResult Build(
            TData details
        );
    }
}
