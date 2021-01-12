using System;
using TemperatureConverterMain.View;

namespace TemperatureConverterMain.Presenter
{
    public class Presenter
    {
        private readonly IView view;
        private readonly Model.Model model;

        public Presenter(IView view, Model.Model model)
        {
            this.view = view;
            this.model = model;

            view.ConvertTemperature += OnConvertButton_Click;
            view.LoadForm += View_LoadForm;

            model.Changes += Model_res;
            model.AddScale += Model_AddScaleName;
            model.RemoveScale += Model_RemoveScaleName;
        }

        private void OnConvertButton_Click()
        {
            model.Convert(view.SourceScale, view.ResultScale, int.Parse(view.SourceDegrees));
        }

        private void View_LoadForm()
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
            view.SetResultDegrees(model.GetResult(Math.Round(view.ResultScale, 2)).ToString());
        }
    }
}
