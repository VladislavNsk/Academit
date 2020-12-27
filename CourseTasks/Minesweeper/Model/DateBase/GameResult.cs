using System.Data.Linq.Mapping;

namespace Minesweeper.Model.DateBase
{
    [Table(Name = "scoreTables")]
    class GameResult
    {
        [Column(IsPrimaryKey = true)]
        public string Name { get; set; }

        [Column]
        public int Score { get; set; }
    }
}
