using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public interface IView
    {
        event Action SetParametrs;
        event Action LoadFormEvent;
        event Action SetSpecialParametrs;
        event Action ShowScoreTableEvent;
        event Action<Control> SetFlagEvent;
        event Action<Control> RemoveFlagEvent;
        event Action<Control, int, int> LeftButtonClick;

        string GetParametrName();

        (int rowsCount, int columnsCount, int minesCount) GetSpecialParametrs();

        void GameOver(int[,] minesCoordinates);

        void OpenCell(Control control, int value);

        void SetFlag(Control control, bool isCanGetFlag);

        void RemoveFlag(Control control);

        void PrintPlayingField(int rowsCount, int columnsCount, int minesCount);

        void WinGame(int[,] minesCoordinates);

        void OpenCellsRange(List<int[]> cellsCoordinates, List<int> values);

        void ChangeFlagsCount(int flagsCount);

        void ShowScoreTable(Dictionary<string, int> scoreTable);

        void SetParametrsNames(string[] parametrsNames);

        string GetPlayerName();
    }
}
