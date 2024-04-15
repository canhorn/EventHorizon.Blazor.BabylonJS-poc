namespace EventHorizon.Game.Client.Engine.Systems.Camera.Model;

using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;

internal class EmptyCamera : ICamera
{
    public long ClientId { get; } = -1;

    public void AttachControl() { }

    public Task Dispose()
    {
        return Task.CompletedTask;
    }

    public Task Initialize()
    {
        return Task.CompletedTask;
    }

    public bool IsInFrustum(IEngineMesh mesh)
    {
        return false;
    }

    public Task PostInitialize()
    {
        return Task.CompletedTask;
    }

    public void SetAsActive() { }
}
