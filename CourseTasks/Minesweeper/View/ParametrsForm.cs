using System;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public partial class ParametrsForm : Form
    {
        private readonly string parametrSpecialName = "Свой";
        public event Action SetSpecialParametrs;
        public event Action SetParametrs;

        public ParametrsForm()
        {
            InitializeComponent();
        }

        private void OnApplyButton(object sender, EventArgs e)
        {
            if (parametrsBox.Text == parametrSpecialName)
            {
                var maxMinesCount = (int)(rowsNumeric.Value * columnsNumeric.Value) - 1;

                if (maxMinesCount < minesNumeric.Value)
                {
                    MessageBox.Show($"Количество мин должно быть от 9 до {maxMinesCount}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SetSpecialParametrs?.Invoke();
            }
            else
            {
                SetParametrs?.Invoke();
            }

            Close();
        }

        private void OnSelectionChangeCommitted(object sender, EventArgs e)
        {
            var parametrName = sender as ComboBox;

            if (parametrName.Text == parametrSpecialName)
            {
                rowsNumeric.Enabled = true;
                columnsNumeric.Enabled = true;
                minesNumeric.Enabled = true;
            }
            else
            {
                rowsNumeric.Enabled = false;
                columnsNumeric.Enabled = false;
                minesNumeric.Enabled = false;
            }
        }

        public void SetParametrsNames(string[] parametrsNames)
        {
            parametrsBox.Items.AddRange(parametrsNames);
            parametrsBox.Items.Add(parametrSpecialName);
            parametrsBox.SelectedIndex = 0;
        }

        public string GetParametrName()
        {
            return parametrsBox.Text;
        }

        public (int rowsCount, int columnsCount, int minesCount) GetSpecialParametrs()
        {
            return ((int)rowsNumeric.Value, (int)columnsNumeric.Value, (int)minesNumeric.Value);
        }

        public string GetPlayerName()
        {
            return nameBox.Text;
        }
    }
}
