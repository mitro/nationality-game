using Autofac;
using NationalityGame.Presentation;
using NationalityGame.Presentation.Layout;

namespace NationalityGame.App.Container.Modules
{
    public class PresentationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GamePresenter>()
                .As<IGamePresenter>();

            builder.RegisterType<GameLayout>()
                .As<IGameLayout>();
        }
    }
}