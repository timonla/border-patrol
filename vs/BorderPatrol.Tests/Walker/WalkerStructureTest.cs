using NUnit.Framework;
using Scripts.Model;
using FluentAssertions;
using BorderPatrol.Tests.Walker;
using Scripts.WalkerStructure;

namespace WhenWalking {
    [TestFixture]
    class GivenNotActive {
        [Test]
        public void ShouldNotUpdatePosition() {
            // arrange
            var tags = Helpers.CreateTags();
            tags.XValue.Set(2);
            tags.YValue.Set(2);
            tags.Running.Set(false);
            var walker = new Walker(tags);
            var oldPosition = walker.Position;

            // act
            walker.Walk();

            // assert
            var actual = walker.Position;
            var expected = oldPosition;
            actual.Should().BeEquivalentTo(expected);
        }
    }

    [TestFixture]
    class GivenIsActive {
        public static Walker walker;

        [SetUp]
        public void SetUp() {
            var tags = Helpers.CreateTags();
            tags.XValue.Set(3);
            tags.YValue.Set(3);
            tags.Running.Set(true);
            walker = new Walker(tags);
            walker.UpdateGrid();
        }

        public void ShouldMoveInRightDirection(int x, int y, int newX, int newY) {
            // arrange
            while (walker.Position.X != x || walker.Position.Y != y) {
                walker.Walk();
            }

            // act
            walker.Walk();

            // assert
            var actual = walker.Position;
            var expected = new Position(newX, newY);
            actual.Should().BeEquivalentTo(expected);
        }

        [TestFixture]
        class InTopLeftCorner : GivenIsActive {
            [Test]
            public void ShouldMoveRight() => ShouldMoveInRightDirection(0, 0, 1, 0);
        }

        [TestFixture]
        class InBottomLeftCorner : GivenIsActive {
            [Test]
            public void ShouldMoveUp() => ShouldMoveInRightDirection(0, 2, 0, 1);
        }

        [TestFixture]
        class InMiddleOfBottomRow : GivenIsActive {
            [Test]
            public void ShouldMoveLeft() => ShouldMoveInRightDirection(1, 2, 0, 2);
        }

        [TestFixture]
        class InBottomRightCorner : GivenIsActive {
            [Test]
            public void ShouldMoveLeft() => ShouldMoveInRightDirection(2, 2, 1, 2);
        }

        [TestFixture]
        class InMiddleOfRightColumn : GivenIsActive {
            [Test]
            public void ShouldMoveDown() => ShouldMoveInRightDirection(2, 1, 2, 2);
        }

        [TestFixture]
        class InMiddleOfTopRow : GivenIsActive{
            [Test]
            public void ShouldMoveRight() => ShouldMoveInRightDirection(1, 0, 2, 0);
        }

        [TestFixture]
        class InTopRightCorner : GivenIsActive{
            [Test]
            public void ShouldMoveDown() => ShouldMoveInRightDirection(2, 0, 2, 1);
        }

        [TestFixture]
        class InMiddleOfLeftColumn : GivenIsActive{
            [Test]
            public void ShouldMoveUp() => ShouldMoveInRightDirection(0, 1, 0, 0);
        }
    }
}

namespace WhenUpdatingGrid {
    [TestFixture]
    class GivenXValue {
        public static WalkerTags tags;
        public static Walker walker;


        [SetUp]
        public virtual void SetUp() {
            tags = Helpers.CreateTags();
            tags.XValue.Set(2);
            walker = new Walker(tags);
        }

        public virtual void ShouldResetWalkerPosition() {
            // arrange
            tags.Running.Set(true);
            walker.UpdateGrid();
            walker.Walk();
            var expected = new Position(0, 0);
            walker.Position.Should().BeEquivalentTo(new Position(1, 0));

            // act
            walker.UpdateGrid();

            // assert
            var actual = walker.Position;
            actual.Should().BeEquivalentTo(expected);
        }

        [TestFixture]
        class NoYValue : GivenXValue {
            [SetUp]
            public override void SetUp() {
                base.SetUp();
                tags.YValue.Set(0);
            }

            [Test]
            public void ShouldCreateSquareWithXDimensions() {
                // act
                walker.UpdateGrid();

                // assert
                var actual = walker.Content;
                var expected = new Square(2);
                actual.Should().BeEquivalentTo(expected);
            }

            [Test]
            public override void ShouldResetWalkerPosition() => base.ShouldResetWalkerPosition();
        }

        [TestFixture]
        class YValue : GivenXValue {
            [SetUp]
            public override void SetUp() {
                base.SetUp();
                tags.YValue.Set(4);
            }

            [Test]
            public void ShouldCreateNewRectangleWithXAndYDimensions() {
                // act
                walker.UpdateGrid();

                // assert
                var actual = walker.Content;
                var expected = new Rectangle(2, 4);
                actual.Should().BeEquivalentTo(expected);
            }

            [Test]
            public override void ShouldResetWalkerPosition() => base.ShouldResetWalkerPosition();
        }
    }

    [TestFixture]
    class GivenNoXValue {
        public static WalkerTags tags;
        public static Walker walker;

        [SetUp]
        public virtual void SetUp() {
            tags = Helpers.CreateTags();
            tags.XValue.Set(0);
            walker = new Walker(tags);
        }

        public virtual void ShouldResetWalkerPosition() {
            // arrange
            tags.Running.Set(true);
            walker.UpdateGrid();
            walker.Walk();
            walker.Position.Should().BeEquivalentTo(new Position(1, 0));

            // act
            walker.UpdateGrid();

            // assert
            var actual = walker.Position;
            var expected = new Position(0, 0);
            actual.Should().BeEquivalentTo(expected);
        }

        [TestFixture]
        class NoYValue : GivenNoXValue {
            [SetUp]
            public override void SetUp() {
                base.SetUp();
                tags.YValue.Set(0);
            }

            [Test]
            public void ShouldNotUpdateRectangle() {
                // act
                walker.UpdateGrid();

                // assert
                var actual = walker.Content;
                actual.Should().Be(null);
            }
        }


        [TestFixture]
        class GivenYValue : GivenNoXValue {
            [SetUp]
            public override void SetUp() {
                base.SetUp();
                tags.YValue.Set(3);
            }

            [Test]
            public void ShouldCreateSquareWithXDimensions() {
                // act
                walker.UpdateGrid();

                // assert
                var actual = walker.Content;
                var expected = new Square(3);
                actual.Should().BeEquivalentTo(expected);
            }

            [Test]
            public override void ShouldResetWalkerPosition() => base.ShouldResetWalkerPosition();
        }
    }
}
