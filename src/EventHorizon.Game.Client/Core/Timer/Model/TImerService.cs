namespace EventHorizon.Game.Client.Core.Timer.Model
{
    using System;
    using System.Timers;
    using EventHorizon.Game.Client.Core.Timer.Api;

    public class TimerService
        : ITimerService
    {
        private Timer? _timer;
        private event Action? _onElapsed;

        public void SetTimer(
            double millisecondInterval,
            Action onElapsed
        )
        {
            _onElapsed = onElapsed;
            _timer = new Timer(
                millisecondInterval
            );
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.Start();
        }

        public void Clear()
        {
            _timer?.Dispose();
            _timer = null;
        }

        private void NotifyTimerElapsed(
            object source, 
            ElapsedEventArgs _
        )
        {
            _onElapsed?.Invoke();
            _timer?.Dispose();
        }
    }
}
