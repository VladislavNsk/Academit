namespace TemperatureConverterMain.MvpModel.Scales
{
    class Fahrenheit : IScale
    {
        public double Degrees { get; set; }

        public string Name { get; } = "Фаренгейта";

        public double GetDefoultValue()
        {
            return (Degrees - 32) * 5 / 9;
        }

        public double GetValueAboutOtherScale(IScale otherScale)
        {
            return otherScale.GetDefoultValue() * 9 / 5 + 32;
        }
    }
}
