namespace EventHorizon.Game.Client.Core.Timer.Api;

using System;
using System.Threading.Tasks;

public interface IIntervalTimerService
{
    IIntervalTimerService Setup(double millisecondInterval, Func<Task> onElapsed);
    IIntervalTimerService Start();
    IIntervalTimerService Pause();
    void Dispose();
}
