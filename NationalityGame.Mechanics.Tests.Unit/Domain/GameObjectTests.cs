using System;
using System.Windows;
using FluentAssertions;
using NationalityGame.Mechanics.Domain;
using Xunit;

namespace NationalityGame.Mechanics.Tests.Unit.Domain
{
    public class GameObjectTests
    {
        [Fact]
        public void Should_set_properties()
        {
            var gameObject = new Photo(new Point(1, 2), "1", "1") as GameObject;

            gameObject.Center.Should().Be(new Point(1, 2));
        }

        [Fact]
        public void Should_get_vector_to_another_object()
        {
            var gameObject = new Photo(new Point(1, 2), "1", "1") as GameObject;

            var anotherGameObject = new Photo(new Point(5, 7), "1", "1") as GameObject;

            var vector = gameObject.GetVectorTo(anotherGameObject);

            vector.X.Should().Be(4);
            vector.Y.Should().Be(5);
        }

        [Fact]
        public void Should_move_along_movement_vector()
        {
            var gameObject = new Photo(new Point(1, 1), "1", "1") as GameObject;
            
            gameObject.SetMovementVector(new Vector(2, 2));

            gameObject.Move(Math.Sqrt(2));

            gameObject.Center.X.Should().BeInRange(1.99, 2.01);
            gameObject.Center.Y.Should().BeInRange(1.99, 2.01);
        }
    }
}