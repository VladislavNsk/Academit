using System;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public partial class NewRecordForm : Form
    {
        public event Action<string> AddNewRecord;

        public NewRecordForm()
        {
            InitializeComponent();
        }

        private void OnOkButton(object sender, EventArgs e)
        {
            if (playerNameBox.Text.Length == 0)
            {
                MessageBox.Show("Введите никнейм.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddNewRecord?.Invoke(playerNameBox.Text);
            Close();
        }
    }
}
