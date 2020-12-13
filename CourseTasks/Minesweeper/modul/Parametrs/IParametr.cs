namespace Minesweeper.Modul.Parametrs
{
  public   interface IParametr
    {
        int RowsCount { get; }

        int ColumnsCount { get; }

        int MinesCount { get; }

        string Name { get; set; }
    }
}
