using FluentAssertions;
using NationalityGame.Mechanics.Scoring;
using Xunit;

namespace NationalityGame.Mechanics.Tests.Unit.Scoring
{
    public class AddingScoringStrategyTests
    {
        [Fact]
        public void Should_have_total_score_of_0_after_creation()
        {
            var strategy = new AddingScoringStrategy(10, -5);

            strategy.TotalScore.Should().Be(0);
        }

        [Fact]
        public void Should_add_score_on_correct_choice()
        {
            var strategy = new AddingScoringStrategy(10, -5);

            strategy.ChangeScore(true);

            strategy.TotalScore.Should().Be(10);
        }

        [Fact]
        public void Should_add_score_on_incorrect_choice()
        {
            var strategy = new AddingScoringStrategy(10, -5);

            strategy.ChangeScore(false);

            strategy.TotalScore.Should().Be(-5);
        }

        [Fact]
        public void Should_reset_score()
        {
            var strategy = new AddingScoringStrategy(10, -5);

            strategy.ChangeScore(false);

            strategy.TotalScore.Should().Be(-5);

            strategy.Reset();

            strategy.TotalScore.Should().Be(0);
        }
    }
}