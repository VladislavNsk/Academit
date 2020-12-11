using System.Windows.Forms;
using System.Drawing;
using System;

namespace Minesweeper
{
    partial class ViewForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menu = new MenuStrip();
            gameMenuItem = new ToolStripMenuItem();
            exitMenuItem = new ToolStripMenuItem();
            higeScoreTable = new ToolStripMenuItem();
            newGameMenuItem = new ToolStripMenuItem();
            about = new ToolStripMenuItem();

            scoreTablePanel = new TableLayoutPanel();
            playerFieldPanel = new TableLayoutPanel();

            panel = new Panel();
            parametrsPanel = new Panel();

            rowsCountBox = new NumericUpDown();
            minesCountBox = new NumericUpDown();
            columnsCountBox = new NumericUpDown();

            applyButton = new Button();
            applyScoreButton = new Button();

            labelName = new Label();
            minesLeftCountLabel = new Label();

            nameBox = new TextBox();
            parametrsNamesComboBox = new ComboBox();

            cellSize = new Size(25, 25);
            minePicture = new PictureBox();
            flagImage = new Bitmap(Resource1.flag, cellSize);
            mineImage = new Bitmap(Resource1.mine, cellSize);

            SuspendLayout();

            string aboutText = "Цель игры Сапер – вскрыть пустые ячейки, не вскрыв при этом ни одной, содержащей мину." +
                "Игра начинается с первого клика по любой ячейке на поле. При клике на ячейке, она открывается." +
                " Если в ней находится мина, Вы проиграли.";
            Size parametrsBoxsSize = new Size(50, 17);

            rowsCountBox.Enabled = false;
            rowsCountBox.Value = 9;
            rowsCountBox.Minimum = 9;
            rowsCountBox.Maximum = 24;
            rowsCountBox.Size = parametrsBoxsSize;
            rowsCountBox.Location = new Point(panel.Right - 40, panel.Top + 20);

            columnsCountBox.Enabled = false;
            columnsCountBox.Value = 9;
            columnsCountBox.Minimum = 9;
            columnsCountBox.Maximum = 30;
            columnsCountBox.Size = parametrsBoxsSize;
            columnsCountBox.Location = new Point(panel.Right - 40, panel.Top + 50);

            minesCountBox.Value = 10;
            minesCountBox.Minimum = 10;
            minesCountBox.Maximum = rowsCountBox.Maximum * columnsCountBox.Maximum - 1;
            minesCountBox.Size = parametrsBoxsSize;
            minesCountBox.Location = new Point(panel.Right - 40, panel.Top + 80);
            minesCountBox.Enabled = false;

            parametrsNamesComboBox.Visible = true;
            parametrsNamesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            parametrsNamesComboBox.Location = new Point(panel.Location.X + 10, columnsCountBox.Location.Y);
            parametrsNamesComboBox.SelectionChangeCommitted += SelectionChangeCommitted;

            applyScoreButton.Text = "Ок";
            applyScoreButton.Size = new Size(84, 25);
            applyScoreButton.Click += ((control, e) => scoreTablePanel.Visible = false);

            gameMenuItem.Text = "Игра";
            gameMenuItem.DropDownItems.Add(newGameMenuItem);
            gameMenuItem.DropDownItems.Add(exitMenuItem);

            exitMenuItem.Text = "Выход";
            exitMenuItem.Click += ExitMenuItem_Click;

            newGameMenuItem.Text = "Новая игра";
            newGameMenuItem.Click += NewGameMenuItem_Click;

            higeScoreTable.Text = "Рекорды";
            higeScoreTable.Click += HigeScoreTable_Click;

            about.Text = "О программе";
            about.Click += ((control, e) =>
            {
                MessageBox.Show(aboutText);
            });

            menu.Items.Add(gameMenuItem);
            menu.Items.Add(higeScoreTable);
            menu.Items.Add(about);
            menu.BackColor = SystemColors.ActiveCaption;

