namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model;

using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;

public class RegisterDrawableBase
    : RegisterBase<IDrawableEntity>,
        IRegisterDrawable
{
    public override Task Run()
    {
        foreach (var entity in _entityList)
        {
            entity.Draw();
        }

        return Task.CompletedTask;
    }
}
