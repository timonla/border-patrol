using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using Scripts.Model;

namespace WhenDrawingCanvas {
    [TestFixture]
    class GivenNoContent {
        [Test]
        public void ShouldHaveCorrectDimensions() {
            // arrange
            var expectedWidth = 76;
            var expectedHeight = 39;

            // act
            var actual = new Canvas(74, 37).Draw();

            // assert
            var actualWidth = actual.Split('\n').ToList().Max(subString => subString.Length);
            var actualHeight = actual.Split('\n').Length;

            actualWidth.Should().BeLessOrEqualTo(expectedWidth);
            actualHeight.Should().BeLessOrEqualTo(expectedHeight);
        }
    }

    [TestFixture]
    class GivenSomeContent {
        [Test]
        public void ShouldIncludeContent() {
            // arrange
            var rectangle = new Rectangle(3, 7);
            var content = "Some text\nto include";

            // act
            var actual = new Canvas(5, 9).Draw(content).Split('\n');

            // assert
            content.Split('\n').ToList()
                .Select((subString, index) => (subString, index)).ToList()
                .ForEach(item => actual[item.index].Should().Be(item.subString));
        }

        [Test]
        public void ShouldHaveCorrectDimensions() {
            // act
            var actual = new Canvas(74, 37).Draw();
            var actualWidth = actual.Split('\n').ToList().Max(subString => subString.Length);
            var actualHeight = actual.Split('\n').Length;

            // assert
            var expectedWidth = 76;
            var expectedHeight = 39;
            actualWidth.Should().BeLessOrEqualTo(expectedWidth);
            actualHeight.Should().BeLessOrEqualTo(expectedHeight);
        }
    }
}

