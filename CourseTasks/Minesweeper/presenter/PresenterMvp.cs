using Minesweeper.Modul;
using Minesweeper.View;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Minesweeper.Presenter
{
    public class PresenterMvp
    {
        private readonly PlayingField field = new PlayingField();
        private readonly IView view;

        public PresenterMvp(IView view)
        {
            this.view = view;

            view.SetParametrs += View_SetParametrs;
            view.LeftButtonClick += View_LeftButtonClick;
            view.LoadFormEvent += View_LoadFormEvent;
            view.ShowScoreTableEvent += View_ShowScoreTableEvent;
            view.SetSpecialParametrs += View_SetSpecialParametrs;
            view.SetFlagEvent += View_SetFlagEvent;
            view.RemoveFlagEvent += View_RemoveFlagEvent;

            field.ChangeParameters += Model_StartGame;
            field.GameOver += GameOver;
            field.Win += Model_Win;
            field.OpenCellsRangeEvent += Model_OpenCellsRangeEvent;
            field.ChangeFlagsCountAction += Model_ChangeFlagsCount;
        }

        private void View_RemoveFlagEvent(Control control)
        {
            view.RemoveFlag(control);
            field.RemoveFlag();
        }

        private void View_SetFlagEvent(Control control)
        {
            view.SetFlag(control, field.IsCanGetFlag());
            field.SetFlag();
        }

        #region Parametrs

        private void View_SetSpecialParametrs()
        {
            var (rowsCount, columnsCount, minesCount) = view.GetSpecialParametrs();
            field.SetParametrs(rowsCount, columnsCount, minesCount);
            field.AddPlayerName(view.GetPlayerName());
        }

        private void View_SetParametrs()
        {
            field.SetParametrs(view.GetParametrName());
            field.AddPlayerName(view.GetPlayerName());
        }

        #endregion

        private void Model_OpenCellsRangeEvent(List<int[]> cellsCoordinates, List<int> values)
        {
            view.OpenCellsRange(cellsCoordinates, values);
        }

        private void View_LeftButtonClick(Control control, int rowIndex, int columnIndex)
        {
            view.OpenCell(control, field.GetCellValue(rowIndex, columnIndex));
        }

        #region Flag

        private void Model_ChangeFlagsCount(int flagsCount)
        {
            view.ChangeFlagsCount(flagsCount);
        }

        #endregion

        private void Model_Win(int[,] minesCoordinates)
        {
            view.WinGame(minesCoordinates);
        }

        private void GameOver(int[,] minesCoordinatesx)
        {
            view.GameOver(minesCoordinatesx);
        }

        private void Model_StartGame()
        {
            view.PrintPlayingField(field.GetRowsCount(), field.GetColumnsCount(), field.GetMinesCount());
        }

        private void View_ShowScoreTableEvent(object sender, EventArgs e)
        {
            view.ShowScoreTable(field.GetScoreTable());
        }

        private void View_LoadFormEvent(object sender, EventArgs e)
        {
            view.SetParametrsNames(field.GetNamesParametrs());
            view.PrintPlayingField(field.GetRowsCount(), field.GetColumnsCount(), field.GetMinesCount());
            field.AddPlayerName(view.GetPlayerName());
        }
    }
}
