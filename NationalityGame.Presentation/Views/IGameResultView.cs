using System;

namespace NationalityGame.Presentation.Views
{
    public interface IGameResultView
    {
        event Action PlayAgainRequested; 

        void Show(int totalScore);

        void Hide();
    }
}