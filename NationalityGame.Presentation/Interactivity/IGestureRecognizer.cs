using System;
using System.Windows;

namespace NationalityGame.Presentation.Interactivity
{
    public interface IGestureRecognizer
    {
        event Action<Vector> PanRecognized;
    }
}