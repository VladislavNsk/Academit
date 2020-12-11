using System;
using System.Collections.Generic;

namespace Minesweeper.Modul
{
    public class HighScoreTable
    {
        public event Action Win;
        private readonly Dictionary<string, int> scoreTable;
        private string currentPlayer;
        private int currentScore;
        private int totalScore;

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

        public HighScoreTable()
        {
            scoreTable = new Dictionary<string, int>();
        }

        public void Save(bool isWin)
        {
            if (isWin)
            {
                Score += totalScore;
            }

            scoreTable[currentPlayer] += Score;
        }

        public void Add(string name)
        {
            string playerName = name;

            if (playerName.Length == 0)
            {
                playerName = "unknown";
            }

            if (!scoreTable.ContainsKey(playerName))
            {
                scoreTable.Add(playerName, 0);
                currentPlayer = playerName;
            }
        }

        public Dictionary<string, int> GetScoreTable()
        {
            return scoreTable;
        }

        public void SetMaxScore(int totalScore)
        {
            this.totalScore = totalScore;
            currentScore = 0;
        }
    }
}
