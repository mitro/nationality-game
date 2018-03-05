using Autofac;
using NationalityGame.Presentation;

namespace NationalityGame.App.Container.Modules
{
    public class PresentationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GamePresenter>()
                .As<IGamePresenter>();
        }
    }
}