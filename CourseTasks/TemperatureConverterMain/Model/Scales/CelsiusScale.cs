namespace TemperatureConverterMain.Model.Scales
{
    public class CelsiusScale : IScale
    {
        public string Name => "Цельсия";

        public double GetDefaultTemperatureInScale(double degrees)
        {
            return degrees;
        }

        public double GetTemperatureInCurrentScale(IScale otherScale, double degrees)
        {
            return otherScale.GetDefaultTemperatureInScale(degrees);
        }
    }
}
