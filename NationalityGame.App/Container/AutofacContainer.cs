using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using NationalityGame.App.Container.Modules;
using NationalityGame.App.Interactivity.Wpf;
using NationalityGame.App.Views.Wpf;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation;
using NationalityGame.Presentation.Interactivity;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Container
{
    public static class AutofacContainer
    {
        public static IContainer Build(GameWindow gameWindow, GameSettings gameSettings)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(gameWindow);

            builder.RegisterInstance(gameSettings);

            builder.RegisterModule<ViewsModule>();

            builder.RegisterModule<InteractivityModule>();

            builder.RegisterModule<MechanicsModule>();

            builder.RegisterModule<PresentationModule>();

            builder.RegisterModule<DomainModule>();

            return builder.Build();
        }
    }
}