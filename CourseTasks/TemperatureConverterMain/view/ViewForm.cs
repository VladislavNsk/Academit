using System;
using System.Drawing;
using System.Windows.Forms;
using TemperatureConverterMain.View;

namespace TemperatureConverterMain
{
    public partial class ViewForm : Form, IView
    {
        public event Action ConvertTemperature;
        public event Action LoadForm;

        public string SourceDegrees => sourceDegreesTb.Text;

        public string SourceScale => sourceScaleDegreesCb.Text;

        public string ResultScale => resultScaleDegreesCb.Text;

        public ViewForm()
        {
            InitializeComponent();
            PrintMainWindow();
        }

        private void PrintMainWindow()
        {
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.ColumnCount = 3;

            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

                for (int j = 0; j < tableLayoutPanel.ColumnCount; j++)
                {
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                }
            }

            tableLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel.BackColor = SystemColors.InactiveCaption;

            tableLayoutPanel.Controls.Add(nameProjectLabel, 1, 0);
            tableLayoutPanel.Controls.Add(resultScaleLabel, 2, 1);
            tableLayoutPanel.Controls.Add(sourceScaleLabel, 0, 1);
            tableLayoutPanel.Controls.Add(sourceDegreesTb, 0, 4);
            tableLayoutPanel.Controls.Add(sourceScaleDegreesCb, 2, 2);
            tableLayoutPanel.Controls.Add(resultScaleDegreesCb, 0, 2);
            tableLayoutPanel.Controls.Add(resultDegreesTb, 2, 4);
            tableLayoutPanel.Controls.Add(convertButton, 1, 5);
            tableLayoutPanel.Controls.Add(resultDegreesLabel, 2, 3);
            tableLayoutPanel.Controls.Add(sourceDegreesLabel, 0, 3);
        }

        public void SetResultDegrees(string value) => resultDegreesTb.Text = value;

        public void AddScale(string scaleName)
        {
            sourceScaleDegreesCb.Items.Add(scaleName);
            resultScaleDegreesCb.Items.Add(scaleName);
        }

        public void RemoveScale(string scaleName)
        {
            sourceScaleDegreesCb.Items.Remove(scaleName);
            resultScaleDegreesCb.Items.Remove(scaleName);
        }

        public void AddScaleRange(string[] scalesNames)
        {
            sourceScaleDegreesCb.Items.AddRange(scalesNames);
            resultScaleDegreesCb.Items.AddRange(scalesNames);

            sourceScaleDegreesCb.SelectedIndex = 0;
            resultScaleDegreesCb.SelectedIndex = 0;
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            LoadForm?.Invoke();
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(SourceDegrees, out double degrees))
            {
                MessageBox.Show($"Значение исходных градусов ({SourceDegrees}) должно быть числом.", "Ошибка заполнения",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConvertTemperature?.Invoke();
        }
    }
}
