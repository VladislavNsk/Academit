using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper.View
{
    public partial class MainForm : Form, IView
    {
        public event Action SetParametrs;
        public event Action LoadFormEvent;
        public event Action SetSpecialParametrs;
        public event Action ShowScoreTableEvent;
        public event Action<Control> SetFlagEvent;
        public event Action<Control> RemoveFlagEvent;
        public event Action<Control, int, int> LeftButtonClick;

        private readonly ParametrsForm parametrsForm;
        private readonly HighScoreTableForm highScoreTableForm;

        public MainForm(ParametrsForm parametrsForm, HighScoreTableForm highScoreTableForm)
        {
            this.parametrsForm = parametrsForm;
            this.highScoreTableForm = highScoreTableForm;

            InitializeComponent();
        }

        #region Events

        private void OnSetSpecialParametrs()
        {
            SetSpecialParametrs?.Invoke();
        }

        private void OnSetParametrs()
        {
            SetParametrs?.Invoke();
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
            parametrsForm.ShowDialog();
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnLeftButton(sender as Control);
                return;
            }

            OnRightButton(sender as Control);
        }

        private void OnLeftButton(Control currentControl)
        {
            if (currentControl.BackgroundImage != null)
            {
                return;
            }

            var controlPosition = playerFieldPanel.GetPositionFromControl(currentControl);
            LeftButtonClick?.Invoke(currentControl, controlPosition.Row, controlPosition.Column);

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

        #region Parametrs

        public (int rowsCount, int columnsCount, int minesCount) GetSpecialParametrs()
        {
            return parametrsForm.GetSpecialParametrs();
        }

        public string GetParametrName()
        {
            return parametrsForm.GetParametrName();
        }

        #endregion

        #region OpenCells

        public void OpenCell(Control control, int value)
        {
            if (value == 0)
            {
                control.Text = null;
            }
            else
            {
                control.Text = value.ToString();
            }

            control.BackColor = Color.White;
            control.Font = cellsFont;
            control.Enabled = false;
        }

        public void OpenCellsRange(List<int[]> cellsCoordinates, List<int> values)
        {
            int i = 0;

            foreach (var cellCoordinates in cellsCoordinates)
            {
                var control = playerFieldPanel.GetControlFromPosition(cellCoordinates[1], cellCoordinates[0]);

                if (control.BackgroundImage == null)
                {
                    if (values[i] != 0)
                    {
                        control.Text = values[i].ToString();
                    }

                    control.BackColor = Color.White;
                    control.Enabled = false;
                    control.Font = cellsFont;
                }

                i++;
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
                        BackColor = Color.FromArgb(72, 110, 240),
                        Margin = new Padding(0),
                        FlatStyle = FlatStyle.Flat,
                    });
                };
            };

            SubscribeControlsToEvent();
            SetAdditionallyItemsParametrs(minesCount);

            Size = new Size(playerFieldPanel.Size.Width + 20, playerFieldPanel.Size.Height + 10);
        }

        private void SetAdditionallyItemsParametrs(int minesCount)
        {
            minesLeftCountLabel.Text = minesCount.ToString();
            minesLeftCountLabel.Location = new Point(playerFieldPanel.Right - 70, playerFieldPanel.Bottom + 20);
            minePicture.Location = new Point(minesLeftCountLabel.Location.X - 50, minesLeftCountLabel.Location.Y + 7);
        }

        public void RefreshField()
        {
            playerFieldPanel.Enabled = true;

            foreach (Control control in playerFieldPanel.Controls)
            {
                control.Enabled = true;
                control.BackColor = Color.FromArgb(72, 110, 240);
                control.BackgroundImage = null;
                control.Text = null;
            }
        }

        private void ShowMines(int[,] minesCoordinates)
        {
            for (int i = 0; i < minesCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < minesCoordinates.GetLength(1); j++)
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
            MessageBox.Show("Победа");
            playerFieldPanel.Enabled = false;
        }

        public void GameOver(int[,] minesCoordinates)
        {
            MessageBox.Show("Провал");
            ShowMines(minesCoordinates);
            playerFieldPanel.Enabled = false;
        }

        private void SubscribeControlsToEvent()
        {
            playerFieldPanel.Enabled = true;

            foreach (Control c in playerFieldPanel.Controls)
            {
                c.MouseUp += Control_MouseUp;
            }
        }

        private void OnHigeScoreTable(object sender, EventArgs e)
        {
            ShowScoreTableEvent?.Invoke();
            highScoreTableForm.ShowDialog();
        }

        public void ShowScoreTable(Dictionary<string, int> scoreTable)
        {
            highScoreTableForm.SetValues(scoreTable);
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

        public void SetParametrsNames(string[] parametrsNames)
        {
            parametrsForm.SetParametrsNames(parametrsNames);
        }

        public string GetPlayerName()
        {
            return parametrsForm.GetPlayerName();
        }
    }
}
