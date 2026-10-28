using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;

namespace Loupe.UI
{
  class LoupeCanvas : GH_Canvas
  {
    readonly GH_Canvas main;
    readonly ConditionalWeakTable<GH_Document, Tuple<float, PointF>> views = new ConditionalWeakTable<GH_Document, Tuple<float, PointF>>();
    Point? panFrom;

    public LoupeCanvas(GH_Canvas main)
    {
      this.main = main;
      Widgets.Clear();
      Document = main.Document;
      main.DocumentChanged += OnDocumentChanged;
      main.CanvasPostPaintOverlay += OnMainPainted;
    }

    public bool ViewOnly { get; set; } = true;

    public void MatchView() => SetView(main.Viewport.Zoom, main.Viewport.MidPoint);

    void SetView(float zoom, PointF mid)
    {
      Viewport.Zoom = zoom;
      Viewport.ComputeProjection();
      Viewport.MidPoint = mid;
      Viewport.ComputeProjection();
      Refresh();
    }

    void OnDocumentChanged(GH_Canvas sender, GH_CanvasDocumentChangedEventArgs e)
    {
      if (Document != null) views.AddOrUpdate(Document, Tuple.Create(Viewport.Zoom, Viewport.MidPoint));
      Document = e.NewDocument;
      if (Document != null && views.TryGetValue(Document, out var v)) SetView(v.Item1, v.Item2);
      else if (IsHandleCreated) BeginInvoke((Action)MatchView);
      Invalidate();
    }

    void OnMainPainted(GH_Canvas sender)
    {
      if (Visible) Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (!ViewOnly) base.OnMouseDown(e);
      else if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle) panFrom = e.Location;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (!ViewOnly) base.OnMouseMove(e);
      else if (panFrom is Point from)
      {
        var m = Viewport.MidPoint;
        Viewport.MidPoint = new PointF(m.X - (e.X - from.X) / Viewport.Zoom, m.Y - (e.Y - from.Y) / Viewport.Zoom);
        Viewport.ComputeProjection();
        panFrom = e.Location;
        Invalidate();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      panFrom = null;
      if (ViewOnly) return;
      base.OnMouseUp(e);
      main.Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (!ViewOnly)
      {
        base.OnMouseWheel(e);
        return;
      }
      var before = Viewport.UnprojectPoint(e.Location);
      Viewport.Zoom = Math.Clamp(Viewport.Zoom * (e.Delta > 0 ? 1.25f : 0.8f), 0.05f, 10f);
      Viewport.ComputeProjection();
      var after = Viewport.UnprojectPoint(e.Location);
      var m = Viewport.MidPoint;
      Viewport.MidPoint = new PointF(m.X + before.X - after.X, m.Y + before.Y - after.Y);
      Viewport.ComputeProjection();
      Invalidate();
    }

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
