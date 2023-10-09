namespace EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Module.Api;

public interface IObjectEntity : IClientEntity
{
    long EntityId { get; }
    string Name { get; }
    string GlobalId { get; }
    string Type { get; }
    ITransform Transform { get; }
    IList<string> Tags { get; }
    IObjectEntityDetails Details { get; }

    void RegisterModule(string name, IModule module);
    T? GetModule<T>(string name)
        where T : IModule;
    public bool RemoveModule<T>(string name, [NotNullWhen(true)] out T? module)
        where T : IModule;
    Option<T> GetPropertyAsOption<T>(string name);
    void SetProperty(string name, object property);
    T? GetProperty<T>(string name);

    /// <summary>
    /// Take in details and override existing details.
    /// </summary>
    /// <param name="details">New details for Entity</param>
    /// <returns>The details set into entity.</returns>
    IObjectEntityDetails UpdateDetails(IObjectEntityDetails details);
}
