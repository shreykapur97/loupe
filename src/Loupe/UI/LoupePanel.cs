using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;

namespace Loupe.UI
{
  class LoupePanel : Control
  {
    const int Move = 16;
    static readonly Color Ink = Color.FromArgb(40, 40, 40);
    readonly GH_Canvas main;
    readonly GH_Canvas canvas = new GH_Canvas();
    readonly IconButton lockButton, homeButton;
    readonly float scale;
    bool locked = true;
    int grip;
    Point dragStart;
    Rectangle boundsStart;

    public LoupePanel(GH_Canvas main)
    {
      this.main = main;
      scale = main.DeviceDpi / 96f;
      DoubleBuffered = true;
      Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      canvas.Widgets.Clear();
      lockButton = new IconButton(S(20), g => DrawLock(g, locked)) { Location = new Point(S(12), S(12)) };
      homeButton = new IconButton(S(20), DrawHome) { Location = new Point(S(36), S(12)) };
      lockButton.Click += (s, e) => { locked = !locked; lockButton.Invalidate(); };
      homeButton.Click += (s, e) => Home();
      Controls.AddRange(new Control[] { lockButton, homeButton, canvas });
      Attach(main.Document);
      main.DocumentChanged += OnDocumentChanged;
      main.CanvasPostPaintOverlay += OnMainPainted;
      main.SizeChanged += OnMainResized;
      canvas.MouseUp += OnLoupeMouseUp;
    }

    int S(int v) => (int)(v * scale);
    int Border => S(8);

    public void Home()
    {
      Size = new Size(S(480), S(360));
      Location = new Point(main.Left + S(16), main.Bottom - Height - S(16));
      Bounds = Clamp(Bounds);
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

    void OnMainResized(object sender, EventArgs e) => Bounds = Clamp(Bounds);

    void OnLoupeMouseUp(object sender, MouseEventArgs e) => main.Invalidate();

    protected override void OnResize(EventArgs e)
    {
      canvas.Bounds = Rectangle.Inflate(ClientRectangle, -Border, -Border);
      using (var path = Rounded(ClientRectangle, S(10))) Region = new Region(path);
      Invalidate();
      base.OnResize(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      var r = new Rectangle(0, 0, Width - 1, Height - 1);
      using (var path = Rounded(r, S(10)))
      using (var fill = new LinearGradientBrush(r, Color.FromArgb(175, 175, 175), Color.FromArgb(105, 105, 105), 90f))
      using (var edge = new Pen(Ink, S(2)))
      {
        g.FillPath(fill, path);
        g.DrawPath(edge, path);
      }
      using (var rim = new Pen(Color.FromArgb(235, 235, 235), S(2)))
        g.DrawRectangle(rim, Rectangle.Inflate(canvas.Bounds, 1, 1));
    }

    int HitTest(Point p)
    {
      if (locked) return 0;
      int c = S(20);
      bool l = p.X < c, r = p.X > Width - c, t = p.Y < c, b = p.Y > Height - c;
      return (l || r) && (t || b) ? (l ? 1 : 0) | (r ? 2 : 0) | (t ? 4 : 0) | (b ? 8 : 0) : Move;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      grip = e.Button == MouseButtons.Left ? HitTest(e.Location) : 0;
      dragStart = Cursor.Position;
      boundsStart = Bounds;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (grip == 0)
      {
        var h = HitTest(e.Location);
        Cursor = h == 0 ? Cursors.Default : h == Move ? Cursors.SizeAll : h == 5 || h == 10 ? Cursors.SizeNWSE : Cursors.SizeNESW;
      }
      else
      {
        int dx = Cursor.Position.X - dragStart.X, dy = Cursor.Position.Y - dragStart.Y;
        var r = boundsStart;
        if (grip == Move) r.Offset(dx, dy);
        else
        {
          if ((grip & 1) != 0) { r.X += dx; r.Width -= dx; }
          if ((grip & 2) != 0) r.Width += dx;
          if ((grip & 4) != 0) { r.Y += dy; r.Height -= dy; }
          if ((grip & 8) != 0) r.Height += dy;
          if (r.Width < S(160)) { if ((grip & 1) != 0) r.X = boundsStart.Right - S(160); r.Width = S(160); }
          if (r.Height < S(120)) { if ((grip & 4) != 0) r.Y = boundsStart.Bottom - S(120); r.Height = S(120); }
        }
        Bounds = Clamp(r);
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      grip = 0;
      base.OnMouseUp(e);
    }

    Rectangle Clamp(Rectangle r)
    {
      var a = main.Bounds;
      r.Width = Math.Min(r.Width, a.Width);
      r.Height = Math.Min(r.Height, a.Height);
      r.X = Math.Max(a.Left, Math.Min(r.X, a.Right - r.Width));
      r.Y = Math.Max(a.Top, Math.Min(r.Y, a.Bottom - r.Height));
      return r;
    }

    static GraphicsPath Rounded(Rectangle r, int radius)
    {
      var d = radius * 2;
      var path = new GraphicsPath();
      path.AddArc(r.Left, r.Top, d, d, 180, 90);
      path.AddArc(r.Right - d, r.Top, d, d, 270, 90);
      path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
      path.AddArc(r.Left, r.Bottom - d, d, d, 90, 90);
      path.CloseFigure();
      return path;
    }

    static void DrawLock(Graphics g, bool closed)
    {
      using (var pen = new Pen(Ink, 2f))
      using (var ink = new SolidBrush(Ink))
      {
        var y = closed ? 4 : 1;
        g.DrawArc(pen, 6, y, 8, 8, 180, 180);
        g.DrawLine(pen, 6, y + 4, 6, 9);
        g.DrawLine(pen, 14, y + 4, 14, closed ? 9 : y + 5);
        g.FillRectangle(ink, 4, 9, 12, 8);
      }
    }

    static void DrawHome(Graphics g)
    {
      using (var ink = new SolidBrush(Ink))
      {
        g.FillPolygon(ink, new[] { new Point(10, 3), new Point(17, 10), new Point(15, 10), new Point(15, 17), new Point(5, 17), new Point(5, 10), new Point(3, 10) });
        g.FillRectangle(Brushes.WhiteSmoke, 9, 12, 3, 5);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        main.DocumentChanged -= OnDocumentChanged;
        main.CanvasPostPaintOverlay -= OnMainPainted;
        main.SizeChanged -= OnMainResized;
        canvas.MouseUp -= OnLoupeMouseUp;
        canvas.Document = null;
      }
      base.Dispose(disposing);
    }

    class IconButton : Control
    {
      readonly Action<Graphics> glyph;

      public IconButton(int size, Action<Graphics> glyph)
      {
        this.glyph = glyph;
        Size = new Size(size, size);
        DoubleBuffered = true;
        Cursor = Cursors.Hand;
      }

      protected override void OnPaint(PaintEventArgs e)
      {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Color.FromArgb(240, 240, 240));
        using (var edge = new Pen(Ink)) g.DrawRectangle(edge, 0, 0, Width - 1, Height - 1);
        g.ScaleTransform(Width / 20f, Height / 20f);
        glyph(g);
      }
    }
  }
}
