using System;
using System.Linq;
using System.Collections.Generic;

using Minesweeper.Model.Parameters;

namespace Minesweeper.Model
{
    public class FieldParameters
    {
        public event Action WinEvent;
        public event Action SetFlagEvent;
        public event Action RemoveFlagEvent;

        private int flagsCount;
        private int openedCells;
        private int cellsWithoutMinesCount;

        private readonly List<IGameDifficultyParameter> parameters = new List<IGameDifficultyParameter>
        {
            new BeginnerParameter(),
            new StandardParameter(),
            new ProfessionalParameter()
        };

        public bool[,] Visited { get; set; }

        public int MinesCount { get; private set; }

        public int RowsCount { get; private set; }

        public int ColumnsCount { get; private set; }

        public int OpenedCells
        {
            get => openedCells;
            set
            {
                if (value < 0 || value > cellsWithoutMinesCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Количество открытых ячеек может быть в пределах от 0 до {cellsWithoutMinesCount}");
                }

                openedCells = value;

                if (OpenedCells == cellsWithoutMinesCount)
                {
                    WinEvent?.Invoke();
                }
            }
        }

        public int FlagsCount
        {
            get => flagsCount;
            set
            {
                if (value < 0 || value > MinesCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Количество флажков должно быть в пределах от 0 до {MinesCount}");
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
            openedCells = 0;
            cellsWithoutMinesCount = RowsCount * ColumnsCount - MinesCount;
        }

        public (int rowsCount, int columnsCount, int minesCount) GetParameters(string parameterName)
        {
            SetParameters(parameterName);
            return (RowsCount, ColumnsCount, MinesCount);
        }

        public string[] GetParametersNames()
        {
            return parameters.Select(x => x.Name).ToArray();
        }
    }
}
