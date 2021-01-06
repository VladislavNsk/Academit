using System;
using System.Linq;
using System.Collections.Generic;

using Minesweeper.Model.DateBase;

namespace Minesweeper.Model
{
    public class PlayingField
    {
        public event Action AddNewRecordEvent;
        public event Action ChangeParametersEvent;
        public event Action<int> ChangeTimerValueEvent;
        public event Action<int> ChangeFlagsCountEvent;
        public event Action<int[,]> WinEvent;
        public event Action<int[,]> GameOverEvent;
        public event Action<List<int[]>, List<int>> OpenCellsRangeEvent;
        public event Func<string> GetParameterName;

        private readonly HighScoreTable scoreTable;
        private readonly FieldParameters fieldParameters;
        private readonly DataBase dataBase;

        private int[,] playingField;

        public PlayingField()
        {
            fieldParameters = new FieldParameters();
            dataBase = new DataBase(fieldParameters);
            scoreTable = new HighScoreTable(dataBase);

            fieldParameters.WinEvent += OnWinEvent;
            scoreTable.ChangeTimerValue += OnChangeTimerValue;
            fieldParameters.SetFlagEvent += OnChangeFlagsCount;
            fieldParameters.RemoveFlagEvent += OnChangeFlagsCount;
            scoreTable.AddNewRecord += OnAddNewRecord;
        }

        private void OnAddNewRecord()
        {
            AddNewRecordEvent?.Invoke();
        }

        private void OnChangeTimerValue(int secondsCount)
        {
            ChangeTimerValueEvent?.Invoke(secondsCount);
        }

        private void OnChangeFlagsCount()
        {
            ChangeFlagsCountEvent?.Invoke(fieldParameters.FlagsCount);
        }

        private void FillPlayingField(int selectedRowIndex, int selectedColumnIndex)
        {
            playingField = new int[fieldParameters.RowsCount, fieldParameters.ColumnsCount];
            var minesCount = 0;
            var random = new Random();

            while (minesCount < fieldParameters.MinesCount)
            {
                var rowIndex = random.Next(0, fieldParameters.RowsCount);
                var columnIndex = random.Next(0, fieldParameters.ColumnsCount);

                if (playingField[rowIndex, columnIndex] != -1 && selectedRowIndex != rowIndex && selectedColumnIndex != columnIndex)
                {
                    playingField[rowIndex, columnIndex] = -1;
                    FindNearestCells(rowIndex, columnIndex, (i, j) => playingField[i, j]++);

                    minesCount++;
                }
            }
        }

        private void FindNearestCells(int rowIndex, int columnIndex, Action<int, int> action)
        {
            for (var i = rowIndex - 1; i <= rowIndex + 1; i++)
            {
                if (i < 0 || i >= fieldParameters.RowsCount)
                {
                    continue;
                }

                for (var j = columnIndex - 1; j <= columnIndex + 1; j++)
                {
                    if (j < 0 || j >= fieldParameters.ColumnsCount || playingField[i, j] == -1 || (i == rowIndex && j == columnIndex))
                    {
                        continue;
                    }

                    action(i, j);
                }
            }
        }

        public int GetCellValue(int rowIndex, int columnIndex)
        {
            if (fieldParameters.OpenedCells == 0)
            {
                scoreTable.StartTimer();
                FillPlayingField(rowIndex, columnIndex);
            }

            fieldParameters.Visited[rowIndex, columnIndex] = true;

            if (playingField[rowIndex, columnIndex] == -1)
            {
                scoreTable.StopTimer();
                GameOverEvent?.Invoke(playingField);
                return -1;
            }

            if (playingField[rowIndex, columnIndex] != 0)
            {
                fieldParameters.OpenedCells++;
                return playingField[rowIndex, columnIndex];
            }

            OpenCellsRange(new int[] { rowIndex, columnIndex });

            return 0;
        }

        public void OpenCellsRange(int[] firstEmptyCellCoordinates)
        {
            var queue = new Queue<int[]>();
            var cellsCoordinates = new List<int[]> { firstEmptyCellCoordinates };
            var cellsValues = new List<int> { playingField[firstEmptyCellCoordinates[0], firstEmptyCellCoordinates[1]] };
            fieldParameters.Visited[firstEmptyCellCoordinates[0], firstEmptyCellCoordinates[1]] = true;
            queue.Enqueue(firstEmptyCellCoordinates);

            while (queue.Count != 0)
            {
                int[] cellCoordinates = queue.Dequeue();

                if (playingField[cellCoordinates[0], cellCoordinates[1]] > 0)
                {
                    continue;
                }

                FindNearestCells(cellCoordinates[0], cellCoordinates[1], (i, j) =>
                {
                    if (playingField[i, j] != -1 && fieldParameters.Visited[i, j] == false)
                    {
                        fieldParameters.Visited[i, j] = true;
                        cellsValues.Add(playingField[i, j]);
                        cellsCoordinates.Add(new int[] { i, j });
                        queue.Enqueue(new int[] { i, j });
                    }
                });
            }

            OpenCellsRangeEvent?.Invoke(cellsCoordinates, cellsValues);
            fieldParameters.OpenedCells += cellsValues.Count;
        }

        public void SetFlag()
        {
            if (fieldParameters.FlagsCount != 0)
            {
                fieldParameters.FlagsCount--;
            }
        }

        public void RemoveFlag()
        {
            if (fieldParameters.FlagsCount != fieldParameters.MinesCount)
            {
                fieldParameters.FlagsCount++;
            }
        }

        public void SetParameters(int rowsCount, int columnsCount, int minesCount)
        {
            fieldParameters.SetParameters(rowsCount, columnsCount, minesCount);
            ChangeParametersEvent?.Invoke();
            scoreTable.StopTimer();
        }

        public void SetParameters(string parameterName)
        {
            fieldParameters.SetParameters(parameterName);
            ChangeParametersEvent?.Invoke();
            scoreTable.StopTimer();
        }

        public string[] GetNamesParameters()
        {
            return fieldParameters.GetParametersNames();
        }

        private void OnWinEvent()
        {
            scoreTable.StopTimer();
            WinEvent?.Invoke(playingField);
            var parameterName = GetParameterName?.Invoke();

            if (fieldParameters.GetParametersNames().Contains(parameterName))
            {
                scoreTable.Save(parameterName);
            }
        }

        public void AddNewRecord(string playerName)
        {
            scoreTable.Add(playerName);
        }

        public int GetRowsCount()
        {
            return fieldParameters.RowsCount;
        }

        public int GetColumnsCount()
        {
            return fieldParameters.ColumnsCount;
        }

        public int GetMinesCount()
        {
            return fieldParameters.MinesCount;
        }

        public Dictionary<string, int> GetScoreTable(string parameterName)
        {
            return scoreTable.GetScoreTable(parameterName);
        }

        public bool IsCanGetFlag()
        {
            return fieldParameters.FlagsCount != 0;
        }

        public (int rowsCount, int columnsCount, int minesCount) GetParameters(string parameterName)
        {
            return fieldParameters.GetParameters(parameterName);
        }
    }
}
