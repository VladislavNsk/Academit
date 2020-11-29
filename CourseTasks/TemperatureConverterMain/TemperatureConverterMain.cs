using System;
using System.Windows.Forms;
using TemperatureConverterMain.Controller;

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
            ControllerMvc controller = new ControllerMvc(view);
            Application.Run(view);
        }
    }
}
