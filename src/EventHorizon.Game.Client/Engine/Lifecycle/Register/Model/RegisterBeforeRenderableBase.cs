namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;

public class RegisterBeforeRenderableBase
    : RegisterBase<IBeforeRenderableEntity>,
        IRegisterBeforeRenderable
{
    public override Task Run()
    {
        foreach (var entity in _entityList)
        {
            entity.BeforeRender();
        }

        return Task.CompletedTask;
    }
}
