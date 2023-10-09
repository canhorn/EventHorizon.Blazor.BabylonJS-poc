namespace EventHorizon.Game.Client.Engine.Rendering.Api;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;

public interface IRenderingGui : IServiceEntity
{
    public IGuiCanvas GetGuiCanvas();
    public T GetGuiCanvas<T>()
        where T : class, IGuiCanvas;
}
