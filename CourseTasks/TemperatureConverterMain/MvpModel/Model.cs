using System;
using TemperatureConverterMain.MvpModel.Scales;

namespace TemperatureConverterMain.MvpModel
{
    public class Model
    {
        private readonly ScalesList scalesList;
        public event Action Changes;
        public event Action<string> AddScale;
        public event Action<string> RemoveScale;

        public Model()
        {
            scalesList = new ScalesList();
            scalesList.AddScale += Scales_AddScale;
            scalesList.RemoveScale += Scales_RemoveScale;

            scalesList.Add(new Celsius());
            scalesList.Add(new Kelvin());
            scalesList.Add(new Fahrenheit());
        }

        private void Scales_RemoveScale(string scaleName)
        {
            RemoveScale?.Invoke(scaleName);
        }

        private void Scales_AddScale(string scaleName)
        {
            AddScale?.Invoke(scaleName);
        }

        public string[] GetScalesRange()
        {
            return scalesList.GetScalesRange();
        }

        public void Convert(string sourceScale, string resultScale, int degrees)
        {
            IScale scaleFrom = scalesList.Get(sourceScale);
            IScale scaleTo = scalesList.Get(resultScale);

            scaleFrom.Degrees = degrees;
            scaleTo.Degrees = scaleTo.GetValueAboutOtherScale(scaleFrom);

            Changes?.Invoke();
        }

        public double GetResult(string resultScale)
        {
            return scalesList.Get(resultScale).Degrees;
        }
    }
}
