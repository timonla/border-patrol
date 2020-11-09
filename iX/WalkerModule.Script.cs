//--------------------------------------------------------------
// Press F1 to get help about using script.
// To access an object that is not located in the current class, start the call with Globals.
// When using events and timers be cautious not to generate memoryleaks,
// please see the help for more information.
//---------------------------------------------------------------

namespace Neo.ApplicationFramework.Generated
{
    using System.Windows.Forms;
    using System;
    using System.Drawing;
    using Neo.ApplicationFramework.Tools;
    using Neo.ApplicationFramework.Common.Graphics.Logic;
    using Neo.ApplicationFramework.Controls;
    using Neo.ApplicationFramework.Interfaces;
	using Walker;
    
    
    public partial class WalkerModule {
		static WalkerTags walkerTags = new WalkerTags(
			new VariableReference<int>(
				// XValue
				() => Globals.Tags.XValue.Value,
				val => { Globals.Tags.XValue.Value = val; }),
			new VariableReference<int>(
				// YValue
				() => Globals.Tags.YValue.Value,
				val => { Globals.Tags.YValue.Value = val; }),
			new VariableReference<string>(
				// RenderOutput
				() => Globals.Tags.RenderOutput.Value,
				val => { Globals.Tags.RenderOutput.Value = val; }),
			new VariableReference<bool>(
				// Running
				() => Globals.Tags.Running.Value,
				val => { Globals.Tags.Running.Value = val; })
			);
		
		Walker walker = new Walker(walkerTags);
		
		public void Loop_1s() {
			walker.Walk();
		}

		public void UpdateGrid() {
			walker.UpdateGrid();
		}
    }
}
