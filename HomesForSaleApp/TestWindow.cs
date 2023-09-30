using System;
namespace Assignment1
{
    public partial class TestWindow : Gtk.Window
    {
        public TestWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
