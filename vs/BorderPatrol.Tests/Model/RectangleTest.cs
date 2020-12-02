using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Scripts.Model;

namespace WhenDrawingRectangle {
    [TestFixture]
    class GivenSomeWidthAndHeight {
        [Test]
        public void ShouldUseCorrectSymbols() {
            // arrange
            var rectangle = new Rectangle(5, 4);

            // act
            var actual = rectangle.Draw(new Position());

            // assert
            var expected = "┌─────┐\n" +
                "│@    │\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "└─────┘";
            actual.Should().Be(expected);
        }

        [TestCase(1, 1)]
        [TestCase(1, 11)]
        [TestCase(4, 1)]
        [TestCase(74, 37)]
        public void ShouldHaveCorrectDimensions(int x, int y) {
            // arrange
            var rectangle = new Rectangle(x, y);

            // act
            var actual = rectangle.Draw(new Position());

            // assert
            var actualWidth = actual.Split('\n').ToList().Max(subString => subString.Length);
            var actualHeight = actual.Split('\n').Length;
            actualWidth.Should().Be(x + 2);
            actualHeight.Should().Be(y + 2);
        }
    }
}
