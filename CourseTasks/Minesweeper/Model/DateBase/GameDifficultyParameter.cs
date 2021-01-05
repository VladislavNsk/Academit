using System.Data.Linq.Mapping;

namespace Minesweeper.Model.DateBase
{
    [Table(Name = "gameDifficultyParameters")]
    public class GameDifficultyParameter
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column]
        public string ParameterName { get; set; }
    }
}
