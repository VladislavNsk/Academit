namespace TemperatureConverterMain.Model.Scales
{
    public interface IScale
    {
        string Name { get; }

        double Degrees { get; set; }

        double GetDefaultValue();

        double GetValueAboutOtherScale(IScale otherScale);
    }
}
