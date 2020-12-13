using Minesweeper.presenter;
using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class Minesweeper
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            ViewForm view = new ViewForm();
            new Presenter(view);
            Application.Run(view);
        }
    }
}
