namespace Minesweeper.Model.Parameters
{
    public class ProfessionalParameter : IGameDifficultyParameter
    {
        public int RowsCount => 16;

        public int ColumnsCount => 30;

        public int MinesCount => 99;

        public string Name { get; set; } = "Тяжелый";
    }
}
