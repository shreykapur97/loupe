using System.Drawing;

namespace Loupe
{
  static class Assets
  {
    public static Bitmap Load(string name) => new Bitmap(typeof(Assets).Assembly.GetManifestResourceStream(name + ".png"));
  }
}