            parametrsPanel.Size = new Size(250, 250);
            parametrsPanel.BackColor = SystemColors.ControlLight;
            parametrsPanel.BorderStyle = BorderStyle.Fixed3D;
            parametrsPanel.Anchor = AnchorStyles.None;
            parametrsPanel.Visible = false;
            parametrsPanel.Controls.Add(applyButton);
            parametrsPanel.Controls.Add(nameBox);
            parametrsPanel.Controls.Add(rowsCountBox);
            parametrsPanel.Controls.Add(minesCountBox);
            parametrsPanel.Controls.Add(parametrsNamesComboBox);
            parametrsPanel.Controls.Add(columnsCountBox);
            parametrsPanel.Controls.Add(labelName);

            applyButton.Text = "Принять";
            applyButton.Size = new Size(84, 25);
            applyButton.Click += ApplyButton_Click;
            applyButton.Location = new Point(parametrsPanel.Width / 2 - 42, parametrsPanel.Bottom - 50);
            applyButton.Anchor = AnchorStyles.None;
            applyButton.BackColor = SystemColors.Window;

            nameBox.Size = new Size(84, 25);
            nameBox.Location = new Point(applyButton.Location.X, applyButton.Location.Y - 50);
            nameBox.MaxLength = 15;

            labelName.Text = "Введите ник";
            labelName.Location = new Point(nameBox.Location.X, nameBox.Location.Y - 20);

            scoreTablePanel.BorderStyle = BorderStyle.FixedSingle;
            scoreTablePanel.Size = new Size(225, 225);
            scoreTablePanel.Visible = false;
            scoreTablePanel.AutoScroll = true;
            scoreTablePanel.BackColor = SystemColors.ControlLight; ;
            scoreTablePanel.Anchor = AnchorStyles.None;

            minesLeftCountLabel.Size = new Size(70, 40);
            minesLeftCountLabel.BackColor = Color.White;
            minesLeftCountLabel.BorderStyle = BorderStyle.FixedSingle;
            minesLeftCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            minesLeftCountLabel.Font = new Font(Font.FontFamily.Name, 30, FontStyle.Regular);
            minesLeftCountLabel.Enabled = false;
            minesLeftCountLabel.Anchor = AnchorStyles.None;

            minePicture.Image = new Bitmap(Resource1.mine, new Size(40, 40));
            minePicture.Anchor = AnchorStyles.None;

            panel.Controls.Add(parametrsPanel);
            panel.Controls.Add(playerFieldPanel);
            panel.Controls.Add(minesLeftCountLabel);
            panel.Controls.Add(minePicture);
            panel.AutoSize = true;

            playerFieldPanel.AutoSize = true;

            DoubleBuffered = true;
            AutoScroll = true;
            Load += ViewForm_Load;
            Controls.Add(menu);
            Controls.Add(panel);
            Controls.Add(scoreTablePanel);
            BackColor = SystemColors.ActiveCaption;
            Text = "Сапер";
            AcceptButton = applyButton;
            MinimumSize = new Size(400, 450);
            AutoSize = true;

            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel playerFieldPanel;
        private TableLayoutPanel scoreTablePanel;

        private MenuStrip menu;
        private ToolStripMenuItem higeScoreTable;
        private ToolStripMenuItem newGameMenuItem;
        private ToolStripMenuItem gameMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem about;

        private NumericUpDown rowsCountBox;
        private NumericUpDown minesCountBox;
        private NumericUpDown columnsCountBox;

        private Panel panel;
        private Panel parametrsPanel;

        private Button applyButton;
        private Button applyScoreButton;

        private Label labelName;
        private Label minesLeftCountLabel;

        private TextBox nameBox;
        private PictureBox minePicture;
        private ComboBox parametrsNamesComboBox;

        private Bitmap mineImage;
        private Bitmap flagImage;
        private Size cellSize;
    }
}

