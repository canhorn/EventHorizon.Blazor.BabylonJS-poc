namespace EventHorizon.Game.Client.Systems.ClientScripts.Model
{
    using System;
    using EventHorizon.Game.Client.Systems.ClientScripts.Api;

    public class ClientScriptsAssemblyDetails
        : IClientScriptsAssemblyDetails
    {
        public string Hash { get; set; } = string.Empty;
    }
}
