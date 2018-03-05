using System;
using System.Windows;
using System.Windows.Input;
using Autofac;
using NationalityGame.App.Bootstrapping;
using NationalityGame.App.Container;
using NationalityGame.Configuration;
using NationalityGame.Configuration.Settings;
using NationalityGame.Presentation;

namespace NationalityGame.App
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameBootstrapper _bootstrapper;

        private ILifetimeScope _rootLifetimeScope;

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var settings = TryReadSettings();

            var container = AutofacContainer.Build(this, settings);

            _rootLifetimeScope = container.BeginLifetimeScope();

            var gamePresenter = _rootLifetimeScope.Resolve<IGamePresenter>();

            gamePresenter.Start();

            //    _bootstrapper = new GameBootstrapper();

            //_bootstrapper.Bootstrap(this);
        }

        private GameSettings TryReadSettings()
        {
            var settingsReader = new SettingsReader();

            try
            {
                return settingsReader.Read();
            }
            catch (Exception ex)
            {
                var message = $"Invalid settings. Fix them and restart the game.\n{ex.Message}";

                MessageBox.Show(message, "Fatal error", MessageBoxButton.OK, MessageBoxImage.Error);

                Close();

                return null;
            }
        }

        private void OnCloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _rootLifetimeScope?.Dispose();
        }
    }
}
