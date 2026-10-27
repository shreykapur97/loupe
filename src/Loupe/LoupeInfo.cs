using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Loupe
{
  public class LoupeInfo : GH_AssemblyInfo
  {
    internal static readonly Bitmap Icon24 = new Bitmap(typeof(LoupeInfo).Assembly.GetManifestResourceStream("loupe_24.png"));

    public override string Name => "Loupe";
    public override Bitmap Icon => Icon24;
    public override string Description => "A second, navigable canvas on the same document.";
    public override Guid Id => new Guid("678928df-71db-494e-a428-195fc2025477");
    public override string AuthorName => "Shrey Kapur";
    public override string AuthorContact => "https://github.com/shreykapur97/loupe";
    public override string AssemblyVersion => GetType().Assembly.GetName().Version.ToString();
  }
}
