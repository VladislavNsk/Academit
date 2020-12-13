namespace Minesweeper.Modul.Parametrs
{
    public class Fun : IParametr
    {
        public int RowsCount => 16;

        public int ColumnsCount => 16;

        public int MinesCount => 40;

        public string Name { get; set; } = "Средний";
    }
}
