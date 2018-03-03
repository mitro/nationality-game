using System.Windows;

namespace NationalityGame.Mechanics
{
    public class Bucket : GameObject
    {
        public string Label { get; }

        public Bucket(string label, Point center) : base(center)
        {
            Label = label;
        }
    }
}