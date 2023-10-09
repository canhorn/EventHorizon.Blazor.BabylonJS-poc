namespace EventHorizon.Game.Client.Engine.Rendering.Api;

using System;
using System.Threading.Tasks;

public interface ISceneImplementation
{
    void Dispose();
    string RegisterBeforeRender(Func<Task> afterRenderAction);
    string RegisterAfterRender(Func<Task> afterRenderAction);
    void Render();
}
