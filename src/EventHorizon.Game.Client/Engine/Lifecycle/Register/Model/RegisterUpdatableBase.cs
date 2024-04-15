namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Lifecycle.Api;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;

public class RegisterUpdatableBase : RegisterBase<IUpdatableEntity>, IRegisterUpdatable
{
    public override Task Run()
    {
        foreach (var entity in _entityList)
        {
            entity.Update();
        }

        return Task.CompletedTask;
    }
}
