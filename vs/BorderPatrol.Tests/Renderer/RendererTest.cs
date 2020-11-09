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
        public void WithoutRectangle_Should_RenderWithinDimensions() {
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
        public void WithRectangle_Should_RenderWithinDimensions() {
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
        public void WithRectangle_Should_RenderRectangle() {
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
        public void WithPolice_Should_RenderWithRectangleAndPolice() {
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
