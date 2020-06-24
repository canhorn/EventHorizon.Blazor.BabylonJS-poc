namespace EventHorizon.Game.Client.Engine.Lifecycle.Register.Model
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Lifecycle.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;

    public class RegisterDisposableBase
        : RegisterBase<IDisposableEntity>, IRegisterDisposable
    {
        public override Task Run()
        {
            foreach (var entity in _entityList)
            {
                entity.Dispose();
            }

            return Task.CompletedTask;
        }
    }
}
