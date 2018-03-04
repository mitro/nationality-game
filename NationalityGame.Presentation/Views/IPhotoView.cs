using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Presentation.Views
{
    public interface IPhotoView
    {
        void Start(Photo photo);
         
        void Update();

        void StartFadingOut(double durationInMs);

        void Hide();
    }
}