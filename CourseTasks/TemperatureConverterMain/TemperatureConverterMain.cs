using System;
using System.Windows.Forms;
using TemperatureConverterMain.MvcPresenter;

namespace TemperatureConverterMain
{
    static class TemperatureConverterMain
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ViewForm view = new ViewForm();
            new Presenter(view);
            Application.Run(view);
        }
    }
}
