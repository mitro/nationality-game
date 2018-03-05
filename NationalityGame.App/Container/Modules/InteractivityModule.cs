using Autofac;
using NationalityGame.App.Interactivity.Wpf;
using NationalityGame.Configuration.Settings;
using NationalityGame.Presentation.Interactivity;

namespace NationalityGame.App.Container.Modules
{
    public class InteractivityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();

                return new UserInteractionRecognizer(window.GameCanvas);
            })
                .As<IUserInteractionRecognizer>();

            builder.Register(c =>
            {
                var settings = c.Resolve<GameSettings>();

                return new Ticker(settings.Appearance.TickIntervalInMs);
            })
                .As<ITicker>();
        }
    }
}