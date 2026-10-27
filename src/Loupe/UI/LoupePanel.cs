using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Grasshopper.GUI.Canvas;

namespace Loupe.UI
{
  class LoupePanel : Control
  {
    const int MoveGrip = 16;
    readonly GH_Canvas main;
    readonly LoupeCanvas canvas;
    readonly IconButton lockButton, floatButton, viewButton;
    readonly float scale;
    FloatingHost host;
    int grip;
    Point dragStart;
    Rectangle boundsStart;

    public LoupePanel(GH_Canvas main)
    {
      this.main = main;
      scale = main.DeviceDpi / 96f;
      canvas = new LoupeCanvas(main);
      DoubleBuffered = true;
      Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      lockButton = AddButton(0, "lock", "lock-open", () => lockButton.Active = !lockButton.Active);
      AddButton(1, "house", null, Home);
      floatButton = AddButton(2, "square-arrow-down-left", "square-arrow-out-up-right", ToggleFloating);
      viewButton = AddButton(3, "eye", "pencil", () => canvas.ViewOnly = viewButton.Active = !viewButton.Active);
      AddButton(4, "scan", null, MatchView);
      floatButton.Active = false;
      Controls.Add(canvas);
      main.SizeChanged += OnMainResized;
    }

    bool Locked => lockButton.Active;
    bool Floating => host != null;
    Control Target => Floating ? host : this;
    int Border => S(5);
    int Radius => S(10);
    int S(int v) => (int)(v * scale);

    public bool Shown
    {
      get => Target.Visible;
      set
      {
        Target.Visible = value;
        if (value && !Floating) BringToFront();
      }
    }

    public void MatchView() => canvas.MatchView();

    public void Home()
    {
      if (Floating) ToggleFloating();
      Bounds = Clamp(new Rectangle(main.Left + S(16), main.Bottom - S(376), S(480), S(360)));
    }

    IconButton AddButton(int row, string on, string off, Action click)
    {
      var b = new IconButton(S(22), on, off) { Location = new Point(S(10), S(10) + row * S(26)), Active = true };
      b.Click += (s, e) => click();
      Controls.Add(b);
      return b;
    }

    void ToggleFloating()
    {
      if (Floating)
      {
        var r = main.Parent.RectangleToClient(host.Bounds);
        var old = host;
        host = null;
        Dock = DockStyle.None;
        main.Parent.Controls.Add(this);
        Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        Bounds = Clamp(r);
        BringToFront();
        old.Dispose();
      }
      else
      {
        host = new FloatingHost { Bounds = main.Parent.RectangleToScreen(Bounds) };
        Anchor = AnchorStyles.None;
        Dock = DockStyle.Fill;
        host.Controls.Add(this);
        host.Show(main.FindForm());
      }
      floatButton.Active = Floating;
    }

    void OnMainResized(object sender, EventArgs e)
    {
      if (!Floating) Bounds = Clamp(Bounds);
    }

    protected override void OnResize(EventArgs e)
    {
      canvas.Bounds = Rectangle.Inflate(ClientRectangle, -Border, -Border);
      using (var path = Style.Rounded(ClientRectangle, Radius))
      {
        Region = new Region(path);
        if (Floating) host.Region = new Region(path);
      }
      Invalidate();
      base.OnResize(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      using (var outline = Style.Rounded(ClientRectangle, Radius))
      using (var rim = Style.Rounded(Rectangle.Inflate(ClientRectangle, -S(3), -S(3)), Radius - S(3)))
      using (var ink = new SolidBrush(Style.Ink))
      using (var light = new SolidBrush(Style.Rim))
      {
        g.FillPath(ink, outline);
        g.FillPath(light, rim);
      }
    }

    int HitTest(Point p)
    {
      if (Locked) return 0;
      int c = S(20);
      bool l = p.X < c, r = p.X > Width - c, t = p.Y < c, b = p.Y > Height - c;
      return (l || r) && (t || b) ? (l ? 1 : 0) | (r ? 2 : 0) | (t ? 4 : 0) | (b ? 8 : 0) : MoveGrip;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      grip = e.Button == MouseButtons.Left ? HitTest(e.Location) : 0;
      dragStart = Cursor.Position;
      boundsStart = Target.Bounds;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (grip == 0)
      {
        var h = HitTest(e.Location);
        Cursor = h == 0 ? Cursors.Default : h == MoveGrip ? Cursors.SizeAll : h == 5 || h == 10 ? Cursors.SizeNWSE : Cursors.SizeNESW;
      }
      else
      {
        var r = Dragged(Cursor.Position.X - dragStart.X, Cursor.Position.Y - dragStart.Y);
        if (Floating) host.Bounds = r;
        else Bounds = Clamp(r);
      }
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      grip = 0;
      base.OnMouseUp(e);
    }

    Rectangle Dragged(int dx, int dy)
    {
      var r = boundsStart;
      if (grip == MoveGrip)
      {
        r.Offset(dx, dy);
        return r;
      }
      if ((grip & 1) != 0) { r.X += dx; r.Width -= dx; }
      if ((grip & 2) != 0) r.Width += dx;
      if ((grip & 4) != 0) { r.Y += dy; r.Height -= dy; }
      if ((grip & 8) != 0) r.Height += dy;
      if (r.Width < S(160)) { if ((grip & 1) != 0) r.X = boundsStart.Right - S(160); r.Width = S(160); }
      if (r.Height < S(160)) { if ((grip & 4) != 0) r.Y = boundsStart.Bottom - S(160); r.Height = S(160); }
      return r;
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

    protected override void Dispose(bool disposing)
    {
      if (disposing) main.SizeChanged -= OnMainResized;
      base.Dispose(disposing);
    }
  }
}
