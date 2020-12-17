using System;
using System.Windows.Forms;

using Minesweeper.Modul;
using Minesweeper.presenter;
using Minesweeper.View;

namespace Minesweeper
{
    public static class Minesweeper
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            MainForm view = new MainForm(new ParametrsForm(), new HighScoreTableForm());
            new Presenter(view, new PlayingField());
            Application.Run(view);
        }
    }
}
