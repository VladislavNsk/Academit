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
        //private TableDb table;

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
                                   "Name TEXT NOT NULL PRIMARY KEY ," +
                                   "Score INTEGER NOT NULL" +
                                   ")");
        }

        public void Add(string playerName)
        {
            var table = new TableDb { Name = playerName};
            var tableFromDb = context.GetTable<TableDb>();

            if (!tableFromDb.Where(x => x.Name == playerName).Any())
            {
                tableFromDb.InsertOnSubmit(table);
                context.SubmitChanges();
            }
        }

        public void Save(int score, string playerName)
        {
            Table<TableDb> tableFromDb = context.GetTable<TableDb>();
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
            Table<TableDb> tableFromDb = context.GetTable<TableDb>();
            var scoreTable = new Dictionary<string, int>();
            var sampleResult = from table in tableFromDb
                               select table;

            foreach (var score in sampleResult)
            {
                scoreTable.Add(score.Name, score.Score);
            }

            return scoreTable;
        }
    }

    [Table(Name = "scoreTables")]
    public class TableDb
    {
        [Column(IsPrimaryKey = true)]
        public string Name { get; set; }

        [Column]
        public int Score { get; set; }
    }
}
