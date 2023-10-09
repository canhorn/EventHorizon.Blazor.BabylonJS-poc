namespace EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;

using System;

public interface IAnimationGroup
{
    string Name { get; }

    void Pause();
    void Play(bool? loop = false);
}
