using System;
using System.Drawing;
using System.IO;
using System.Text.Json;

namespace Loupe.UI
{
  class PanelLayout
  {
    static readonly string File = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Grasshopper", "Loupe.json");

    public bool Floating { get; set; }
    public bool ViewOnly { get; set; } = true;
    public int[] Pinned { get; set; }
    public int[] Window { get; set; }

    public static PanelLayout Load()
    {
      try { return JsonSerializer.Deserialize<PanelLayout>(System.IO.File.ReadAllText(File)) ?? new PanelLayout(); }
      catch { return new PanelLayout(); }
    }

    public void Save()
    {
      try { System.IO.File.WriteAllText(File, JsonSerializer.Serialize(this)); }
      catch { }
    }

    public static int[] From(Rectangle r) => new[] { r.X, r.Y, r.Width, r.Height };
    public static Rectangle? To(int[] a) => a?.Length == 4 ? new Rectangle(a[0], a[1], a[2], a[3]) : (Rectangle?)null;
  }
}
