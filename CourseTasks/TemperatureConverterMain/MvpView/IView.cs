using System;

namespace TemperatureConverterMain.MvpView
{
    public interface IView
    {
        void SetResultDegrees(string degrees);

        void AddScale(string scaleName);

        void RemoveScale(string scaleName);

        void AddScaleRange(string[] scaleName);

        string SourceScale { get; }

        string ResultScale { get; }

        string SourceDegrees { get; }

        event EventHandler ConvertTemperature;
        event EventHandler LoadForm;
    }
}
