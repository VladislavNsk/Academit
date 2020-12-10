using System;
using TemperatureConverterMain.view;
using TemperatureConverterMain.model;

namespace TemperatureConverterMain.presenter
{
    public class Presenter
    {
        private readonly IView view;
        private readonly Model model = new Model();

        public Presenter(IView view)
        {
            this.view = view;

            view.ConvertTemperature += new EventHandler(OnConvertButton_Click);
            view.LoadForm += View_LoadForm;

            model.Changes += Model_res;
            model.AddScale += Model_AddScaleName;
            model.RemoveScale += Model_RemoveScaleName;
        }

        private void OnConvertButton_Click(object sender, EventArgs e)
        {
            model.Convert(view.SourceScale, view.ResultScale, int.Parse(view.SourceDegrees));
        }

        private void View_LoadForm(object sender, EventArgs e)
        {
            view.AddScaleRange(model.GetScalesRange());
        }

        private void Model_RemoveScaleName(string scaleName)
        {
            view.RemoveScale(scaleName);
        }

        private void Model_AddScaleName(string scaleName)
        {
            view.AddScale(scaleName);
        }

        private void Model_res()
        {
            view.SetResultDegrees(model.GetResult(view.ResultScale).ToString("#.##"));
        }
    }
}
