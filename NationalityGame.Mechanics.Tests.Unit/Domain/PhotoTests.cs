using System.Windows;
using FluentAssertions;
using NationalityGame.Mechanics.Domain;
using Xunit;

namespace NationalityGame.Mechanics.Tests.Unit.Domain
{
    public class PhotoTests
    {
        [Fact]
        public void Should_set_properties()
        {
            var photo = new Photo(new Point(1, 2), "Thai", "Path");

            photo.Center.Should().Be(new Point(1, 2));
            photo.Nationality.Should().Be("Thai");
            photo.ImagePath.Should().Be("Path");
        }

        [Fact]
        public void Should_clone_itself()
        {
            var photo = new Photo(new Point(1, 2), "Thai", "Path");

            var clonedPhoto = photo.Clone();

            clonedPhoto.Should().NotBeSameAs(photo);

            clonedPhoto.Center.Should().Be(new Point(1, 2));
            clonedPhoto.Nationality.Should().Be("Thai");
            clonedPhoto.ImagePath.Should().Be("Path");
        }
    }
}