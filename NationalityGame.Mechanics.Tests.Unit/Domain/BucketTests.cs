using System.Windows;
using FluentAssertions;
using NationalityGame.Mechanics.Domain;
using Xunit;

namespace NationalityGame.Mechanics.Tests.Unit.Domain
{
    public class BucketTests
    {
        [Fact]
        public void Should_set_properties()
        {
            var bucket = new Bucket("Thai", new Point(1, 2));

            bucket.Nationality.Should().Be("Thai");
            bucket.Center.Should().Be(new Point(1, 2));
        }

        [Fact]
        public void Should_match_photo_if_nationalities_equal()
        {
            var bucket = new Bucket("Thai", new Point(1, 2));

            bucket.Matches(new Photo(new Point(), "Thai", string.Empty)).Should().BeTrue();
        }

        [Fact]
        public void Should_match_photo_if_nationalities_equal_but_different_cases()
        {
            var bucket = new Bucket("Thai", new Point(1, 2));

            bucket.Matches(new Photo(new Point(), "thai", string.Empty)).Should().BeTrue();
        }

        [Fact]
        public void Should_not_match_photo_if_nationalities_not_equal()
        {
            var bucket = new Bucket("Thai", new Point(1, 2));

            bucket.Matches(new Photo(new Point(), "Chinese", string.Empty)).Should().BeFalse();
        }
    }
}