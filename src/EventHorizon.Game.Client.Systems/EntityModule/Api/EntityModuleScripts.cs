namespace EventHorizon.Game.Client.Systems.EntityModule.Api;

public interface EntityModuleScripts
{
    public string Name { get; }
    public string InitializeScript { get; }
    public string DisposeScript { get; }
    public string UpdateScript { get; }
}
