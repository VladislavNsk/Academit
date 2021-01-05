using System.Data.Linq.Mapping;

namespace Minesweeper.Model.DateBase
{
    [Table(Name = "players")]
    public class Player
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }
    }
}
