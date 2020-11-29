using System;
using System.Runtime.CompilerServices;
using Scripts.Model;

[assembly: InternalsVisibleTo("BorderPatrol.Tests")]
namespace Scripts.WalkerStructure {
	internal class VariableReference<T> {
		public Func<T> Get { get; private set; }
		public Action<T> Set { get; private set; }
		public VariableReference(Func<T> getter, Action<T> setter) {
			Get = getter;
			Set = setter;
		}
	}

	internal class WalkerTags {
		public readonly VariableReference<int> XValue;
		public readonly VariableReference<int> YValue;
		public readonly VariableReference<string> RenderOutput;
		public readonly VariableReference<bool> Running;

		public WalkerTags(
			VariableReference<int> xValue = null,
			VariableReference<int> yValue = null,
			VariableReference<string> renderOutput = null,
			VariableReference<bool> running = null
		) {
			XValue = xValue;
			YValue = yValue;
			RenderOutput = renderOutput;
			Running = running;
		}
	}

	internal class Walker {
		private readonly WalkerTags tags;
		public Canvas Canvas { get; private set; }
		public Content Content { get; private set; }
		public Position Position { get; private set; }
		
		public Walker(WalkerTags tags) {
			this.tags = tags;
			Canvas = new Canvas(74, 37);
			Position = new Position();
		}
		
		public void Walk() {
			if (Content != null) {
				if (tags.Running.Get()) {
			    	Position = Content.GetNextPosition(Position);
			    }

				tags.RenderOutput.Set(Canvas.Draw(Content.Draw(Position)));
            }
		}

		public void UpdateGrid() {
			var x = tags.XValue.Get();
			var y = tags.YValue.Get();
			if (x == 0 && y == 0) return;
			if (y == 0) {
				Content = new Square(x);
			} else if (x == 0) { 
				Content = new Square(y);
			} else { 
				Content = new Rectangle(x, y);
			}
			Position = new Position();
		}
	}
}
