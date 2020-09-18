namespace EventHorizon.Game.Client.Systems.EntityModule
{
    using EventHorizon.Game.Client.Systems.EntityModule.Api;

    public class EntityModuleScriptsModel
        : EntityModuleScripts
    {
        public string Name { get; set; } = string.Empty;
        public string InitializeScript { get; set; } = string.Empty;
        public string DisposeScript { get; set; } = string.Empty;
        public string UpdateScript { get; set; } = string.Empty;
    }
}
