using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public interface IView
    {
        event Action SetParametersEvent;
        event Action LoadFormEvent;
        event Action SetSpecialParametersEvent;
        event Action ShowScoreTableEvent;
        event Action ChangeParameterEvent;
        event Action<Control> SetFlagEvent;
        event Action<Control> RemoveFlagEvent;
        event Action<Control, int, int> LeftButtonClickEvent;

        string GetParametrName();

        (int rowsCount, int columnsCount, int minesCount) GetSpecialParameters();

        void GameOver(int[,] minesCoordinates);

        void OpenCell(Control control, int value);

        void SetFlag(Control control, bool isCanGetFlag);

        void RemoveFlag(Control control);

        void PrintPlayingField(int rowsCount, int columnsCount, int minesCount);

        void SetParametersBoxs((int rowsCount, int columnsCount, int minesCount) parameters);

        void WinGame(int[,] minesCoordinates);

        void OpenCellsRange(List<int[]> cellsCoordinates, List<int> values);

        void ChangeFlagsCount(int flagsCount);

        void ShowScoreTable(Dictionary<string, int> scoreTable);

        void SetParametersNames(string[] parametersNames);

        string GetPlayerName();
    }
}
