using System;

namespace NationalityGame.Mechanics
{
    public class GameBuilder
    {
        public Game Build(double boardWidth, double boardHeight)
        {
            var game = new Game(
                boardWidth,
                boardHeight);

            return game;
        }
    }
}