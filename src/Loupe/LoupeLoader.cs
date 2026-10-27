using Grasshopper;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Loupe.UI;
using Rhino;

namespace Loupe
{
  public class LoupeLoader : GH_AssemblyPriority
  {
    public override GH_LoadingInstruction PriorityLoad()
    {
      Instances.CanvasCreated += OnCanvasCreated;
      return GH_LoadingInstruction.Proceed;
    }

    static void OnCanvasCreated(GH_Canvas canvas)
    {
      Instances.CanvasCreated -= OnCanvasCreated;
      RhinoApp.Idle += OnIdle;
    }

    static void OnIdle(object sender, System.EventArgs e)
    {
      if (Instances.DocumentEditor == null) return;
      RhinoApp.Idle -= OnIdle;
      ToolbarButton.Add(Instances.DocumentEditor);
    }
  }
}
