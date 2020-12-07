namespace TemperatureConverterMain.MvcModel.Scales
{
    class Celsius : IScale
    {
        public double Degrees { get; set; }

        public string Name { get; } = "Цельсия";

        public double GetDefoultValue()
        {
            return Degrees;
        }

        public double GetValueAboutOtherScale(IScale otherScale)
        {
            return otherScale.GetDefoultValue();
        }
    }
}
