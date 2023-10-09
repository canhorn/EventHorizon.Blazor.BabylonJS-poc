namespace EventHorizon.Game.Client.Systems.ServerModule;

using EventHorizon.Game.Client.Systems.ServerModule.Api;

public class ServerModuleScriptsModel : IServerModuleScripts
{
    public string Name { get; set; } = string.Empty;
    public string InitializeScript { get; set; } = string.Empty;
    public string DisposeScript { get; set; } = string.Empty;
    public string UpdateScript { get; set; } = string.Empty;
}
