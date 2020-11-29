using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Scripts.Model;

namespace BorderPatrol.Tests.Model {
    [TestFixture]
    class SquareTest {
        [TestCase]
        public void Render_ReturnsCorrectLines() {
            // arrange
            var square = new Square(5);
            var expected = "┌─────┐\n" +
                "│@    │\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "└─────┘";

            // act
            var actual = square.Draw(new Position());

            // assert
            actual.Should().Be(expected);
        }

        [TestCase(1, TestName="Render_WithMinimumSize_ReturnsRightAmountOfLines")]
        [TestCase(37, TestName="Render_WithMaximumSize_ReturnsRightAmountOfLines")]
        public void Render_ForDifferentSizes_ReturnsRightAmountOfLines(int x) {
            // arrange
            var square = new Square(x);

            // act
            var actual = square.Draw(new Position());
            var actualWidth = actual.Split('\n').ToList().Max(subString => subString.Length);
            var actualHeight = actual.Split('\n').Length;

            // assert
            actualWidth.Should().Be(x + 2);
            actualHeight.Should().Be(x + 2);
        }
    }
}
