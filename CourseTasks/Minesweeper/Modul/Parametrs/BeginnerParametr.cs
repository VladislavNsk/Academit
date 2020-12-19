namespace Minesweeper.Modul.Parametrs
{
    public class BeginnerParametr : IParametr
    {
        public int RowsCount => 9;

        public int ColumnsCount => 9;

        public int MinesCount => 10;

        public string Name { get; set; } = "Легкий";
    }
}
