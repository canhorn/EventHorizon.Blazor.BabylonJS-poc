namespace EventHorizon.Game.Client.Core.Timer.Model;

using System;
using System.Threading.Tasks;
using System.Timers;

using EventHorizon.Game.Client.Core.Timer.Api;

public class IntervalTimerService : IIntervalTimerService
{
    private Timer? _timer;
    private event Func<Task>? _onElapsed;

    public IIntervalTimerService Setup(
        double millisecondInterval,
        Func<Task> onElapsed
    )
    {
        _onElapsed = onElapsed;
        _timer = new Timer(millisecondInterval);
        _timer.Elapsed += NotifyTimerElapsed;
        return this;
    }

    public IIntervalTimerService Start()
    {
        _timer?.Start();
        return this;
    }

    public IIntervalTimerService Pause()
    {
        _timer?.Stop();
        return this;
    }

    public void Dispose()
    {
        _timer?.Dispose();
        _timer = null;
    }

    private async void NotifyTimerElapsed(object source, ElapsedEventArgs _)
    {
        if (_onElapsed != null)
        {
            await _onElapsed.Invoke();
        }
    }
}
