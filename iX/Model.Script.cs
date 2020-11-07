using System.Runtime.CompilerServices;
using System.Linq;

[assembly: InternalsVisibleTo("BorderPatrol.Tests")]
namespace Model {
    internal class Rectangle {
        public int Width { get; set; }
        public int Height { get; set; }
        private static readonly char topLeftCorner = '┌';
        private static readonly char topRightCorner = '┐';
        private static readonly char bottomLeftCorner = '└';
        private static readonly char bottomRightCorner = '┘';
        private static readonly char top = '─';
        private static readonly char side = '│';
        private static readonly char bottom = '─';

        public Rectangle(int width, int height) {
            Width = width;
            Height = height;
        }

        public string Render() {
            var topRow = topLeftCorner + new string(top, Width) + topRightCorner + "\n";
            var row = side + new string(' ', Width) + side + "\n";
            var bottomRow = bottomLeftCorner + new string(bottom, Width) + bottomRightCorner;

            return topRow + string.Join("", Enumerable.Repeat(row, Height).ToArray()) + bottomRow;
        }
    }

    internal class Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
