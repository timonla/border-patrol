using NUnit.Framework;
using Scripts.Model;
using FluentAssertions;
using BorderPatrol.Tests.Walker;
using Scripts.WalkerStructure;

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
