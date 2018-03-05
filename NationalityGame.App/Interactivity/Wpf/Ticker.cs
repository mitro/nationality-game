using System;
using System.ComponentModel;
using System.Timers;
using NationalityGame.Presentation.Interactivity;

namespace NationalityGame.App.Interactivity.Wpf
{
    public class Ticker : ITicker
    {
        private readonly BackgroundWorker _backgroundWorker;

        private readonly Timer _timer;

        private DateTime _lastTickAt;

        public Ticker(int tickIntervalInMs)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;

            _timer = new Timer
            {
                Interval = tickIntervalInMs
            };

            _timer.Elapsed += OnTimerElapsed;
        }

        public event Action<double> Ticked;

        public void Start()
        {
            _lastTickAt = DateTime.Now;

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs args)
        {
            if (_backgroundWorker.IsBusy)
            {
                return;
            }

            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var msSinceLastTick = (DateTime.Now - _lastTickAt).TotalMilliseconds;

            Ticked?.Invoke(msSinceLastTick);

            _lastTickAt = DateTime.Now;
        }
    }
}