using System.Windows;
using FluentAssertions;
using NationalityGame.Mechanics.Domain;
using Xunit;

namespace NationalityGame.Mechanics.Tests.Unit.Domain
{
    public class BoardTests
    {
        [Fact]
        public void Should_set_dimensions()
        {
            var board = new Board(10, 20);

            board.Width.Should().Be(10);
            board.Height.Should().Be(20);
        }

        [Fact]
        public void Should_check_photo_is_inside()
        {
            PhotoShouldBeInside(10, 20, 0, 0);
            PhotoShouldBeInside(10, 20, 5, 5);
            PhotoShouldBeInside(10, 20, 10, 20);
        }

        [Fact]
        public void Should_check_photo_is_outside()
        {
            PhotoShouldBeOutside(10, 20, -1, 0);
            PhotoShouldBeOutside(10, 20, 5, -1);
            PhotoShouldBeOutside(10, 20, 5, 21);
            PhotoShouldBeOutside(10, 20, 11, 5);
        }

        private void PhotoShouldBeInside(double boardWidth, double boardHeight, double pointX, double pointY)
        {
            var board = new Board(boardWidth, boardHeight);

            var photo = new Photo(new Point(pointX, pointY), string.Empty, string.Empty);

            board.PhotoIsOutside(photo).Should().BeFalse();
        }

        private void PhotoShouldBeOutside(double boardWidth, double boardHeight, double pointX, double pointY)
        {
            var board = new Board(boardWidth, boardHeight);

            var photo = new Photo(new Point(pointX, pointY), string.Empty, string.Empty);

            board.PhotoIsOutside(photo).Should().BeTrue();
        }
    }
}