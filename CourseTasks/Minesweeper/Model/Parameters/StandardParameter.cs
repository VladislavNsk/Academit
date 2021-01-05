namespace Minesweeper.Model.Parameters
{
    public class StandardParameter : IGameDifficultyParameter
    {
        public int RowsCount => 16;

        public int ColumnsCount => 16;

        public int MinesCount => 40;

        public string Name { get; set; } = "Средний";
    }
}
