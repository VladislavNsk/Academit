using System;

namespace Minesweeper.Modul.Parametrs
{
    interface IParametr
    {
        int RowsCount { get; }

        int ColumnsCount { get; }

        int MinesCount { get; }

        string Name { get; set; }
    }
}
