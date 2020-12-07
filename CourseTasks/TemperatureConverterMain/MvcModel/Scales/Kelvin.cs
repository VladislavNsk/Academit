namespace TemperatureConverterMain.MvcModel.Scales
{
    class Kelvin : IScale
    {
        public double Degrees { get; set; }

        public string Name { get; } = "Кельвина";

        public double GetDefoultValue()
        {
          return  Degrees - 273.15;
        }

        public double GetValueAboutOtherScale(IScale otherScale)
        {
            return otherScale.GetDefoultValue() +273.15;
        }
    }
}
