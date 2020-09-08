namespace EventHorizon.Game.Client.Engine.Canvas.Api
{
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;

    public interface ICanvas
        : IServiceEntity
    {
        T GetDrawingCanvas<T>() where T : class;
    }
}
