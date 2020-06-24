namespace EventHorizon.Game.Client.Engine.Systems.ServerModule.Add
{
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Systems.Scripting.Api;
    using EventHorizon.Game.Client.Engine.Systems.ServerModule.Model;

    public class StandardServerModule : ClientEntityBase, IServerModule
    {
        private readonly ServerModuleScripts _scripts;

        public string Name => _scripts.Name;

        public StandardServerModule(
            long clientId,
            ServerModuleScripts scripts
        ) : base(clientId)
        {
            this._scripts = scripts;
        }

        public Task Dispose()
        {
            // TODO: [ServerModule] : Implement logic
            throw new System.NotImplementedException();
        }

        public Task Initialize()
        {
            // TODO: [ServerModule] : Implement logic
            throw new System.NotImplementedException();
        }

        public Task PostInitialize()
        {
            // TODO: [ServerModule] : Implement logic
            throw new System.NotImplementedException();
        }

        public Task Update()
        {
            // TODO: [ServerModule] : Implement logic
            throw new System.NotImplementedException();
        }
    }
}