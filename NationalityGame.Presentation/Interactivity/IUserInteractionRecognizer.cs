using System;
using System.Windows;

namespace NationalityGame.Presentation.Interactivity
{
    public interface IUserInteractionRecognizer
    {
        event Action<Vector> PanRecognized;
    }
}