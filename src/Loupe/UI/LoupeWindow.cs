using System.Drawing;
using System.Windows.Forms;
using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;

namespace Loupe.UI
{
  class LoupeWindow : Form
  {
    readonly GH_Canvas main;
    readonly GH_Canvas canvas = new GH_Canvas { Dock = DockStyle.Fill };

    public LoupeWindow(GH_DocumentEditor editor)
    {
      main = Instances.ActiveCanvas;
      Text = "Loupe";
      FormBorderStyle = FormBorderStyle.SizableToolWindow;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.Manual;
      Size = new Size(480, 360);
      Location = main.PointToScreen(new Point(16, main.Height - Height - 16));
      Controls.Add(canvas);
      Attach(main.Document);
      main.DocumentChanged += OnDocumentChanged;
      main.CanvasPostPaintOverlay += OnMainPainted;
      canvas.MouseUp += OnLoupeMouseUp;
    }

    void Attach(GH_Document doc)
    {
      canvas.Document = doc;
      canvas.Invalidate();
    }

    void OnDocumentChanged(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e) => Attach(e.NewDocument);

    void OnMainPainted(GH_Canvas sender)
    {
      if (Visible) canvas.Invalidate();
    }

    void OnLoupeMouseUp(object sender, MouseEventArgs e) => main.Invalidate();

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      if (e.CloseReason == CloseReason.UserClosing)
      {
        e.Cancel = true;
        Hide();
      }
      base.OnFormClosing(e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        main.DocumentChanged -= OnDocumentChanged;
        main.CanvasPostPaintOverlay -= OnMainPainted;
        canvas.MouseUp -= OnLoupeMouseUp;
        canvas.Document = null;
      }
      base.Dispose(disposing);
    }
  }
}
