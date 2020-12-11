using System;
using System.Collections.Generic;

namespace Minesweeper.Modul
{
    public class PlayingField
    {
        public event Action ChangeParameters;
        public event Action<int[,]> Win;
        public event Action<int[,]> GameOver;
        public event Action<int> ChangeFlagsCountAction;
        public event Action<List<int[]>, List<int>> OpenCellsRangeEvent;

        private readonly HighScoreTable scoreTable;
        private readonly FieldParametrs fieldParametrs;
        private int[,] playingField;

        public PlayingField()
        {
            scoreTable = new HighScoreTable();
            fieldParametrs = new FieldParametrs();

            scoreTable.SetMaxScore(fieldParametrs.RowsCount * fieldParametrs.ColumnsCount);
            FillPlatingField();

            scoreTable.Win += ScoreTable_Win;
            fieldParametrs.SetFlagEvent += ChangeFlagsCount;
            fieldParametrs.RemoveFlagEvent += ChangeFlagsCount;
        }

        private void ChangeFlagsCount()
        {
            ChangeFlagsCountAction?.Invoke(fieldParametrs.FlagsCount);
        }

        private void FillPlatingField()
        {
            playingField = new int[fieldParametrs.RowsCount, fieldParametrs.ColumnsCount];
            int minesCount = 0;
            Random random = new Random();

            while (minesCount < fieldParametrs.MinesCount)
            {
                int rowIndex = random.Next(0, fieldParametrs.RowsCount);
                int columnIndex = random.Next(0, fieldParametrs.ColumnsCount);

                if (playingField[rowIndex, columnIndex] != -1)
                {
                    playingField[rowIndex, columnIndex] = -1;
                    GetNearestMinesCount(rowIndex, columnIndex);

                    minesCount++;
                }
            }
        }

        private void GetNearestMinesCount(int rowIndex, int columnIndex)
        {
            for (int i = rowIndex - 1; i <= rowIndex + 1; i++)
            {
                if (i < 0 || i >= fieldParametrs.RowsCount)
                {
                    continue;
                }

                for (int j = columnIndex - 1; j <= columnIndex + 1; j++)
                {
                    if (j < 0 || j >= fieldParametrs.ColumnsCount || playingField[i, j] == -1 || (i == rowIndex && j == columnIndex))
                    {
                        continue;
                    }

                    playingField[i, j]++;
                }
            }
        }

        public int GetCellValue(int rowIndex, int columnIndex)
        {
            fieldParametrs.Visited[rowIndex, columnIndex] = true;

            if (playingField[rowIndex, columnIndex] == -1)
            {
                GameOver?.Invoke(playingField);
                scoreTable.Save(false);
            }

            if (playingField[rowIndex, columnIndex] != 0)
            {
                scoreTable.Score++;
                return playingField[rowIndex, columnIndex];
            }

            OpenCellsRange(new int[] { rowIndex, columnIndex });

            return 0;
        }

        private void OpenCellsRange(int[] firstEmptyCellCoordinates)
        {
            Queue<int[]> queue = new Queue<int[]>();
            List<int[]> cellsCoordinates = new List<int[]> { firstEmptyCellCoordinates };
            List<int> cellsValues = new List<int> { playingField[firstEmptyCellCoordinates[0], firstEmptyCellCoordinates[1]] };
            fieldParametrs.Visited[firstEmptyCellCoordinates[0], firstEmptyCellCoordinates[1]] = true;
            int[] cellCoordinates;
            queue.Enqueue(firstEmptyCellCoordinates);

            while (queue.Count != 0)
            {
                cellCoordinates = queue.Dequeue();

                if (playingField[cellCoordinates[0], cellCoordinates[1]] > 0)
                {
                    continue;
                }

                for (int i = cellCoordinates[0] - 1; i <= cellCoordinates[0] + 1; i++)
                {
                    if (i < 0 || i >= fieldParametrs.RowsCount)
                    {
                        continue;
                    }

                    for (int j = cellCoordinates[1] - 1; j <= cellCoordinates[1] + 1; j++)
                    {
                        if (j < 0 || j >= fieldParametrs.ColumnsCount || (i == cellCoordinates[0] && j == cellCoordinates[1]))
                        {
                            continue;
                        }

                        if (playingField[i, j] != -1 && fieldParametrs.Visited[i, j] == false)
                        {
                            fieldParametrs.Visited[i, j] = true;
                            cellsValues.Add(playingField[i, j]);
                            cellsCoordinates.Add(new int[] { i, j });
                            queue.Enqueue(new int[] { i, j });
                        }
                    }
                }
            }

            OpenCellsRangeEvent?.Invoke(cellsCoordinates, cellsValues);
            scoreTable.Score += cellsValues.Count;
        }

        public void SetFlag()
        {
            if (fieldParametrs.FlagsCount != 0)
            {
                fieldParametrs.FlagsCount--;
                scoreTable.Score++;
            }
        }

        public void RemoveFlag()
        {
            if (fieldParametrs.FlagsCount != fieldParametrs.MinesCount)
            {
                scoreTable.Score--;
                fieldParametrs.FlagsCount++;
            }
        }

        public void SetParametrs(int rowsCount, int columnsCount, int minesCount)
        {
            fieldParametrs.SetParametrs(rowsCount, columnsCount, minesCount);
            scoreTable.SetMaxScore(rowsCount * columnsCount);
            FillPlatingField();

            ChangeParameters?.Invoke();
        }

        public void SetParametrs(string parametrName)
        {
            fieldParametrs.SetParametrs(parametrName);
            scoreTable.SetMaxScore(fieldParametrs.RowsCount * fieldParametrs.ColumnsCount);
            FillPlatingField();

            ChangeParameters?.Invoke();
        }

        public string[] GetNamesParametrs()
        {
            return fieldParametrs.GetNamesParametrs();
        }

        private void ScoreTable_Win()
        {
            Win?.Invoke(playingField);
        }

        public int GetRowsCount()
        {
            return fieldParametrs.RowsCount;
        }

        public int GetColumnsCount()
        {
            return fieldParametrs.ColumnsCount;
        }

        public int GetMinesCount()
        {
            return fieldParametrs.MinesCount;
        }

        public Dictionary<string, int> GetScoreTable()
        {
            return scoreTable.GetScoreTable();
        }

        public bool IsCanGetFlag()
        {
            return fieldParametrs.FlagsCount != 0;
        }

        public void AddPlayerName(string playerName)
        {
            scoreTable.Add(playerName);
        }
    }
}
