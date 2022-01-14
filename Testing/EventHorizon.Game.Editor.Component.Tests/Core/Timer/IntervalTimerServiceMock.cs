namespace EventHorizon.Game.Editor.Client.Core.Timer;

using System;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Timer.Api;

public class IntervalTimerServiceMock
    : IIntervalTimerService
{
    public void Dispose() { }

    public IIntervalTimerService Pause()
    {
        return this;
    }

    public IIntervalTimerService Setup(
        double millisecondInterval,
        Func<Task> onElapsed
    )
    {
        return this;
    }

    public IIntervalTimerService Start()
    {
        return this;
    }
}
