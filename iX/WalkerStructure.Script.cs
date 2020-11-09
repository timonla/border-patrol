using System;
using Model;
using Renderer;

namespace Walker {
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
		private Rectangle rectangle;
		private Position police;
		
		public Walker(WalkerTags tags) {
			this.tags = tags;
			police = new Position(0, 0);
		}
		
		public void Walk() {
			if (tags.Running.Get()) {
				// Update police position
			}
			
			// Update render output
			tags.RenderOutput.Set(TextRenderer.Render(rectangle, police));
		}

		public void UpdateGrid() {
			rectangle = new Rectangle(tags.XValue.Get(), tags.YValue.Get());
		}
	}
}
