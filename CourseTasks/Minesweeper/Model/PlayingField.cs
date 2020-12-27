using Minesweeper.modul.DateBase;

using System;
using System.Collections.Generic;

namespace Minesweeper.Model
{
    public class PlayingField
    {
        public event Action ChangeParameters;
        public event Action<int[,]> Win;
        public event Action<int[,]> GameOver;
        public event Action<int> ChangeFlagsCountAction;
        public event Action<List<int[]>, List<int>> OpenCellsRangeEvent;

        private readonly HighScoreTable scoreTable;
        private readonly FieldParameters fieldParameters;
        private readonly DataBase dataBase;

        private int[,] playingField;

        public PlayingField()
        {
            dataBase = new DataBase();
            fieldParameters = new FieldParameters();
            scoreTable = new HighScoreTable(dataBase);

            scoreTable.SetMaxScore(fieldParameters.RowsCount * fieldParameters.ColumnsCount);

            scoreTable.Win += ScoreTable_Win;
            fieldParameters.SetFlagEvent += ChangeFlagsCount;
            fieldParameters.RemoveFlagEvent += ChangeFlagsCount;
        }

        private void ChangeFlagsCount()
        {
            ChangeFlagsCountAction?.Invoke(fieldParameters.FlagsCount);
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
            if (scoreTable.Score == fieldParameters.MinesCount - fieldParameters.FlagsCount)
            {
                FillPlayingField(rowIndex, columnIndex);
            }

            fieldParameters.Visited[rowIndex, columnIndex] = true;

            if (playingField[rowIndex, columnIndex] == -1)
            {
                GameOver?.Invoke(playingField);
                scoreTable.Save(false);
                return -1;
            }

            if (playingField[rowIndex, columnIndex] != 0)
            {
                scoreTable.Score++;
                CheckForWin();
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
            scoreTable.Score += cellsValues.Count;
            CheckForWin();
        }

        private void CheckForWin()
        {
            if (scoreTable.TotalScore - scoreTable.Score == fieldParameters.FlagsCount)
            {
                Win?.Invoke(playingField);
            }
        }

        public void SetFlag()
        {
            if (fieldParameters.FlagsCount != 0)
            {
                fieldParameters.FlagsCount--;
                scoreTable.Score++;
            }
        }

        public void RemoveFlag()
        {
            if (fieldParameters.FlagsCount != fieldParameters.MinesCount)
            {
                scoreTable.Score--;
                fieldParameters.FlagsCount++;
            }
        }

        public void SetParameters(int rowsCount, int columnsCount, int minesCount)
        {
            fieldParameters.SetParameters(rowsCount, columnsCount, minesCount);
            scoreTable.SetMaxScore(rowsCount * columnsCount);

            ChangeParameters?.Invoke();
        }

        public void SetParameters(string parametrName)
        {
            fieldParameters.SetParameters(parametrName);
            scoreTable.SetMaxScore(fieldParameters.RowsCount * fieldParameters.ColumnsCount);

            ChangeParameters?.Invoke();
        }

        public string[] GetNamesParameters()
        {
            return fieldParameters.GetNamesParameters();
        }

        private void ScoreTable_Win()
        {
            Win?.Invoke(playingField);
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

        public Dictionary<string, int> GetScoreTable()
        {
            return scoreTable.GetScoreTable();
        }

        public bool IsCanGetFlag()
        {
            return fieldParameters.FlagsCount != 0;
        }

        public void AddPlayerName(string playerName)
        {
            scoreTable.Add(playerName);
        }

        public (int rowsCount, int columnsCount, int minesCount) GetParameters(string parameterName)
        {
            return fieldParameters.GetParameters(parameterName);
        }
    }
}
