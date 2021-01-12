namespace TemperatureConverterMain.Model.Scales
{
    public interface IScale
    {
        string Name { get; }

        double GetDefaultTemperatureInScale(double degrees);

        double GetTemperatureInCurrentScale(IScale otherScale, double degrees);
    }
}
