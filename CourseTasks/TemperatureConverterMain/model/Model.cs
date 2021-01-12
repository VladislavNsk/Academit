using System;
using TemperatureConverterMain.Model.Scales;

namespace TemperatureConverterMain.Model
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

            scalesList.Add(new CelsiusScale());
            scalesList.Add(new KelvinScale());
            scalesList.Add(new FahrenheitScale());
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
            return scalesList.GetNames();
        }

        public void Convert(string sourceScale, string resultScale, int degrees)
        {
            var scaleFrom = scalesList.GetScale(sourceScale);
            var scaleTo = scalesList.GetScale(resultScale);

            scaleFrom.Degrees = degrees;
            scaleTo.Degrees = scaleTo.GetValueAboutOtherScale(scaleFrom);

            Changes?.Invoke();
        }

        public double GetResult(string resultScale)
        {
            return scalesList.GetScale(resultScale).Degrees;
        }
    }
}
