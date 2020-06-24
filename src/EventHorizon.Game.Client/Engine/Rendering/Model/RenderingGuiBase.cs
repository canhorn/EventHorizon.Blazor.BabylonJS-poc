namespace EventHorizon.Game.Client.Engine.Rendering.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Gui.Api;
    using EventHorizon.Game.Client.Engine.Rendering.Api;

    public class RenderingGuiBase
        : IRenderingGui
    {
        public int Priority { get; }

        public Task Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetCanvas<T>() where T : IGuiCanvas
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            // TODO: [GUI] : Finish implementation
            return Task.CompletedTask;
        }
    }
}
