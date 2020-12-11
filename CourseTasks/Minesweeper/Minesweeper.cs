using Minesweeper.Presenter;
using System;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Minesweeper
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            ViewForm view = new ViewForm();
            new PresenterMvp(view);
            Application.Run(view);
        }
    }
}
