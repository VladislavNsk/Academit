using Minesweeper.modul.DateBase;
using System;
using System.Collections.Generic;

namespace Minesweeper.Modul
{
    public class HighScoreTable
    {
        public event Action Win;
        private readonly DataBase dataBase;
        private string playerName;
        private int currentScore;
        private int totalScore;

        public HighScoreTable(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public int Score
        {
            get
            {
                return currentScore;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Значение value ({value}) должно быть больше 0.", nameof(value));
                }

                currentScore = value;

                if (value == totalScore)
                {
                    Save(true);
                    Win?.Invoke();
                }
            }
        }

        public void Save(bool isWin)
        {
            if (isWin)
            {
                Score += totalScore;
            }

            dataBase.Save(Score, playerName);
        }

        public void Add(string name)
        {
            playerName = name;

            if (playerName.Length == 0)
            {
                playerName = "Безымянный";
            }

            dataBase.Add(playerName);
        }

        public Dictionary<string, int> GetScoreTable()
        {
            return dataBase.GetScoreTable();
        }

        public void SetMaxScore(int totalScore)
        {
            this.totalScore = totalScore;
            currentScore = 0;
        }
    }
}
