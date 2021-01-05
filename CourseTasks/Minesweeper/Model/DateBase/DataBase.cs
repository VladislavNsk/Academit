using System.Linq;
using System.Data.Linq;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Minesweeper.Model.DateBase
{
    public class DataBase
    {
        private static readonly SQLiteConnection connection = new SQLiteConnection("Data Source=MyDb.sqlite; Version = 3");
        private readonly DataContext context = new DataContext(connection);

        public DataBase(FieldParameters fieldParameters)
        {
            if (!context.DatabaseExists())
            {
                CreateTables();
                FillParametersTable(fieldParameters);
            }
        }

        private void CreateTables()
        {
            context.ExecuteCommand("CREATE TABLE IF NOT EXISTS 'players'(" +
                                   "Id INTEGER NOT NULL PRIMARY KEY," +
                                   "Name TEXT NOT NULL" +
                                   ")");

            context.ExecuteCommand("CREATE TABLE IF NOT EXISTS 'gamesTime'(" +
                                   "Id INTEGER NOT NULL PRIMARY KEY," +
                                   "SecondsCount INTEGER NOT NULL," +
                                   "PlayerNameId INTEGER NOT NULL," +
                                   "ParameterNameId INTEGER NOT NULL," +
                                   "FOREIGN KEY(PlayerNameId) REFERENCES players(Id)," +
                                   "FOREIGN KEY(ParameterNameId) REFERENCES gameDifficultyParameter(Id)" +
                                   ")");

            context.ExecuteCommand("CREATE TABLE IF NOT EXISTS 'gameDifficultyParameters'(" +
                                   "Id INTEGER NOT NULL PRIMARY KEY," +
                                   "ParameterName TEXT NOT NULL" +
                                   ")");
        }

        private void FillParametersTable(FieldParameters fieldParameters)
        {
            var gameDifficultyParameter = context.GetTable<GameDifficultyParameter>();

            if (gameDifficultyParameter.Count() != 0)
            {
                return;
            }

            var parametersNames = fieldParameters.GetParametersNames();
            int i = 1;

            foreach (var parameterName in parametersNames)
            {
                gameDifficultyParameter.InsertOnSubmit(new GameDifficultyParameter
                {
                    Id = i,
                    ParameterName = parameterName
                });

                i++;
            }

            context.SubmitChanges();
        }

        private void AddNewResult(int playerId, int secondsCount, string parameterName)
        {
            var playerResultFromDb = context.GetTable<GameTime>();
            var parametersFromDb = context.GetTable<GameDifficultyParameter>();

            var parameterId = parametersFromDb.AsEnumerable().First(x => x.ParameterName == parameterName).Id;
            var playerResult = playerResultFromDb.AsEnumerable().FirstOrDefault(x => x.ParameterNameId == parameterId && x.PlayerNameId == playerId);

            if (playerResult == null)
            {
                playerResultFromDb.InsertOnSubmit(new GameTime
                {
                    Id = playerResultFromDb.Count() + 1,
                    ParameterNameId = parameterId,
                    PlayerNameId = playerId,
                    SecondsCount = secondsCount
                });

                context.SubmitChanges();
                return;
            }

            if (playerResult.SecondsCount > secondsCount)
            {
                playerResult.SecondsCount = secondsCount;
            }

            context.SubmitChanges();
        }

        public void Add(int secondsCount, string parameterName, string playerName)
        {
            var playerFromDb = context.GetTable<Player>();
            var player = playerFromDb.AsEnumerable().FirstOrDefault(x => x.Name == playerName);

            if (player != null)
            {
                AddNewResult(player.Id, secondsCount, parameterName);
                return;
            }

            int id = playerFromDb.Count() + 1;

            playerFromDb.InsertOnSubmit(new Player
            {
                Name = playerName,
                Id = id
            });

            context.SubmitChanges();
            AddNewResult(id, secondsCount, parameterName);
        }

        public Dictionary<string, int> GetScoreTable(string parameterName)
        {
            var playerFromDb = context.GetTable<Player>();
            var playerResultFromDb = context.GetTable<GameTime>();
            var parametersFromDb = context.GetTable<GameDifficultyParameter>();

            var parameterId = parametersFromDb.AsEnumerable().First(x => x.ParameterName == parameterName).Id;
            var playersNames = playerFromDb.ToDictionary(x => x.Id, x => x.Name);

            return playerResultFromDb.Where(x => x.ParameterNameId == parameterId).ToDictionary(x => playersNames[x.PlayerNameId], x => x.SecondsCount);
        }
    }
}
