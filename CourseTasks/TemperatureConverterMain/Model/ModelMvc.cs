using System;
using System.Collections.Generic;

namespace TemperatureConverterMain.Model
{
    public class ModelMvc
    {
        private double celsius = 0;
        private double kelvin = 273.15;
        private double fahrenheit = 32;
        private string errorMessage;
        Dictionary<string, double> scaleDictionary;
        public event Action Changes;
        public event Action Error;

        public string Message
        {
            get
            {
                return errorMessage;
            }

            set
            {
                errorMessage = value;
                Error?.Invoke();
            }
        }

        public double Celsius
        {
            get
            {
                return celsius;
            }

            set
            {
                if (celsius == value)
                {
                    return;
                }

                celsius = value;
                kelvin = celsius + 273.15;
                fahrenheit = celsius * 9 / 5 + 32;
            }
        }

        public double Kelvin
        {
            get
            {
                return kelvin;
            }

            set
            {
                if (kelvin == value)
                {
                    return;
                }

                kelvin = value;
                celsius = kelvin - 273.15;
                fahrenheit = (kelvin - 273.15) * 9 / 5 + 32;
            }
        }

        public double Fahrenheit
        {
            get
            {
                return fahrenheit;
            }

            set
            {
                if (fahrenheit == value)
                {
                    return;
                }

                fahrenheit = value;
                kelvin = (fahrenheit - 32) * 5 / 9 + 273.15;
                celsius = (fahrenheit - 32) * 5 / 9;
            }
        }

        public void Convert(string sourceScale, string resultScale, string degreesLine)
        {
            if (!double.TryParse(degreesLine, out double degrees))
            {
                Message = $"Значение градуса не число ({degreesLine})";
                return;
            }

            if (sourceScale == "")
            {
                Message = "Не задана исходная шкалы градусов";
                return;
            }

            if (resultScale == "")
            {
                Message = "Не задана итоговая шкалы градусов";
                return;
            }

            scaleDictionary = new Dictionary<string, double>
            {
                ["Фаренгейта"] = Fahrenheit,
                ["Кельвина"] = Kelvin,
                ["Цельсия"] = Celsius
            };

            if (!scaleDictionary.ContainsKey(sourceScale) || !scaleDictionary.ContainsKey(resultScale))
            {
                Message = "Шкала не найдена";
                return;
            }

            scaleDictionary[sourceScale] = degrees;

            UpdateDictionary(scaleDictionary);
            Changes?.Invoke();
        }

        private void UpdateDictionary(Dictionary<string, double> scaleDictionary)
        {
            if (Fahrenheit != scaleDictionary["Фаренгейта"])
            {
                Fahrenheit = scaleDictionary["Фаренгейта"];
                scaleDictionary["Кельвина"] = Kelvin;
                scaleDictionary["Цельсия"] = Celsius;
                return;
            }

            if (Kelvin != scaleDictionary["Кельвина"])
            {
                Kelvin = scaleDictionary["Кельвина"];
                scaleDictionary["Фаренгейта"] = Fahrenheit;
                scaleDictionary["Цельсия"] = Celsius;
                return;
            }

            if (Celsius != scaleDictionary["Цельсия"])
            {
                Celsius = scaleDictionary["Цельсия"];
                scaleDictionary["Кельвина"] = Kelvin;
                scaleDictionary["Фаренгейта"] = Fahrenheit;
                return;
            }
        }

        public double GetResult(string resultScale)
        {
            return scaleDictionary[resultScale];
        }
    }
}
