using System;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public partial class ParametersForm : Form
    {
        private readonly string parameterSpecialName = "Свой";
        public event Action SetSpecialParameters;
        public event Action ChangeParameter;
        public event Action SetParameters;

        public ParametersForm()
        {
            InitializeComponent();
        }

        private void OnApplyButton(object sender, EventArgs e)
        {
            if (parametersBox.Text == parameterSpecialName)
            {
                var maxMinesCount = (int)(rowsNumeric.Value * columnsNumeric.Value) - 1;

                if (maxMinesCount < minesNumeric.Value)
                {
                    MessageBox.Show($"Количество мин должно быть от 9 до {maxMinesCount}", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SetSpecialParameters?.Invoke();
            }
            else
            {
                SetParameters?.Invoke();
            }

            Close();
        }

        private void OnSelectionChangeCommitted(object sender, EventArgs e)
        {
            var parametrName = sender as ComboBox;

            if (parametrName.Text == parameterSpecialName)
            {
                rowsNumeric.Enabled = true;
                columnsNumeric.Enabled = true;
                minesNumeric.Enabled = true;
                return;
            }

            rowsNumeric.Enabled = false;
            columnsNumeric.Enabled = false;
            minesNumeric.Enabled = false;

            ChangeParameter?.Invoke();
        }

        public void SetParametersBoxs(int rowsCount, int columnCount,int  minesCount)
        {
            rowsNumeric.Value = rowsCount;
            columnsNumeric.Value = columnCount;
            minesNumeric.Value = minesCount;
        }

        public void SetParametersNames(string[] parametersNames)
        {
            parametersBox.Items.AddRange(parametersNames);
            parametersBox.Items.Add(parameterSpecialName);
            parametersBox.SelectedIndex = 0;
        }

        public string GetParametrName()
        {
            return parametersBox.Text;
        }

        public (int rowsCount, int columnsCount, int minesCount) GetSpecialParameters()
        {
            return ((int)rowsNumeric.Value, (int)columnsNumeric.Value, (int)minesNumeric.Value);
        }

        public string GetPlayerName()
        {
            return nameBox.Text;
        }
    }
}
