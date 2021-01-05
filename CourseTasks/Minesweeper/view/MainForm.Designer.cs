using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Minesweeper.View
{
    partial class MainForm
    {
        private IContainer components = null;

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

            playerFieldPanel = new TableLayoutPanel();
            button = new Button();
            cellSize = new Size(35, 35);
            flagImage = new Bitmap(Resource1.flag, cellSize);
            mineImage = new Bitmap(Resource1.mine, cellSize);
            cellsFont = new Font("Nimes New Roman", 13);
            minesLeftCountLabel = new Label();
            gameTime = new Label();
            minePicture = new PictureBox();
            timePicture = new PictureBox();

            SuspendLayout();

            string aboutText = "Цель игры Сапер – вскрыть пустые ячейки, не вскрыв при этом ни одной, содержащей мину." +
                "Игра начинается с первого клика по любой ячейке на поле. При клике на ячейке, она открывается." +
                " Если в ней находится мина, Вы проиграли.";

            gameMenuItem.Text = "Игра";
            gameMenuItem.DropDownItems.Add(newGameMenuItem);
            gameMenuItem.DropDownItems.Add(exitMenuItem);

            exitMenuItem.Text = "Выход";
            exitMenuItem.Click += OnExitMenuItem;

            newGameMenuItem.Text = "Новая игра";
            newGameMenuItem.Click += OnNewGameMenuItem;

            higeScoreTable.Text = "Рекорды";
            higeScoreTable.Click += OnHighScoreTable;

            about.Text = "О программе";
            about.Click += ((control, e) =>
            {
                MessageBox.Show(aboutText, about.Text);
            });

            newRecordForm.AddNewRecord += OnAddNewRecord;

            menu.Items.Add(gameMenuItem);
            menu.Items.Add(higeScoreTable);
            menu.Items.Add(about);
            menu.BackColor = SystemColors.ActiveCaption;

            minesLeftCountLabel.Size = new Size(70, 40);
            minesLeftCountLabel.BackColor = Color.White;
            minesLeftCountLabel.BorderStyle = BorderStyle.FixedSingle;
            minesLeftCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            minesLeftCountLabel.Font = new Font(Font.FontFamily.Name, 30, FontStyle.Regular);
            minesLeftCountLabel.Enabled = false;

            gameTime.Size = new Size(85, 40);
            gameTime.BackColor = Color.White;
            gameTime.BorderStyle = BorderStyle.FixedSingle;
            gameTime.TextAlign = ContentAlignment.MiddleCenter;
            gameTime.Font = new Font(Font.FontFamily.Name, 30, FontStyle.Regular);
            gameTime.Enabled = false;

            minePicture.Image = new Bitmap(Resource1.mine, new Size(40, 45));
            minePicture.Size = new Size(40, 45);

            timePicture.Image = new Bitmap(Resource1.time, new Size(40, 40));
            timePicture.Size = new Size(40, 40);

            playerFieldPanel.AutoSize = true;
            playerFieldPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            playerFieldPanel.Location = new Point(0, 40);

            parametersForm.SetParameters += OnSetParameters;
            parametersForm.ChangeParameter += OnChangeParameter; ;
            parametersForm.SetSpecialParameters += OnSetSpecialParameters;

            highScoreTableForm.FillHighScoreTable += OnFillHighScoreTable;

            button.Dock = DockStyle.Fill;
            button.BackColor = cellColor;
            button.Margin = new Padding(0);
            button.FlatStyle = FlatStyle.Flat;

            DoubleBuffered = true;
            Load += OnViewForm;
            Controls.Add(menu);
            Controls.Add(playerFieldPanel);
            Controls.Add(minesLeftCountLabel);
            Controls.Add(minePicture);
            Controls.Add(timePicture);
            Controls.Add(gameTime);
            BackColor = SystemColors.ActiveCaption;
            Text = "Сапер";
            AutoSize = true;
            Icon = Resource1.mineIcon;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel playerFieldPanel;

        private MenuStrip menu;
        private ToolStripMenuItem higeScoreTable;
        private ToolStripMenuItem newGameMenuItem;
        private ToolStripMenuItem gameMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem about;

        private Label minesLeftCountLabel;
        private Label gameTime;
        private Button button;
        private PictureBox minePicture;
        private PictureBox timePicture;
        private Bitmap mineImage;
        private Bitmap flagImage;
        private Size cellSize;
        private Font cellsFont;
    }
}

