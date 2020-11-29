using System.Runtime.CompilerServices;
using System.Linq;
using System;

[assembly: InternalsVisibleTo("BorderPatrol.Tests")]
namespace Scripts.Model {
    /* Workaround because Tuples are not available in Compact Framework 3.5
     * https://stackoverflow.com/questions/4312218/error-with-tuple-in-c-sharp-2008
     */
    internal class Tuple<T, U> {
        public T Item1 { get; private set; }
        public U Item2 { get; private set; }

        public Tuple(T item1, U item2) {
            Item1 = item1;
            Item2 = item2;
        }
    }

    internal static class Tuple {
        public static Tuple<T, U> Create<T, U>(T item1, U item2) {
            return new Tuple<T, U>(item1, item2);
        }
    }

    internal class Canvas {
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public Canvas(int width, int height) {
            Width = width;
            Height = height;
        }

        public string Draw(string content = "") {
            var emptyRow = new string(' ', Width);
            var sb = Enumerable.Repeat(emptyRow, Height).ToList();
            sb = sb.Select(_ => emptyRow).ToList();

            if (content != "") {
                var contentRows = content.Split('\n');

                contentRows
                    .Select((row, index) => Tuple.Create<int, string>(index, row)).ToList()
                    .ForEach(tuple => sb[tuple.Item1] = tuple.Item2);
            }

            return string.Join("\n", sb.ToArray());
        }
    }

    internal class Position {
        public int X { get; set; }
        public int Y { get; set; }

        public Position() {
            X = 0;
            Y = 0;
        }

        public Position(int x, int y) {
            X = x;
            Y = y;
        }
    }

    internal abstract class Content {
        protected static readonly char topLeftCorner = '┌';
        protected static readonly char topRightCorner = '┐';
        protected static readonly char bottomLeftCorner = '└';
        protected static readonly char bottomRightCorner = '┘';
        protected static readonly char top = '─';
        protected static readonly char side = '│';
        protected static readonly char bottom = '─';
        protected static readonly char person = '@';

        public abstract string Draw(Position position);
        public abstract Position GetNextPosition(Position position);
    }

    internal class Rectangle : Content {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle(int width, int height) {
            Width = width;
            Height = height;
        }

        public override string Draw(Position position) {
            var topRow = topLeftCorner + new string(top, Width) + topRightCorner + "\n";
            var row = side + new string(' ', Width) + side + "\n";
            var bottomRow = bottomLeftCorner + new string(bottom, Width) + bottomRightCorner;

            var result = topRow + string.Join("", Enumerable.Repeat(row, Height).ToArray()) + bottomRow;

            var resultRows = result.Split('\n');

            if (position != null) {
                resultRows[position.Y + 1] = resultRows[position.Y + 1]
                    .Remove(position.X + 1, 1)
                    .Insert(position.X + 1, person.ToString());
            }

            return string.Join("\n", resultRows);
        }

        public override Position GetNextPosition(Position position) {
            int x = position.X;
            int y = position.Y;

            // Update police position
            if (y == 0) {
                // In top row
                if (x == Width - 1) {
                    // In end corner
                    y += 1;
                } else {
                    // Along the way
                    x += 1;
                }
            } else if (y == Height - 1) {
                // In bottom row
                if (x == 0) {
                    // In end corner
                    y -= 1;
                } else {
                    // Along the way
                    x -= 1;
                }
            } else if (x == 0) {
                // In left column
                y -= 1;
            } else {
                // In right column
                y += 1;
            }

            return new Position(x, y);
        }
    }

    internal class Square : Content {
        public int Width { get; set; }

        public Square(int width) {
            Width = width;
        }

        public override string Draw(Position position) {
            var topRow = topLeftCorner + new string(top, Width) + topRightCorner + "\n";
            var row = side + new string(' ', Width) + side + "\n";
            var bottomRow = bottomLeftCorner + new string(bottom, Width) + bottomRightCorner;

            var result = topRow + string.Join("", Enumerable.Repeat(row, Width).ToArray()) + bottomRow;

            var resultRows = result.Split('\n');

            if (position != null) {
                resultRows[position.Y + 1] = resultRows[position.Y + 1]
                    .Remove(position.X + 1, 1)
                    .Insert(position.X + 1, person.ToString());
            }

            return string.Join("\n", resultRows);
        }

        public override Position GetNextPosition(Position position) {
            int x = position.X;
            int y = position.Y;

            if (y == 0) {
                // In top row
                if (x == Width - 1) {
                    // In end corner
                    y += 1;
                } else {
                    // Along the way
                    x += 1;
                }
            } else if (y == Width - 1) {
                // In bottom row
                if (x == 0) {
                    // In end corner
                    y -= 1;
                } else {
                    // Along the way
                    x -= 1;
                }
            } else if (x == 0) {
                // In left column
                y -= 1;
            } else {
                // In right column
                y += 1;
            }

            return new Position(x, y);
        }
    }
}
