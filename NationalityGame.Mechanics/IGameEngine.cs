using System;
using System.Windows;
using NationalityGame.Mechanics.Domain;

namespace NationalityGame.Mechanics
{
    public interface IGameEngine
    {
        event Action<int> RoundFinished;

        event Action TickProcessed;

        event Action<Photo> NextPhotoRun;

        event Action<double> BucketChosen;

        event Action<int> CurrentScoreChanged; 

        void StartNewRound();

        void ProcessTick(double msSinceLastTick);

        void ProcessPan(Vector vector);
    }
}