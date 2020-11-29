using NUnit.Framework;
using Scripts.Model;
using FluentAssertions;

namespace BorderPatrol.Tests.Walker {
    [TestFixture]
    class WalkerStructureTest {
        [TestCase]
        public void Walk_WhenNotRunning_DoesNotUpdatePosition() {
            // Arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(2);
            tags.YValue.Set(2);
            tags.Running.Set(false);
            var walker = new Scripts.WalkerStructure.Walker(tags);
            var expected = walker.Position;

            // Act
            walker.Walk();

            // Assert
            var actual = walker.Position;
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase(0, 0, 1, 0, TestName="Walk_WhenInTopLeftCorner_MovesRight")]
        [TestCase(1, 0, 2, 0, TestName="Walk_WhenInMiddleOfTopRow_MovesRight")]
        [TestCase(2, 0, 2, 1, TestName="Walk_WhenInTopRightCorner_MovesDown")]
        [TestCase(2, 1, 2, 2, TestName="Walk_WhenInMiddleOfRightColumn_MovesDown")]
        [TestCase(2, 2, 1, 2, TestName="Walk_WhenInBottomRightCorner_MovesLeft")]
        [TestCase(1, 2, 0, 2, TestName="Walk_WhenInMiddleOfBottomRow_MovesLeft")]
        [TestCase(0, 2, 0, 1, TestName="Walk_WhenInBottomLeftCorner_MovesUp")]
        [TestCase(0, 1, 0, 0, TestName="Walk_WhenInMiddleOfLeftColumn_MovesUp")]
        public void Walk_WhenRunning_MovesInRightDirection(int x, int y, int newX, int newY) {
            // Arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(3);
            tags.YValue.Set(3);
            tags.Running.Set(true);
            var walker = new Scripts.WalkerStructure.Walker(tags);
            walker.UpdateGrid();
            var expected = new Position(newX, newY);
            while (walker.Position.X != x || walker.Position.Y != y) {
                walker.Walk();
            }

            // Act
            walker.Walk();

            // Assert
            var actual = walker.Position;
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase]
        public void UpdateGrid_WithoutXValueAndYValue_DoesNotUpdateRectangle() {
            // Arrange
            var tags = Helpers.CreateTags();
            var walker = new Scripts.WalkerStructure.Walker(tags);

            // Act
            walker.UpdateGrid();

            // Assert
            var actual = walker.Content;
            actual.Should().Be(null);
        }

        [TestCase]
        public void UpdateGrid_WithOnlyYValue_CreatesNewSquareWithXDimensions() {
            // Arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(0);
            tags.YValue.Set(2);
            var walker = new Scripts.WalkerStructure.Walker(tags);
            var expected = new Square(2);

            // Act
            walker.UpdateGrid();

            // Assert
            var actual = walker.Content;
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase]
        public void UpdateGrid_WithOnlyXValue_CreatesNewSquareWithYDimensions() {
            // Arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(3);
            tags.YValue.Set(0);
            var walker = new Scripts.WalkerStructure.Walker(tags);
            var expected = new Square(3);

            // Act
            walker.UpdateGrid();

            // Assert
            var actual = walker.Content;
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase]
        public void UpdateGrid_WithXValueAndYValue_CreatesNewRectangleWithXYDimensions() {
            // Arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(6);
            tags.YValue.Set(4);
            var walker = new Scripts.WalkerStructure.Walker(tags);
            var expected = new Rectangle(6, 4);

            // Act
            walker.UpdateGrid();

            // Assert
            var actual = walker.Content;
            actual.Should().BeEquivalentTo(expected);
        }

        [TestCase]
        public void UpdateGrid_WhenCreatingNewRectangle_ResetsPolice() {
            // Arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(6);
            tags.YValue.Set(4);
            tags.Running.Set(true);
            var walker = new Scripts.WalkerStructure.Walker(tags);
            walker.UpdateGrid();
            walker.Walk();
            var expected = new Position(0, 0);
            walker.Position.Should().BeEquivalentTo(new Position(1, 0));

            // Act
            walker.UpdateGrid();

            // Assert
            var actual = walker.Position;
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
