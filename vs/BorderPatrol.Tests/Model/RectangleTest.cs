using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Model;

namespace BorderPatrol.Tests.Model {
    [TestFixture]
    class RectangleTest {
        [TestCase]
        public void Render_ReturnsCorrectLines() {
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

        [TestCase(1, 1, TestName="Render_WithMinimumSize_ReturnsRightAmountOfLines")]
        [TestCase(1, 11, TestName="Render_WithHeightGreaterWidth_ReturnsRightAmountOfLines")]
        [TestCase(4, 1, TestName="Render_WithWidthGreaterHeight_ReturnsRightAmountOfLines")]
        [TestCase(74, 37, TestName="Render_WithMaximumSize_ReturnsRightAmountOfLines")]
        public void Render_ForDifferentSizes_ReturnsRightAmountOfLines(int x, int y) {
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
