using Minesweeper.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class ViewForm : Form, IView
    {
        public event EventHandler LoadFormEvent;
        public event EventHandler ShowScoreTableEvent;

        public event Action SetParametrs;
        public event Action SetSpecialParametrs;
        public event Action<Control, int, int> LeftButtonClick;
        public event Action<Control> SetFlagEvent;
        public event Action<Control> RemoveFlagEvent;

        private readonly Font cellsFont = new Font("Nimes New Roman", 13);
        private readonly string parametrSpecialName = "Свой";

        public ViewForm()
        {
            InitializeComponent();
        }

        #region Events

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (parametrsNamesComboBox.Text == parametrSpecialName)
            {
                SetSpecialParametrs?.Invoke();
            }
            else
            {
                SetParametrs?.Invoke();
            }

            parametrsPanel.Visible = false;
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            LoadFormEvent?.Invoke(sender, e);
        }

        private void NewGameMenuItem_Click(object sender, EventArgs e)
        {
            parametrsPanel.Visible = true;
            parametrsPanel.Select();
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LeftButton_Click(sender as Control);
                return;
            }

            RightButton_Click(sender as Control);
        }

        private void LeftButton_Click(Control currentControl)
        {
            if (currentControl.BackgroundImage != null)
            {
                return;
            }

            var controlPosition = playerFieldPanel.GetPositionFromControl(currentControl);
            LeftButtonClick?.Invoke(currentControl, controlPosition.Row, controlPosition.Column);

            playerFieldPanel.Select();
        }

        private void RightButton_Click(Control currentControl)
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

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            var parametrName = sender as ComboBox;

            if (parametrName.Text == parametrSpecialName)
            {
                rowsCountBox.Enabled = true;
                columnsCountBox.Enabled = true;
                minesCountBox.Enabled = true;
            }
            else
            {
                rowsCountBox.Enabled = false;
                columnsCountBox.Enabled = false;
                minesCountBox.Enabled = false;
            }
        }

        #endregion

        #region Parametrs

        public (int rowsCount, int columnsCount, int minesCount) GetSpecialParametrs()
        {
            return ((int)rowsCountBox.Value, (int)columnsCountBox.Value, (int)minesCountBox.Value);
        }

        public string GetParametrName()
        {
            return parametrsNamesComboBox.Text;
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
                        Margin = new Padding(),
                        FlatStyle = FlatStyle.Flat,
                    });
                };
            };

            playerFieldPanel.Anchor = AnchorStyles.None;
            panel.Dock = DockStyle.Fill;

            SubscribeControlsToEvent();
            SetAdditionallyItemsParametrs(minesCount);
        }

        private void SetAdditionallyItemsParametrs(int minesCount)
        {
            minesLeftCountLabel.Text = minesCount.ToString();
            minesLeftCountLabel.Location = new Point(playerFieldPanel.Right - 70, playerFieldPanel.Bottom + 20);
            minePicture.Location = new Point(minesLeftCountLabel.Location.X - 50, minesLeftCountLabel.Location.Y + 7);
            parametrsPanel.Location = new Point(playerFieldPanel.Location.X, playerFieldPanel.Location.Y);
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
            parametrsPanel.Visible = false;
            playerFieldPanel.Enabled = true;

            foreach (Control c in playerFieldPanel.Controls)
            {
                c.MouseUp += Control_MouseUp;
            }
        }

        private void HigeScoreTable_Click(object sender, EventArgs e)
        {
            ShowScoreTableEvent?.Invoke(sender, e);
        }

        public void ShowScoreTable(Dictionary<string, int> scoreTable)
        {
            if (scoreTablePanel.Visible == true)
            {
                return;
            }

            scoreTablePanel.Location = new Point(playerFieldPanel.Location.X, playerFieldPanel.Location.Y);
            int defoultRowsCount = 4;
            int maxRowsCount = Math.Max(defoultRowsCount, scoreTable.Count) + 1;

            scoreTablePanel.Controls.Clear();
            scoreTablePanel.RowCount = maxRowsCount;
            scoreTablePanel.ColumnCount = 3;

            for (int i = 0; i < maxRowsCount; i++)
            {
                scoreTablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 41));

                for (int j = 0; j < 2; j++)
                {
                    scoreTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                }
            }

            int k = 0;

            foreach (var pair in scoreTable)
            {
                scoreTablePanel.Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(),
                    Anchor = AnchorStyles.None,
                    AutoSize = true,
                    Text = pair.Key,
                    TextAlign = ContentAlignment.MiddleCenter
                }, 0, k);

                scoreTablePanel.Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(),
                    Anchor = AnchorStyles.None,
                    Text = pair.Value.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                }, 2, k);

                k++;
            }

            scoreTablePanel.Controls.Add(applyScoreButton, 1, maxRowsCount - 1);
            scoreTablePanel.BringToFront();
            scoreTablePanel.Visible = true;
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
            parametrsNamesComboBox.Items.AddRange(parametrsNames);
            parametrsNamesComboBox.Items.Add(parametrSpecialName);
            parametrsNamesComboBox.SelectedIndex = 0;
        }

        public string GetPlayerName()
        {
            return nameBox.Text;
        }
    }
}
