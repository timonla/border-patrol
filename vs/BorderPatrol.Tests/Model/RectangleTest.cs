using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Model;

namespace BorderPatrol.Tests.Model {
    [TestFixture]
    class RectangleTest {
        [TestCase]
        public void Should_RenderExpectedString() {
            // arrange
            var rectangle = new Rectangle(5, 4);
            var expected = "┌─────┐\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "└─────┘";

            // act
            var actual = rectangle.Render();

            // assert
            actual.Should().Be(expected);
        }

        [TestCase(1, 1)]
        [TestCase(1, 11)]
        [TestCase(4, 1)]
        [TestCase(76, 35)]
        public void Should_WorkForDifferentSizes(int x, int y) {
            // arrange
            var rectangle = new Rectangle(x, y);

            // act
            var actual = rectangle.Render();
            var actualWidth = actual.Split('\n').ToList().Max(subString => subString.Length);
            var actualHeight = actual.Split('\n').Length;

            // assert
            actualWidth.Should().Be(x + 2);
            actualHeight.Should().Be(y + 2);
        }
    }
}
