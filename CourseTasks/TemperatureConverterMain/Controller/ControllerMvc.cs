using System;
using TemperatureConverterMain.View;
using TemperatureConverterMain.Model;

namespace TemperatureConverterMain.Controller
{
    public class ControllerMvc
    {
        private readonly IView view;
        private readonly ModelMvc model = new ModelMvc();

        public ControllerMvc(IView view)
        {
            this.view = view;
            view.ConvertButton_ClickEventHandler += new EventHandler(OnConvertButton_Click);
            model.Changes += Model_res;
            model.Error += Model_Error;
        }

        private void Model_Error()
        {
            view.PrintError(model.Message);
        }

        private void Model_res()
        {
            view.SetResultDegrees(model.GetResult(view.ResultTemperatureScale).ToString("#.##"));
        }

        private void OnConvertButton_Click(object sender, EventArgs e)
        {
            model.Convert(view.SourceTemperatureScale, view.ResultTemperatureScale, view.SourseDegrees);
        }
    }
}
