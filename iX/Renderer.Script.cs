using System.Linq;
using Model;

namespace Renderer {
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

    internal class TextRenderer {
        public static int Width = 78;
        public static int Height = 37;
        private static readonly char person = '웃';

        public static string Render(Rectangle rectangle = null, Position police = null) {
            var emptyRow = new string(' ', Width);
            var sb = Enumerable.Repeat(emptyRow, Height).ToList();
            sb = sb.Select(_ => emptyRow).ToList();

            if (rectangle != null) {
                var rectangleRows = rectangle.Render().Split('\n');

                if (police != null) {
                    rectangleRows[police.Y] = rectangleRows[police.Y].Remove(police.X - 1, 1);
                    rectangleRows[police.Y] = rectangleRows[police.Y].Insert(police.X, person.ToString());
                }

                rectangleRows
                    .Select((row, index) => Tuple.Create<int, string>(index, row)).ToList()
                    .ForEach(tuple => sb[tuple.Item1] = tuple.Item2);
            }

            return string.Join("\n", sb.ToArray());
        }
    }
}
