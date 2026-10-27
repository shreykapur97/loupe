using System;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;

namespace Loupe.UI
{
  class LoupeCanvas : GH_Canvas
  {
    readonly GH_Canvas main;

    public LoupeCanvas(GH_Canvas main)
    {
      this.main = main;
      Widgets.Clear();
      Document = main.Document;
      main.DocumentChanged += OnDocumentChanged;
      main.CanvasPostPaintOverlay += OnMainPainted;
    }

    public bool ViewOnly { get; set; } = true;

    public void MatchView()
    {
      Viewport.Zoom = main.Viewport.Zoom;
      Viewport.ComputeProjection();
      Viewport.MidPoint = main.Viewport.MidPoint;
      Viewport.ComputeProjection();
      Refresh();
    }

    void OnDocumentChanged(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e)
    {
      Document = e.NewDocument;
      Invalidate();
    }

    void OnMainPainted(GH_Canvas sender)
    {
      if (Visible) Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (ViewOnly) return;
      base.OnMouseUp(e);
      main.Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e) { if (!ViewOnly) base.OnMouseDown(e); }
    protected override void OnMouseMove(MouseEventArgs e) { if (!ViewOnly) base.OnMouseMove(e); }
    protected override void OnMouseWheel(MouseEventArgs e) { if (!ViewOnly) base.OnMouseWheel(e); }
    protected override void OnMouseDoubleClick(MouseEventArgs e) { if (!ViewOnly) base.OnMouseDoubleClick(e); }
    protected override void OnMouseEnter(EventArgs e) { if (!ViewOnly) base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { if (!ViewOnly) base.OnMouseLeave(e); }
    protected override void OnKeyDown(KeyEventArgs e) { if (!ViewOnly) base.OnKeyDown(e); }
    protected override void OnKeyUp(KeyEventArgs e) { if (!ViewOnly) base.OnKeyUp(e); }
    protected override void OnDragEnter(DragEventArgs e) { if (!ViewOnly) base.OnDragEnter(e); }
    protected override void OnDragOver(DragEventArgs e) { if (!ViewOnly) base.OnDragOver(e); }
    protected override void OnDragDrop(DragEventArgs e) { if (!ViewOnly) base.OnDragDrop(e); }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        main.DocumentChanged -= OnDocumentChanged;
        main.CanvasPostPaintOverlay -= OnMainPainted;
        Document = null;
      }
      base.Dispose(disposing);
    }
  }
}
