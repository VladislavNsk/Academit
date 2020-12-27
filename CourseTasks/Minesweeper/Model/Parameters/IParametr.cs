namespace Minesweeper.Model.Parameters
{
    public interface IParameter
    {
        int RowsCount { get; }

        int ColumnsCount { get; }

        int MinesCount { get; }

        string Name { get; set; }
    }
}
