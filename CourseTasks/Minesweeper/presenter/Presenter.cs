using Minesweeper.Model;
using Minesweeper.View;

using System.Windows.Forms;
using System.Collections.Generic;

namespace Minesweeper.Presenter
{
    public class Presenter
    {
        private readonly PlayingField field;
        private readonly IView view;

        public Presenter(IView view, PlayingField field)
        {
            this.view = view;
            this.field = field;

            view.SetSpecialParametersEvent += OnSetSpecialParameters;
            view.ChangeParameterEvent += OnChangeParameterEvent;
            view.LeftButtonClickEvent += OnLeftButtonClick;
            view.SetHighScoreTableEvent += OnShowScoreTable;
            view.SetParametersEvent += OnSetParameters;
            view.RemoveFlagEvent += OnRemoveFlag;
            view.LoadFormEvent += OnLoadForm;
            view.SetFlagEvent += OnSetFlag;
            view.AddNewRecord += OnAddNewRecord;

            field.ChangeFlagsCountEvent += OnChangeFlagsCount;
            field.ChangeTimerValueEvent += OnChangeTimerValue;
            field.OpenCellsRangeEvent += OnOpenCellsRange;
            field.ChangeParametersEvent += OnStartGame;
            field.GameOverEvent += OnGameOver;
            field.WinEvent += OnWin;
            field.GetParameterName += OnGetParameterName;
            field.AddNewRecordEvent += OnAddNewRecordEvent;
        }

        private void OnAddNewRecord(string playerName)
        {
            field.AddNewRecord(playerName);
        }

        private void OnAddNewRecordEvent()
        {
            view.ShowAddNewRecordDialog();
        }

        private string OnGetParameterName()
        {
            return view.GetParameterName();
        }

        private void OnChangeTimerValue(int secondsCount)
        {
            view.SetGameTime(secondsCount);
        }

        private void OnChangeParameterEvent()
        {
            view.SetParametersBoxs(field.GetParameters(view.GetParameterName()));
        }

        private void OnRemoveFlag(Control control)
        {
            view.RemoveFlag(control);
            field.RemoveFlag();
        }

        private void OnSetFlag(Control control)
        {
            view.SetFlag(control, field.IsCanGetFlag());
            field.SetFlag();
        }

        #region Parameters

        private void OnSetSpecialParameters()
        {
            var (rowsCount, columnsCount, minesCount) = view.GetSpecialParameters();
            field.SetParameters(rowsCount, columnsCount, minesCount);
        }

        private void OnSetParameters()
        {
            field.SetParameters(view.GetParameterName());
        }

        #endregion

        private void OnOpenCellsRange(List<int[]> cellsCoordinates, List<int> cellsValues)
        {
            view.OpenCellsRange(cellsCoordinates, cellsValues);
        }

        private void OnLeftButtonClick(Control control, int rowIndex, int columnIndex)
        {
            view.OpenCell(control, field.GetCellValue(rowIndex, columnIndex));
        }

        #region Flag

        private void OnChangeFlagsCount(int flagsCount)
        {
            view.ChangeFlagsCount(flagsCount);
        }

        #endregion

        private void OnWin(int[,] minesCoordinates)
        {
            view.WinGame(minesCoordinates);
        }

        private void OnGameOver(int[,] minesCoordinates)
        {
            view.GameOver(minesCoordinates);
        }

        private void OnStartGame()
        {
            view.PrintPlayingField(field.GetRowsCount(), field.GetColumnsCount(), field.GetMinesCount());
        }

        private void OnShowScoreTable(string parameterName)
        {
            view.SetHighScoreTable(field.GetScoreTable(parameterName));
        }

        private void OnLoadForm()
        {
            view.SetParametersNames(field.GetNamesParameters());
            view.PrintPlayingField(field.GetRowsCount(), field.GetColumnsCount(), field.GetMinesCount());
        }
    }
}
