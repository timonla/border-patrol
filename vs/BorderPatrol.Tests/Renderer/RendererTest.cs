using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System;
using Renderer;
using Model;

namespace BorderPatrol.Tests.RendererTest {
    [TestFixture]
    class RendererTest {
        [TestCase]
        public void Render_WithoutRectangle_RendersWithinDimensions() {
            // arrange
            var expectedWidth = 76;
            var expectedHeight = 39;

            // act
            var actual = TextRenderer.Render();

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
            Console.WriteLine(rectangle.Render());
            var expectedWidth = 76;
            var expectedHeight = 39;

            // act
            var actual = TextRenderer.Render(rectangle);

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
            var expected = rectangle.Render().Split('\n');

            // act
            var actual = TextRenderer.Render(rectangle).Split('\n');

            // assert
            expected.ToList()
                .Select((subString, index) => (subString, index)).ToList()
                .ForEach(item => actual[item.index].Should().Be(item.subString));
        }

        [TestCase]
        public void Render_WithRectangleAndPolice_RendersWithRectangleAndPolice() {
            // arrange
            var rectangle = new Rectangle(4, 7);
            var police = new Position(3, 2);
            var expected = rectangle.Render().Split('\n');
            expected[3] = "│   @│";

            // act
            var actual = TextRenderer.Render(rectangle, police).Split('\n');

            // assert
            expected.ToList()
                .Select((subString, index) => (subString, index)).ToList()
                .ForEach(item => actual[item.index].Should().Be(item.subString));
        }
    }
}
