using System;
using System.Windows.Forms;

namespace TemperatureConverterMain
{
    static class TemperatureConverterMain
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var view = new ViewForm();
            new Presenter.Presenter(view, new Model.Model());
            Application.Run(view);
        }
    }
}
