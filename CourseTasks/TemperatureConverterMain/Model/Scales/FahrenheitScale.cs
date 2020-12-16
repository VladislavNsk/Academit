namespace TemperatureConverterMain.Model.Scales
{
    public class FahrenheitScale : IScale
    {
        public double Degrees { get; set; }

        public string Name => "Фаренгейта";

        public double GetDefaultValue()
        {
            return (Degrees - 32) * 5 / 9;
        }

        public double GetValueAboutOtherScale(IScale otherScale)
        {
            return otherScale.GetDefaultValue() * 9 / 5 + 32;
        }
    }
}
