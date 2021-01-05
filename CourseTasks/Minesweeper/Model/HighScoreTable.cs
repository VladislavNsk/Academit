using System;
using System.Linq;
using System.Timers;
using System.Collections.Generic;

using Minesweeper.Model.DateBase;

namespace Minesweeper.Model
{
    public class HighScoreTable
    {
        public event Action<int> ChangeTimerValue;
        public event Action AddNewRecord;

        private readonly DataBase dataBase;
        private readonly Timer timer;

        private int secondsCount;
        private string currentParameterName;

        public HighScoreTable(DataBase dataBase)
        {
            this.dataBase = dataBase;
            timer = new Timer();
            timer.Elapsed += OnElapsed;
        }

        public Dictionary<string, int> GetScoreTable(string parameterName)
        {
            return dataBase.GetScoreTable(parameterName);
        }

        public void StartTimer()
        {
            secondsCount = 0;
            timer.Interval = 1000;
            timer.Start();
        }

        private void OnElapsed(object sender, ElapsedEventArgs e)
        {
            if (secondsCount >= 999)
            {
                timer.Stop();
                return;
            }

            secondsCount++;
            ChangeTimerValue?.Invoke(secondsCount);
        }

        public void Save(string parameterName)
        {
            currentParameterName = parameterName;
            var scoreTable = dataBase.GetScoreTable(currentParameterName);

            if (scoreTable.Count == 0 || scoreTable.Any(x => x.Value > secondsCount))
            {
                AddNewRecord?.Invoke();
            }
        }

        public void Add(string playerName)
        {
            dataBase.Add(secondsCount, currentParameterName, playerName);
        }

        public void StopTimer()
        {
            timer.Stop();
            ChangeTimerValue?.Invoke(0);
        }
    }
}
