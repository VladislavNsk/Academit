namespace TemperatureConverterMain.Model.Scales
{
    public class FahrenheitScale : IScale
    {
        public string Name => "Фаренгейта";

        public double GetDefaultTemperatureInScale(double degrees)
        {
            return (degrees - 32) * 5 / 9;
        }

        public double GetTemperatureInCurrentScale(IScale otherScale, double degrees)
        {
            return otherScale.GetDefaultTemperatureInScale(degrees) * 9 / 5 + 32;
        }
    }
}
