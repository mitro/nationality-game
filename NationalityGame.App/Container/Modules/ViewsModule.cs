using System.Collections.Generic;
using System.Linq;
using Autofac;
using NationalityGame.App.Views.Wpf;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Views;

namespace NationalityGame.App.Container.Modules
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();
                var settings = c.Resolve<GameSettings>();

                return new PhotoView(
                    window.GameCanvas,
                    settings.Appearance.PhotoWidth,
                    settings.Appearance.PhotoHeight);
            })
                .As<IPhotoView>();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();

                return new GameResultView(window.GameCanvas);
            })
                .As<IGameResultView>();

            builder.Register(c =>
            {
                var window = c.Resolve<GameWindow>();
                var settings = c.Resolve<GameSettings>();
                var game = c.Resolve<Game>();

                return game.Buckets
                    .Select(bucket => new BucketView(
                        bucket,
                        window.GameCanvas,
                        settings.Appearance.BucketWidth,
                        settings.Appearance.BucketHeight))
                    .ToList();
            })
                .As<IEnumerable<IBucketView>>();
        }
    }
}