namespace EventHorizon.Game.Client.Core.ModelResolver.Api
{
    public interface IModelResolver<TResult>
    {
        public TResult Resolve(
            object details
        );
    }
}
