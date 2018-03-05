using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Layout;

namespace NationalityGame.App.Container.Modules
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();
                var settings = c.Resolve<GameSettings>();
                var layout = c.Resolve<IGameLayout>();

                var board = new Board(
                    window.GameCanvas.ActualWidth,
                    window.GameCanvas.ActualHeight);

                var buckets = layout.CreateBuckets(settings, board);

                var photos = layout.CreatePhotos(settings, board);

                return new Game(board, buckets, photos);
            })
                .SingleInstance();
        }
    }
}