using System;

namespace NationalityGame.Presentation.Interactivity
{
    public interface ITicker
    {
        event Action<double> Ticked;

        void Start();

        void Stop();
    }
}