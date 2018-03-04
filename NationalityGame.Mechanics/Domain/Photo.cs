using System.Windows;

namespace NationalityGame.Mechanics.Domain
{
    public class Photo : GameObject
    {
        public string Nationality { get; }

        public Photo(Point center, string nationality) : base(center)
        {
            Nationality = nationality;
        }

        public Photo Clone()
        {
            return new Photo(Center, Nationality);
        }
    }
}