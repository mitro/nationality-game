using System;

namespace NationalityGame.Presentation.Interactivity
{
    public interface ITicker
    {
        event Action Ticked;

        void Start();

        void Stop();
    }
}