using Minesweeper.Model.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.Model
{
    public class FieldParameters
    {
        public event Action SetFlagEvent;
        public event Action RemoveFlagEvent;

        private int flagsCount;
        private List<IParameter> parameters = new List<IParameter>
        {
            new BeginnerParameter(),
            new FunParameter(),
            new ProfessionalParameter()
        };

        public int MinesCount { get; private set; }

        public int RowsCount { get; private set; }

        public int ColumnsCount { get; private set; }

        public int FlagsCount
        {
            get => flagsCount;
            set
            {
                if (value < 0 || value > MinesCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Количество флагов должно быть в пределах от 0 до {MinesCount}");
                }

                if (value < flagsCount)
                {
                    flagsCount = value;
                    RemoveFlagEvent?.Invoke();
                    return;
                }

                if (value > flagsCount)
                {
                    flagsCount = value;
                    SetFlagEvent?.Invoke();
                }
            }
        }

        public bool[,] Visited { get; set; }

        public FieldParameters()
        {
            SetParameters(parameters[0].Name);
        }

        public void SetParameters(string parameterName)
        {
            var parameter = parameters.FirstOrDefault(x => x.Name == parameterName);

            if (parameter == null)
            {
                throw new ArgumentException($"Имя параметра {parameterName} нет. Доступны: {string.Join(", ", parameters.Select(x => x.Name).ToList())}", parameterName);
            }

            SetParameters(parameter.RowsCount, parameter.ColumnsCount, parameter.MinesCount);
        }

        public void SetParameters(int rowsCount, int columnsCount, int minesCount)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            MinesCount = minesCount;
            FlagsCount = minesCount;
            Visited = new bool[rowsCount, columnsCount];
        }

        public (int rowsCount, int columnsCount, int minesCount) GetParameters(string parameterName)
        {
            SetParameters(parameterName);
            return (RowsCount, ColumnsCount, MinesCount);
        }

        public string[] GetNamesParameters()
        {
            return parameters.Select(x => x.Name).ToArray();
        }
    }
}
