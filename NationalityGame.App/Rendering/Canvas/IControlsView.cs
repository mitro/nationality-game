using System;

namespace NationalityGame.App.Rendering.Canvas
{
    public interface IControlsView
    {
        event Action NewGameRequested; 

        void Show(int totalScore);

        void Hide();
    }
}