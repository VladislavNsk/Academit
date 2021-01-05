namespace Minesweeper.Model.Parameters
{
    public interface IGameDifficultyParameter
    {
        int RowsCount { get; }

        int ColumnsCount { get; }

        int MinesCount { get; }

        string Name { get; set; }
    }
}
