using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public partial class HighScoreTableForm : Form
    {
        public HighScoreTableForm()
        {
            InitializeComponent();
            SetRowsHeight();
        }

        private void HighScoreTableForm_ShowForm(object sender, EventArgs e)
        {
            highScoreTableDataGrid.ClearSelection();
        }

        public void SetValues(Dictionary<string, int> scoreTable)
        {
            var k = 0;

            foreach (var pair in scoreTable)
            {
                highScoreTableDataGrid[0, k].Value = pair.Key;
                highScoreTableDataGrid[1, k].Value = pair.Value;
                k++;
            }
        }

        private void SetRowsHeight()
        {
            highScoreTableDataGrid.Rows.Add(10);

            for (var i = 0; i < 10; i++)
            {
                highScoreTableDataGrid.Rows[i].Height = 25;
            }
        }
    }
}
