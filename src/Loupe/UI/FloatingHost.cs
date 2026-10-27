using System.Windows.Forms;

namespace Loupe.UI
{
  class FloatingHost : Form
  {
    public FloatingHost()
    {
      FormBorderStyle = FormBorderStyle.None;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.Manual;
    }

    protected override CreateParams CreateParams
    {
      get
      {
        var p = base.CreateParams;
        p.ClassStyle |= 0x20000;
        return p;
      }
    }
  }
}
