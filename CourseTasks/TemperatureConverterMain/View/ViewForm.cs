using System;
using System.Windows.Forms;
using TemperatureConverterMain.View;

namespace TemperatureConverterMain
{
    public partial class ViewForm : Form, IView
    {
        public ViewForm()
        {
            InitializeComponent();
        }

        public event EventHandler ConvertButton_ClickEventHandler;
        public event Action Changes;

        public string SourseDegrees => sourceDegreesBox.Text;

        public string SourceTemperatureScale => sourceScaleDegreesBox.Text;

        public string ResultTemperatureScale => resultScaleDegrees.Text;

        public void SetResultDegrees(string value) => resultDegreesBox.Text = value;

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            ConvertButton_ClickEventHandler?.Invoke(sender, e);
        }

        public void PrintError(string err)
        {
            MessageBox.Show(err);
        }
    }
}
