using Minesweeper.Modul.Parametrs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper.Modul
{
    public class FieldParametrs
    {
        public event Action SetFlagEvent;
        public event Action RemoveFlagEvent;

        private int flagsCount;
        private List<IParametr> parametrs = new List<IParametr>
        {
            new Beginner(),
            new Fun(),
            new Professional()
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

        public FieldParametrs()
        {
            SetParametrs(parametrs[0].Name);
        }

        public void SetParametrs(string parametrName)
        {
            var parametr = parametrs.FirstOrDefault(x => x.Name == parametrName);

            if (parametr == null)
            {
                throw new ArgumentException($"Имя параметра {parametrName} нет. Доступны: {string.Join(", ", parametrs.Select(x => x.Name).ToList())}", parametrName);
            }

            SetParametrs(parametr.RowsCount, parametr.ColumnsCount, parametr.MinesCount);
        }

        public void SetParametrs(int rowsCount, int columnsCount, int minesCount)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            MinesCount = minesCount;
            FlagsCount = minesCount;
            Visited = new bool[rowsCount, columnsCount];
        }

        public string[] GetNamesParametrs()
        {
            return parametrs.Select(x => x.Name).ToArray();
        }
    }
}
