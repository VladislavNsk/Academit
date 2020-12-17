using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper.View
{
    partial class MainForm
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

            playerFieldPanel = new TableLayoutPanel();
            cellSize = new Size(25, 25);
            flagImage = new Bitmap(Resource1.flag, cellSize);
            mineImage = new Bitmap(Resource1.mine, cellSize);
            cellsFont = new Font("Nimes New Roman", 13);
            minesLeftCountLabel = new Label();
            minePicture = new PictureBox();

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
            higeScoreTable.Click += OnHigeScoreTable;

            about.Text = "О программе";
            about.Click += ((control, e) =>
            {
                MessageBox.Show(aboutText);
            });

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

            minePicture.Image = new Bitmap(Resource1.mine, new Size(40, 40));

            playerFieldPanel.AutoSize = true;
            playerFieldPanel.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            playerFieldPanel.Location = new Point(0, 25);

            parametrsForm.SetParametrs += OnSetParametrs;
            parametrsForm.SetSpecialParametrs += OnSetSpecialParametrs;

            DoubleBuffered = true;
            Load += OnViewForm;
            Controls.Add(menu);
            Controls.Add(playerFieldPanel);
            Controls.Add(minesLeftCountLabel);
            Controls.Add(minePicture);
            BackColor = SystemColors.ActiveCaption;
            Text = "Сапер";
            AutoSize = true;
            Icon = new Icon(@"..\..\Resources\mine.ico");

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
        private PictureBox minePicture;
        private Bitmap mineImage;
        private Bitmap flagImage;
        private Size cellSize;
        private Font cellsFont;
    }
}

