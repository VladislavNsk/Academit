namespace TemperatureConverterMain.Model.Scales
{
    public class CelsiusScale : IScale
    {
        public double Degrees { get; set; }

        public string Name => "Цельсия";

        public double GetDefaultValue()
        {
            return Degrees;
        }

        public double GetValueAboutOtherScale(IScale otherScale)
        {
            return otherScale.GetDefaultValue();
        }
    }
}
