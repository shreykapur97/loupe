using Grasshopper;
using Grasshopper.Kernel;
using Loupe.UI;
using Rhino;

namespace Loupe
{
  public class LoupeLoader : GH_AssemblyPriority
  {
    public override GH_LoadingInstruction PriorityLoad()
    {
      RhinoApp.Idle += OnIdle;
      return GH_LoadingInstruction.Proceed;
    }

    static void OnIdle(object sender, System.EventArgs e)
    {
      if (Instances.DocumentEditor == null) return;
      RhinoApp.Idle -= OnIdle;
      ToolbarButton.Add(Instances.DocumentEditor);
    }
  }
}
