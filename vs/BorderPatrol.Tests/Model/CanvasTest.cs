using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System;
using Scripts.Model;

namespace BorderPatrol.Tests.CanvasTest {
    [TestFixture]
    class CanvasTest {
        [TestCase]
        public void Render_WithoutRectangle_RendersWithinDimensions() {
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

        [TestCase]
        public void Render_WithRectangle_RendersWithinDimensions() {
            // arrange
            var rectangle = new Rectangle(14, 7);
            Console.WriteLine(rectangle.Draw(new Position()));
            var expectedWidth = 76;
            var expectedHeight = 39;

            // act
            var actual = new Canvas(74, 37).Draw(rectangle.Draw(new Position()));

            // assert
            var actualWidth = actual.Split('\n').ToList().Max(subString => subString.Length);
            var actualHeight = actual.Split('\n').Length;

            actualWidth.Should().BeLessOrEqualTo(expectedWidth);
            actualHeight.Should().BeLessOrEqualTo(expectedHeight);
        }

        [TestCase]
        public void Render_WithRectangle_RendersRectangle() {
            // arrange
            var rectangle = new Rectangle(3, 7);
            var expected = rectangle.Draw(new Position()).Split('\n');

            // act
            var actual = new Canvas(5, 9).Draw(rectangle.Draw(new Position())).Split('\n');

            // assert
            expected.ToList()
                .Select((subString, index) => (subString, index)).ToList()
                .ForEach(item => actual[item.index].Should().Be(item.subString));
        }

        [TestCase]
        public void Render_WithRectangleAndPolice_RendersWithRectangleAndPolice() {
            // arrange
            var rectangle = new Rectangle(4, 7);
            var expected = rectangle.Draw(new Position(3, 2));

            // act
            var actual = new Canvas(6, 9).Draw(expected).Split('\n');

            // assert
            expected.Split('\n').ToList()
                .Select((subString, index) => (subString, index)).ToList()
                .ForEach(item => actual[item.index].Should().Be(item.subString));
        }
    }
}
