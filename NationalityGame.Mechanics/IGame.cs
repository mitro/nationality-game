using System;
using System.Windows;
using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Mechanics
{
    public interface IGame
    {
        event Action<int> RoundFinished;

        event Action TickProcessed;

        event Action<Photo> NextPhotoRun;

        event Action<Bucket, double> BucketChosen;

        void StartNewRound();

        void ProcessTick(double msSinceLastTick);

        void ProcessPan(Vector vector);
    }
}