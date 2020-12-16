using System;

namespace TemperatureConverterMain.View
{
    public interface IView
    {
        event Action ConvertTemperature;
        event Action LoadForm;

        void SetResultDegrees(string degrees);

        void AddScale(string scaleName);

        void RemoveScale(string scaleName);

        void AddScaleRange(string[] scalesNames);

        string SourceScale { get; }

        string ResultScale { get; }

        string SourceDegrees { get; }
    }
}
