using System;

namespace TemperatureConverterMain.View
{
    public interface IView
    {
        void SetResultDegrees(string value);

        void PrintError(string err);

        string SourceTemperatureScale { get; }

        string ResultTemperatureScale { get; }

        string SourseDegrees { get; }

        event EventHandler ConvertButton_ClickEventHandler;
    }
}
