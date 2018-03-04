using System;
using System.Windows;

namespace NationalityGame.Presentation.Interacting
{
    public interface IUserInteractionRecognizer
    {
        event Action<Vector> PanRecognized;
    }
}