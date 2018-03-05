using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Presentation.Views
{
    public interface IPhotoView
    {
        void Show(Photo photo);
         
        void Refresh();

        void StartFadingOut(double durationInMs);

        void Hide();
    }
}