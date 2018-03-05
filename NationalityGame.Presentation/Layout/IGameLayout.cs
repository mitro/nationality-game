using System.Collections.Generic;
using NationalityGame.Configuration.Settings;
using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Presentation.Layout
{
    public interface IGameLayout
    {
        List<Bucket> CreateBuckets(GameSettings settings, Board board);

        IEnumerable<Photo> CreatePhotos(GameSettings settings, Board board);
    }
}