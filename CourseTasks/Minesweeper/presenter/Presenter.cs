using Minesweeper.Modul;
using Minesweeper.View;

using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesweeper.presenter
{
    public class Presenter
    {
        private readonly PlayingField field;
        private readonly IView view;

        public Presenter(IView view, PlayingField field)
        {
            this.view = view;
            this.field = field;

            view.SetSpecialParametrs += OnSetSpecialParametrs;
            view.ShowScoreTableEvent += OnShowScoreTable;
            view.LeftButtonClick += OnLeftButtonClick;
            view.RemoveFlagEvent += OnRemoveFlag;
            view.SetParametrs += OnSetParametrs;
            view.LoadFormEvent += OnLoadForm;
            view.SetFlagEvent += OnSetFlag;

            field.ChangeFlagsCountAction += OnChangeFlagsCount;
            field.OpenCellsRangeEvent += OnOpenCellsRange;
            field.ChangeParameters += OnStartGame;
            field.GameOver += OnGameOver;
            field.Win += OnWin;
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

        #region Parametrs

        private void OnSetSpecialParametrs()
        {
            var (rowsCount, columnsCount, minesCount) = view.GetSpecialParametrs();
            field.SetParametrs(rowsCount, columnsCount, minesCount);
            field.AddPlayerName(view.GetPlayerName());
        }

        private void OnSetParametrs()
        {
            field.SetParametrs(view.GetParametrName());
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
            view.SetParametrsNames(field.GetNamesParametrs());
            view.PrintPlayingField(field.GetRowsCount(), field.GetColumnsCount(), field.GetMinesCount());
            field.AddPlayerName(view.GetPlayerName());
        }
    }
}
