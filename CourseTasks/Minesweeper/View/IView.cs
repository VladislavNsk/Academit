using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Minesweeper.View
{
    public interface IView
    {
        event Action SetParametersEvent;

        event Action LoadFormEvent;
        event Action ChangeParameterEvent;
        event Action SetSpecialParametersEvent;
        event Action<string> AddNewRecord;
        event Action<string> SetHighScoreTableEvent;
        event Action<Control> SetFlagEvent;
        event Action<Control> RemoveFlagEvent;
        event Action<Control, int, int> LeftButtonClickEvent;

        string GetParameterName();

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

        void SetHighScoreTable(Dictionary<string, int> scoreTable);

        void SetParametersNames(string[] parametersNames);

        void ShowAddNewRecordDialog();

        void SetGameTime(int secondsCount);
    }
}
