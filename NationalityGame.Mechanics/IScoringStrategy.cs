namespace NationalityGame.Mechanics
{
    public interface IScoringStrategy
    {
        int TotalScore { get; }

        void Reset();

        void Change(bool choiceIsCorrect);
    }
}