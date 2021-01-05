using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Minesweeper.View
{
    public partial class HighScoreTableForm : Form
    {
        public event Action<string> FillHighScoreTable;

        public HighScoreTableForm()
        {
            InitializeComponent();
            SetRowsHeight();
        }

        private void OnShowForm(object sender, EventArgs e)
        {
            parametersNamesBox.SelectedIndex = 0;
            FillHighScoreTable?.Invoke(parametersNamesBox.Text);
        }

        public void SetValues(Dictionary<string, int> scoreTable)
        {
            highScoreTableDataGrid.Rows.Clear();
            SetRowsHeight();
            highScoreTableDataGrid.ClearSelection();

            int k = 0;

            foreach (var pair in scoreTable.OrderBy(x => x.Value).Take(highScoreTableDataGrid.Rows.Count))
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

        public void SetParametersNames(string[] parametersNames)
        {
            parametersNamesBox.Items.AddRange(parametersNames);
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FillHighScoreTable?.Invoke((sender as ComboBox).Text);
        }

        private void OnOkButton(object sender, EventArgs e)
        {
            Close();
        }
    }
}
