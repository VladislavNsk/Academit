namespace TemperatureConverterMain.Model.Scales
{
    class KelvinScale : IScale
    {
        public double Degrees { get; set; }

        public string Name => "Кельвина";

        public double GetDefaultValue()
        {
            return Degrees - 273.15;
        }

        public double GetValueAboutOtherScale(IScale otherScale)
        {
            return otherScale.GetDefaultValue() + 273.15;
        }
    }
}
