﻿namespace EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Model;

using System;
using BabylonJS;
using EventHorizon.Game.Client.Systems.Entity.Modules.Animation.Api;

public class BabylonJSAnimationGroup : IAnimationGroup
{
    private readonly AnimationGroup _animationGroup;

    public string Name => _animationGroup.name;

    public BabylonJSAnimationGroup(AnimationGroup animationGroup)
    {
        _animationGroup = animationGroup;
    }

    public void Pause()
    {
        _animationGroup.pause();
    }

    public void Play(bool? loop = false)
    {
        _animationGroup.play(loop);
    }
}
