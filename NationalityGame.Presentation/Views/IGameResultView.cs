using System;

namespace NationalityGame.Presentation.Views
{
    public interface IGameResultView
    {
        event Action PlayAgainExecuted; 

        void Show(int totalScore);

        void Hide();
    }
}