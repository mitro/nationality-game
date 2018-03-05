using System.Collections.Generic;
using System.Windows;
using FluentAssertions;
using NationalityGame.Mechanics.Domain;
using Xunit;

namespace NationalityGame.Mechanics.Tests.Unit.Domain
{
    public class GameTests
    {
        [Fact]
        public void Should_set_properties()
        {
            var board = new Board(1, 2);
            var buckets = new List<Bucket>
            {
                new Bucket("Thai", new Point()),
            };
            var photos = new List<Photo>
            {
                new Photo(new Point(), "Thai", "Path"),
            };

            var game = new Game(board, buckets, photos);

            game.Board.Should().BeSameAs(board);
            game.Buckets.Should().BeSameAs(buckets);
            game.Photos.Should().BeSameAs(photos);
        }
    }
}