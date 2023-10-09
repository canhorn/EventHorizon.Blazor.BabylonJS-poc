namespace EventHorizon.Game.Client.Core.Timer.Api;

using System;

public interface ITimerService
{
    void SetTimer(double millisecondInterval, Action onElapsed);
    void Clear();
}
