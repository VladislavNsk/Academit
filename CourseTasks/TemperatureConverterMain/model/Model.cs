using System;
using TemperatureConverterMain.Model.Scales;

namespace TemperatureConverterMain.Model
{
    public class Model
    {
        private readonly ScalesList scalesList;
        public event Action<double> Changes;
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

        public string[] GetNames()
        {
            return scalesList.GetNames();
        }

        public void Convert(string sourceScale, string resultScale, double degrees)
        {
            var scaleFrom = scalesList.GetScale(sourceScale);
            var scaleTo = scalesList.GetScale(resultScale);
            var result = scaleTo.GetTemperatureInCurrentScale(scaleFrom, degrees);

            Changes?.Invoke(result);
        }
    }
}
