namespace EventHorizon.Game.Client.Engine.Systems.Camera.Api;

using System;
using EventHorizon.Game.Client.Engine.Systems.Camera.Model;

public interface ICameraState
{
    ICamera ActiveCamera { get; }

    void SetActive(string name);

    void Manage(string name, ICamera camera);

    void Dispose(string name);
}
