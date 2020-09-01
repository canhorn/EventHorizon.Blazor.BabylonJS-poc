namespace EventHorizon.Game.Client.Systems.ServerModule.Api
{
    public interface IServerModuleScripts
    {
        public string Name { get; }
        public string InitializeScript { get; }
        public string DisposeScript { get; }
        public string UpdateScript { get; }
    }
}
