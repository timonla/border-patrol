using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Scripts.Model;

namespace WhenDrawingSquare {
    [TestFixture]
    class GivenSomeWidth{
        [Test]
        public void ShouldUseCorrectSymbols() {
            // arrange
            var square = new Square(5);

            // act
            var actual = square.Draw(new Position());

            // assert
            var expected = "┌─────┐\n" +
                "│@    │\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "│     │\n" +
                "└─────┘";
            actual.Should().Be(expected);
        }

        [TestCase(1)]
        [TestCase(37)]
        public void ShouldHaveCorrectDimensions(int x) {
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
