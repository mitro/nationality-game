using System.Windows;
using System.Windows.Controls;

namespace NationalityGame.App.Rendering.Canvas
{
    public class ControlsView : IView
    {
        private readonly System.Windows.Controls.Canvas _canvas;

        public ControlsView(System.Windows.Controls.Canvas canvas)
        {
            _canvas = canvas;

            var stackPanel = new StackPanel
            {
                Width = 400,
                Height = 200,
            };

            System.Windows.Controls.Canvas.SetLeft(stackPanel, (_canvas.ActualWidth - stackPanel.Width) / 2);
            System.Windows.Controls.Canvas.SetTop(stackPanel, (_canvas.ActualHeight - stackPanel.Height) / 2);

            _canvas.Children.Add(stackPanel);

            var label = new Label
            {
                Content = "Your score is 45",
                FontSize = 36,
                Margin = new Thickness(0, 0, 0, 10),
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };

            stackPanel.Children.Add(label);

            var button = new Button
            {
                Content = "Play again",
                Width = 200,
                Height = 50,
            };

            stackPanel.Children.Add(button);
        }

        public void Render()
        {
            
        }
    }
}