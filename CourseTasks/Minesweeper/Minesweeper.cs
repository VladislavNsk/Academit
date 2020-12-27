using System;
using System.Windows.Forms;

using Minesweeper.Model;
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
            MainForm view = new MainForm(new ParametersForm(), new HighScoreTableForm());
            new Presenter.Presenter(view, new PlayingField());
            Application.Run(view);
        }
    }
}
