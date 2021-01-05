using System.Data.Linq.Mapping;

namespace Minesweeper.Model.DateBase
{
    [Table(Name = "gamesTime")]
    public class GameTime
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public int SecondsCount { get; set; }

        [Column]
        public int PlayerNameId { get; set; }

        [Column]
        public int ParameterNameId { get; set; }
    }
}
