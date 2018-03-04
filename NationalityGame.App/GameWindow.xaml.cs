using System.Windows;

namespace NationalityGame.App
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameBootstrapper _bootstrapper;

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _bootstrapper = new GameBootstrapper();

            _bootstrapper.Bootstrap(this);
        }
    }
}
