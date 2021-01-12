namespace TemperatureConverterMain.Model.Scales
{
    public class KelvinScale : IScale
    {
        public string Name => "Кельвина";

        public double GetDefaultTemperatureInScale(double degrees)
        {
            return degrees - 273.15;
        }

        public double GetTemperatureInCurrentScale(IScale otherScale, double degrees)
        {
            return otherScale.GetDefaultTemperatureInScale(degrees) + 273.15;
        }
    }
}
