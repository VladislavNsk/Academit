namespace TemperatureConverterMain.MvcModel.Scales
{
   public  interface IScale
    {
        string Name { get; }

        double Degrees { get; set; }

        double GetDefoultValue();

        double GetValueAboutOtherScale(IScale otherScale);
    }
}
