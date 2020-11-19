using System;
using System.Runtime.CompilerServices;
using Scripts.Model;
using Scripts.Renderer;

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
		public Rectangle Rectangle { get; private set; }
		public Position Police { get; private set; }
		
		public Walker(WalkerTags tags) {
			this.tags = tags;
			Police = new Position(0, 0);
		}
		
		public void Walk() {
			if (tags.Running.Get()) {
				// Update police position
				if (Police.Y == 0) {
					// In top row
					if (Police.X == Rectangle.Width - 1) {
						// In end corner
						Police.Y += 1;
                    } else {
						Police.X += 1;
                    }
                } else if (Police.Y == Rectangle.Height - 1) {
					// In bottom row
					if (Police.X == 0) {
						// In end corner
						Police.Y -= 1;
                    } else {
						Police.X -= 1;
                    }
                } else if (Police.X == 0) {
					// In left column
					Police.Y -= 1;
                } else {
					// In right column
					Police.Y += 1;
                }
			}
			
			// Update render output
			tags.RenderOutput.Set(TextRenderer.Render(Rectangle, Police));
		}

		public void UpdateGrid() {
			var x = tags.XValue.Get();
			var y = tags.YValue.Get();
			if (x == 0 && y == 0) return;
			if (y == 0) {
				Rectangle = new Square(x);
			} else if (x == 0) { 
				Rectangle = new Square(y);
			} else { 
				Rectangle = new Rectangle(x, y);
			}
			Police = new Position(0, 0);
		}
	}
}
