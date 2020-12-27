using Minesweeper.Model;
using Minesweeper.View;

using System.Collections.Generic;
using System.Windows.Forms;

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
            view.ShowScoreTableEvent += OnShowScoreTable;
            view.LeftButtonClickEvent += OnLeftButtonClick;
            view.RemoveFlagEvent += OnRemoveFlag;
            view.SetParametersEvent += OnSetParameters;
            view.LoadFormEvent += OnLoadForm;
            view.SetFlagEvent += OnSetFlag;
            view.ChangeParameterEvent += OnChangeParameterEvent;

            field.ChangeFlagsCountAction += OnChangeFlagsCount;
            field.OpenCellsRangeEvent += OnOpenCellsRange;
            field.ChangeParameters += OnStartGame;
            field.GameOver += OnGameOver;
            field.Win += OnWin;
        }

        private void OnChangeParameterEvent()
        {
            view.SetParametersBoxs(field.GetParameters(view.GetParametrName()));
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
            field.AddPlayerName(view.GetPlayerName());
        }

        private void OnSetParameters()
        {
            field.SetParameters(view.GetParametrName());
            field.AddPlayerName(view.GetPlayerName());
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

        private void OnShowScoreTable()
        {
            view.ShowScoreTable(field.GetScoreTable());
        }

        private void OnLoadForm()
        {
            view.SetParametersNames(field.GetNamesParameters());
            view.PrintPlayingField(field.GetRowsCount(), field.GetColumnsCount(), field.GetMinesCount());
            field.AddPlayerName(view.GetPlayerName());
        }
    }
}
