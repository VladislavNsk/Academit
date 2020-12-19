using System.Data.Linq;
using System.Data.SQLite;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;

namespace Minesweeper.modul.DateBase
{
    public class DataBase
    {
        private static readonly SQLiteConnection connection = new SQLiteConnection("Data Source=MyDb.sqlite; Version = 3");
        private readonly DataContext context = new DataContext(connection);

        public DataBase()
        {
            if (!context.DatabaseExists())
            {
                CreateTable();
            }
        }

        private void CreateTable()
        {
            context.ExecuteCommand("CREATE TABLE IF NOT EXISTS 'scoreTables'(" +
                                   "Name TEXT NOT NULL PRIMARY KEY," +
                                   "Score INTEGER NOT NULL" +
                                   ")");
        }

        public void Add(string playerName)
        {
            var table = new GameResult { Name = playerName };
            var tableFromDb = context.GetTable<GameResult>();

            if (!tableFromDb.Where(x => x.Name == playerName).Any())
            {
                tableFromDb.InsertOnSubmit(table);
                context.SubmitChanges();
            }
        }

        public void Save(int score, string playerName)
        {
            var tableFromDb = context.GetTable<GameResult>();
            var sampleResult = from table in tableFromDb
                               where table.Name == playerName
                               select table;

            foreach (var row in sampleResult)
            {
                row.Score = score;
            }

            context.SubmitChanges();
        }

        public Dictionary<string, int> GetScoreTable()
        {
            var tableFromDb = context.GetTable<GameResult>();
            var scoreTable = new Dictionary<string, int>();
            var sampleResult = (from table in tableFromDb
                                select table).OrderByDescending(x => x.Score);

            foreach (var row in sampleResult)
            {
                scoreTable.Add(row.Name, row.Score);
            }

            return scoreTable;
        }
    }

    [Table(Name = "scoreTables")]
    public class GameResult
    {
        [Column(IsPrimaryKey = true)]
        public string Name { get; set; }

        [Column]
        public int Score { get; set; }
    }
}
