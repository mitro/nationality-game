namespace NationalityGame.Mechanics
{
    public class ScoringStrategy : IScoringStrategy
    {
        private readonly int _correctChoicePoints;

        private readonly int _incorrectChoicePoints;

        public int TotalScore { get; private set; }

        public ScoringStrategy(int correctChoicePoints, int incorrectChoicePoints)
        {
            _correctChoicePoints = correctChoicePoints;
            _incorrectChoicePoints = incorrectChoicePoints;
        }

        public void Reset()
        {
            TotalScore = 0;
        }

        public void Change(bool choiceIsCorrect)
        {
            TotalScore += choiceIsCorrect ?
                _correctChoicePoints :
                _incorrectChoicePoints;
        }
    }
}