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

        public event Action Ticked;

        public Ticker()
        {
            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;

            _timer = new Timer { Interval = 30 };

            _timer.Elapsed += OnTimerElapsed;
        }

        public void Start()
        {
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
            Ticked?.Invoke();
        }
    }
}