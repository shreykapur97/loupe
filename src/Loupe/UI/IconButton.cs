using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Loupe.UI
{
  class IconButton : Control
  {
    readonly Bitmap on, off;
    bool active;

    public IconButton(int size, string on, string off = null)
    {
      this.on = Assets.Load(on);
      this.off = off == null ? this.on : Assets.Load(off);
      Size = new Size(size, size);
      DoubleBuffered = true;
      Cursor = Cursors.Hand;
      using (var path = Style.Rounded(new Rectangle(0, 0, size, size), size / 5)) Region = new Region(path);
    }

    public bool Active
    {
      get => active;
      set { active = value; Invalidate(); }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
      g.Clear(Style.Rim);
      using (var path = Style.Rounded(new Rectangle(0, 0, Width - 1, Height - 1), Width / 5))
      using (var edge = new Pen(Style.Ink)) g.DrawPath(edge, path);
      g.DrawImage(active ? on : off, Rectangle.Inflate(ClientRectangle, -Width / 6, -Height / 6));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        on.Dispose();
        off.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
