using System.Drawing;
using System.Drawing.Drawing2D;

namespace Loupe.UI
{
  static class Style
  {
    public static readonly Color Ink = Color.FromArgb(40, 40, 40);
    public static readonly Color Rim = Color.FromArgb(242, 242, 242);

    public static GraphicsPath Rounded(Rectangle r, int radius)
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
  }
}
