using Autofac;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Mechanics.Scoring;

namespace NationalityGame.App.Container.Modules
{
    public class MechanicsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GameEngine>()
                .As<IGameEngine>();

            builder.Register(c =>
            {
                var settings = c.Resolve<GameSettings>();

                return new AdditionScoringStrategy(
                    settings.Rules.CorrectChoiceScore,
                    settings.Rules.IncorrectChoiceScore);
            })
                .As<IScoringStrategy>();

            builder.Register(c =>
            {
                var game = c.Resolve<Game>();
                var settings = c.Resolve<GameSettings>();

                var velocityInPxPerMs = game.Board.Height / settings.Appearance.PhotoRunTimeInMs;

                return new GameEngine.Settings(
                    settings.Rules.ShufflePhotos,
                    velocityInPxPerMs);
            });
        }
    }
}