namespace Minesweeper.Modul.Parametrs
{
    public class Professional : IParametr
    {
        public int RowsCount => 16;

        public int ColumnsCount => 30;

        public int MinesCount => 99;

        public string Name { get; set; } = "Тяжелый";
    }
}
