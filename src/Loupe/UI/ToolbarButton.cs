using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Rhino;

namespace Loupe.UI
{
  static class ToolbarButton
  {
    static LoupePanel panel;

    public static void Add(GH_DocumentEditor editor)
    {
      var strip = FindCanvasToolbar(editor);
      if (strip == null)
      {
        RhinoApp.WriteLine("Loupe: canvas toolbar not found, button not added.");
        return;
      }
      var button = new ToolStripButton(LoupeInfo.Icon24)
      {
        DisplayStyle = ToolStripItemDisplayStyle.Image,
        ToolTipText = "Loupe: show or hide a second canvas",
      };
      button.Click += (s, e) => Toggle(editor, button);
      strip.Items.Add(button);
      RhinoApp.WriteLine("Loupe: button added to the canvas toolbar.");
    }

    static void Toggle(GH_DocumentEditor editor, ToolStripButton button)
    {
      if (panel == null || panel.IsDisposed)
      {
        var main = MainCanvas(editor);
        panel = new LoupePanel(main);
        panel.VisibleChanged += (s, e) => button.Checked = panel.Visible;
        main.Parent.Controls.Add(panel);
        panel.Home();
        panel.BringToFront();
        button.Checked = true;
        return;
      }
      panel.Visible = !panel.Visible;
      if (panel.Visible) panel.BringToFront();
    }

    static GH_Canvas MainCanvas(Control editor) => Descendants(editor).OfType<GH_Canvas>().First();

    static ToolStrip FindCanvasToolbar(Control editor) =>
      Descendants(editor).OfType<ToolStrip>().FirstOrDefault(t => t.Name == "CanvasToolbar") ?? ToolbarAboveCanvas(editor);

    static ToolStrip ToolbarAboveCanvas(Control editor)
    {
      var canvas = Descendants(editor).OfType<GH_Canvas>().FirstOrDefault();
      if (canvas == null) return null;
      var canvasTop = TopIn(editor, canvas);
      return Descendants(editor)
        .OfType<ToolStrip>()
        .Where(t => !(t is MenuStrip) && !(t is StatusStrip) && TopIn(editor, t) + t.Height <= canvasTop)
        .OrderByDescending(t => TopIn(editor, t) + t.Height)
        .FirstOrDefault();
    }

    static int TopIn(Control root, Control c)
    {
      var top = 0;
      for (; c != null && c != root; c = c.Parent) top += c.Top;
      return top;
    }

    static IEnumerable<Control> Descendants(Control c) =>
      c.Controls.Cast<Control>().SelectMany(x => new[] { x }.Concat(Descendants(x)));
  }
}
