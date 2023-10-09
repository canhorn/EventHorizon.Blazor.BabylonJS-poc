namespace EventHorizon.Game.Client.Engine.Lifecycle.Api;

using System.Threading.Tasks;

public interface IServiceEntity
{
    /// <summary>
    /// Controls the call order of Initialize and Dispose for all Service Entires.
    ///
    /// For Initialize call Lowest number goes First.
    /// For Dispose call Lowest Number goes Last.
    /// </summary>
    int Priority { get; }
    Task Initialize();
    Task Dispose();
}
