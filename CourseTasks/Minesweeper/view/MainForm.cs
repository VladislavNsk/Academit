using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Minesweeper.View
{
    public partial class MainForm : Form, IView
    {
        public event Action SetParametersEvent;
        public event Action LoadFormEvent;
        public event Action SetSpecialParametersEvent;
        public event Action<Control> SetFlagEvent;
        public event Action<Control> RemoveFlagEvent;
        public event Action ChangeParameterEvent;
        public event Action<Control, int, int> LeftButtonClickEvent;
        public event Action<string> AddNewRecord;
        public event Action<string> SetHighScoreTableEvent;

        private readonly ParametersForm parametersForm;
        private readonly HighScoreTableForm highScoreTableForm;
        private readonly NewRecordForm newRecordForm;

        private readonly Color cellColor = Color.FromArgb(72, 110, 240);

        private enum Colors
        {
            White = -1,
            NoColor,
            Blue,
            Green,
            Red,
            Mediu,
            Purpl,
            Gray,
            Brown,
            Black
        }

        public MainForm(ParametersForm parametersForm, HighScoreTableForm highScoreTableForm, NewRecordForm newRecordForm)
        {
            this.parametersForm = parametersForm;
            this.highScoreTableForm = highScoreTableForm;
            this.newRecordForm = newRecordForm;

            InitializeComponent();
        }

        #region Events

        private void OnSetSpecialParameters()
        {
            SetSpecialParametersEvent?.Invoke();
        }

        private void OnAddNewRecord(string playerName)
        {
            AddNewRecord?.Invoke(playerName);
        }

        private void OnChangeParameter()
        {
            ChangeParameterEvent?.Invoke();
        }

        private void OnSetParameters()
        {
            SetParametersEvent?.Invoke();
        }

        private void OnExitMenuItem(object sender, EventArgs e)
        {
            Close();
        }

        private void OnViewForm(object sender, EventArgs e)
        {
            LoadFormEvent?.Invoke();
        }

        private void OnNewGameMenuItem(object sender, EventArgs e)
        {
            parametersForm.ShowDialog();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            var control = sender as Control;
            playerFieldPanel.Select();

            if (control == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                OnLeftButton(control);
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                if (control.Text != "")
                {
                    return;
                }

                OnRightButton(control);
                return;
            }

            var controlPosition = playerFieldPanel.GetPositionFromControl(control);
            int flagsCount = GetFlagsCount(controlPosition.Row, controlPosition.Column);
            int.TryParse(control.Text, out int cellValue);

            if (flagsCount != cellValue || flagsCount == 0)
            {
                ResetCells(sender, e);
                return;
            }

            OpenCellsRange(controlPosition.Row, controlPosition.Column);
        }

        private void OpenCellsRange(int rowIndex, int columnIndex)
        {
            var currentControl = playerFieldPanel.GetControlFromPosition(columnIndex, rowIndex);

            foreach (var nearestControl in GetNearestControls(rowIndex, columnIndex))
            {
                if (currentControl == nearestControl)
                {
                    continue;
                }

                if (nearestControl.BackgroundImage != flagImage && nearestControl.Enabled == true && nearestControl.Text == "")
                {
                    var controlPosition = playerFieldPanel.GetPositionFromControl(nearestControl);
                    LeftButtonClickEvent?.Invoke(nearestControl, controlPosition.Row, controlPosition.Column);
                }
            }
        }

        private int GetFlagsCount(int rowIndex, int columnIndex)
        {
            int flagsCount = 0;

            foreach (var nearestControl in GetNearestControls(rowIndex, columnIndex))
            {
                if (nearestControl.BackgroundImage == flagImage)
                {
                    flagsCount++;
                }
            }

            return flagsCount;
        }

        private void OnLeftButton(Control currentControl)
        {
            if (currentControl.BackgroundImage != null || currentControl.Text != "")
            {
                return;
            }

            var controlPosition = playerFieldPanel.GetPositionFromControl(currentControl);
            LeftButtonClickEvent?.Invoke(currentControl, controlPosition.Row, controlPosition.Column);

            playerFieldPanel.Select();
        }

        private void OnRightButton(Control currentControl)
        {
            if (currentControl.BackgroundImage == flagImage)
            {
                RemoveFlagEvent?.Invoke(currentControl);
            }
            else
            {
                SetFlagEvent?.Invoke(currentControl);
            }
        }

        public void ChangeFlagsCount(int flagsCount)
        {
            minesLeftCountLabel.Text = flagsCount.ToString();
        }

        #endregion

        #region Parameters

        public (int rowsCount, int columnsCount, int minesCount) GetSpecialParameters()
        {
            return parametersForm.GetSpecialParameters();
        }

        public string GetParameterName()
        {
            return parametersForm.GetParameterName();
        }

        #endregion

        #region OpenCells

        public void OpenCell(Control control, int value)
        {
            if (value == 0 || value == -1)
            {
                control.Text = null;
                control.Enabled = false;
            }
            else
            {
                control.Text = value.ToString();
                control.ForeColor = Color.FromName(Enum.GetName(typeof(Colors), value));
            }

            control.BackColor = Color.White;
            control.Font = cellsFont;
        }

        public void OpenCellsRange(List<int[]> cellsCoordinates, List<int> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                var control = playerFieldPanel.GetControlFromPosition(cellsCoordinates[i][1], cellsCoordinates[i][0]);

                if (control.BackgroundImage == null)
                {
                    OpenCell(control, values[i]);
                }
            }
        }

        #endregion

        #region GameWindow

        public void PrintPlayingField(int rowsCount, int columnsCount, int minesCount)
        {
            if (rowsCount == playerFieldPanel.RowCount && columnsCount == playerFieldPanel.ColumnCount)
            {
                RefreshField();
                return;
            }

            playerFieldPanel.Controls.Clear();
            playerFieldPanel.RowCount = rowsCount;
            playerFieldPanel.ColumnCount = columnsCount;

            for (int i = 0; i < rowsCount; i++)
            {
                playerFieldPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, cellSize.Height));

                for (int j = 0; j < columnsCount; j++)
                {
                    playerFieldPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, cellSize.Width));
                    playerFieldPanel.Controls.Add(new Button()
                    {
                        Dock = DockStyle.Fill,
                        BackColor = cellColor,
                        Margin = new Padding(0),
                        FlatStyle = FlatStyle.Flat
                    });
                };
            };

            SubscribeControlsToEvent();
            SetAdditionallyItemsParameters(minesCount);
            playerFieldPanel.Select();

            Size = new Size(playerFieldPanel.Size.Width + 20, playerFieldPanel.Size.Height + 10);
        }

        private void SetAdditionallyItemsParameters(int minesCount)
        {
            minesLeftCountLabel.Text = minesCount.ToString();
            minesLeftCountLabel.Location = new Point(playerFieldPanel.Right - 70, playerFieldPanel.Bottom + 20);

            minePicture.Location = new Point(minesLeftCountLabel.Location.X - 50, minesLeftCountLabel.Location.Y);
            timePicture.Location = new Point(playerFieldPanel.Left, playerFieldPanel.Bottom + 20);

            gameTime.Location = new Point(timePicture.Location.X + 60, playerFieldPanel.Bottom + 20);
            gameTime.Text = "0";
        }

        public void RefreshField()
        {
            playerFieldPanel.Enabled = true;

            foreach (Control control in playerFieldPanel.Controls)
            {
                control.Enabled = true;
                control.BackColor = cellColor;
                control.BackgroundImage = null;
                control.Text = null;
                control.ForeColor = default;
            }

            playerFieldPanel.Select();
        }

        private void ShowMines(int[,] minesCoordinates)
        {
            for (var i = 0; i < minesCoordinates.GetLength(0); i++)
            {
                for (var j = 0; j < minesCoordinates.GetLength(1); j++)
                {
                    if (minesCoordinates[i, j] == -1)
                    {
                        playerFieldPanel.GetControlFromPosition(j, i).BackgroundImage = mineImage;
                    }
                }
            }
        }

        #endregion

        public void WinGame(int[,] minesCoordinates)
        {
            ShowMines(minesCoordinates);
            MessageBox.Show("Вы победили", "Победа", MessageBoxButtons.OK);
            playerFieldPanel.Enabled = false;
        }

        public void GameOver(int[,] minesCoordinates)
        {
            ShowMines(minesCoordinates);
            MessageBox.Show("Вы проиграли", "Поражение", MessageBoxButtons.OK);
            playerFieldPanel.Enabled = false;
        }

        private void SubscribeControlsToEvent()
        {
            playerFieldPanel.Enabled = true;

            foreach (Control c in playerFieldPanel.Controls)
            {
                c.MouseUp += OnMouseUp;
                c.MouseDown += OnMouseDown;
            }
        }

        private void ResetCells(object sender, EventArgs e)
        {
            var control = sender as Control;

            if (control == null)
            {
                return;
            }

            var controlPosition = playerFieldPanel.GetPositionFromControl(control);
            int rowIndex = controlPosition.Row;
            int columnIndex = controlPosition.Column;

            foreach (var nearestControl in GetNearestControls(rowIndex, columnIndex))
            {
                if (nearestControl.Enabled == true && nearestControl.Text == "")
                {
                    nearestControl.BackColor = cellColor;
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                SelectCellsRange(sender as Control);
            }
        }

        private void SelectCellsRange(Control control)
        {
            if (control == null)
            {
                return;
            }

            var controlPosition = playerFieldPanel.GetPositionFromControl(control);
            int rowIndex = controlPosition.Row;
            int columnIndex = controlPosition.Column;

            foreach (var nearestControl in GetNearestControls(rowIndex, columnIndex))
            {
                if (nearestControl.BackgroundImage != flagImage)
                {
                    nearestControl.BackColor = Color.White;
                }
            }
        }

        private IEnumerable<Control> GetNearestControls(int rowIndex, int columnIndex)
        {
            int rowsCount = playerFieldPanel.RowCount;
            int columnsCount = playerFieldPanel.ColumnCount;

            for (int i = rowIndex - 1; i <= rowIndex + 1; i++)
            {
                if (i < 0 || rowsCount <= i)
                {
                    continue;
                }

                for (int j = columnIndex - 1; j <= columnIndex + 1; j++)
                {
                    if (j < 0 || columnsCount <= j)
                    {
                        continue;
                    }

                    yield return playerFieldPanel.GetControlFromPosition(j, i);
                }
            }
        }

        private void OnHighScoreTable(object sender, EventArgs e)
        {
            highScoreTableForm.ShowDialog();
        }

        public void RemoveFlag(Control control)
        {
            control.BackgroundImage = null;
            playerFieldPanel.Select();
        }

        public void SetFlag(Control control, bool isCanGetFlag)
        {
            if (isCanGetFlag)
            {
                control.BackgroundImage = flagImage;
            }

            playerFieldPanel.Select();
        }

        public void SetParametersNames(string[] parametersNames)
        {
            parametersForm.SetParametersNames(parametersNames);
            highScoreTableForm.SetParametersNames(parametersNames);
        }

        public void ShowAddNewRecordDialog()
        {
            newRecordForm.ShowDialog();
        }

        public void SetParametersBoxs((int rowsCount, int columnsCount, int minesCount) parameters)
        {
            parametersForm.SetParametersBoxs(parameters.rowsCount, parameters.columnsCount, parameters.minesCount);
        }

        public void SetGameTime(int secondsCount)
        {
            gameTime.Text = secondsCount.ToString();
        }

        private void OnFillHighScoreTable(string parameterName)
        {
            SetHighScoreTableEvent?.Invoke(parameterName);
        }

        public void SetHighScoreTable(Dictionary<string, int> scoreTable)
        {
            highScoreTableForm.SetValues(scoreTable);
        }
    }
}
