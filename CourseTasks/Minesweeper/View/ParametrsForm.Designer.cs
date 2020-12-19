using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper.View
{
    partial class ParametrsForm
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
            authorizationTableLayoutPanel = new TableLayoutPanel();
            parametrsNamePanel = new TableLayoutPanel();
            parametrsSetPanel = new TableLayoutPanel();
            parametrsPanel = new TableLayoutPanel();

            columnsNumeric = new NumericUpDown();
            minesNumeric = new NumericUpDown();
            rowsNumeric = new NumericUpDown();

            columnsCountLabel = new Label();
            minesCountLabel = new Label();
            parametrsLabel = new Label();
            rowsCountLabel = new Label();
            nickNameLabel = new Label();

            parametrsBox = new ComboBox();
            applyButton = new Button();
            nameBox = new TextBox();

            parametrsPanel.SuspendLayout();
            parametrsSetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(rowsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(columnsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(minesNumeric)).BeginInit();
            authorizationTableLayoutPanel.SuspendLayout();
            parametrsNamePanel.SuspendLayout();
            SuspendLayout();

            parametrsPanel.BackColor = SystemColors.ActiveCaption;
            parametrsPanel.BorderStyle = BorderStyle.FixedSingle;
            parametrsPanel.ColumnCount = 3;
            parametrsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33332F));
            parametrsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            parametrsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            parametrsPanel.Controls.Add(applyButton, 1, 3);
            parametrsPanel.Controls.Add(parametrsSetPanel, 2, 1);
            parametrsPanel.Controls.Add(authorizationTableLayoutPanel, 1, 2);
            parametrsPanel.Controls.Add(parametrsNamePanel, 0, 1);
            parametrsPanel.Dock = DockStyle.Fill;
            parametrsPanel.Location = new Point(0, 0);
            parametrsPanel.Margin = new Padding(0);
            parametrsPanel.RowCount = 4;
            parametrsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.37141F));
            parametrsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 32.88575F));
            parametrsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.37142F));
            parametrsPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.37142F));
            parametrsPanel.Size = new Size(410, 381);

            applyButton.Anchor = AnchorStyles.None;
            applyButton.Location = new Point(165, 324);
            applyButton.Margin = new Padding(0);
            applyButton.Size = new System.Drawing.Size(75, 23);
            applyButton.Text = "Принять";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += OnApplyButton;

            parametrsSetPanel.ColumnCount = 1;
            parametrsSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            parametrsSetPanel.Controls.Add(rowsNumeric, 0, 1);
            parametrsSetPanel.Controls.Add(columnsNumeric, 0, 3);
            parametrsSetPanel.Controls.Add(minesNumeric, 0, 5);
            parametrsSetPanel.Controls.Add(rowsCountLabel, 0, 0);
            parametrsSetPanel.Controls.Add(columnsCountLabel, 0, 2);
            parametrsSetPanel.Controls.Add(minesCountLabel, 0, 4);
            parametrsSetPanel.Dock = DockStyle.Fill;
            parametrsSetPanel.Location = new Point(276, 89);
            parametrsSetPanel.Margin = new Padding(5);
            parametrsSetPanel.RowCount = 6;
            parametrsSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametrsSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametrsSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametrsSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametrsSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametrsSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametrsSetPanel.Size = new Size(127, 114);

            rowsNumeric.Enabled = false;
            rowsNumeric.Location = new Point(0, 19);
            rowsNumeric.Margin = new Padding(0);
            rowsNumeric.Maximum = 16;
            rowsNumeric.Minimum = 9;
            rowsNumeric.Size = new Size(120, 20);

            columnsNumeric.Enabled = false;
            columnsNumeric.Location = new Point(0, 57);
            columnsNumeric.Margin = new Padding(0);
            columnsNumeric.Maximum = 30;
            columnsNumeric.Minimum = 9;
            columnsNumeric.Size = new Size(120, 20);

            minesNumeric.Enabled = false;
            minesNumeric.Location = new Point(0, 95);
            minesNumeric.Margin = new Padding(0);
            minesNumeric.Minimum = 10;
            minesNumeric.Size = new Size(120, 20);

            parametrsBox.Anchor = AnchorStyles.Top;
            parametrsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            parametrsBox.FormattingEnabled = true;
            parametrsBox.Location = new Point(4, 56);
            parametrsBox.Margin = new Padding(0, 0, 0, 2);
            parametrsBox.Size = new Size(121, 21);
            parametrsBox.SelectionChangeCommitted += OnSelectionChangeCommitted;

            authorizationTableLayoutPanel.ColumnCount = 1;
            authorizationTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            authorizationTableLayoutPanel.Controls.Add(nickNameLabel, 0, 0);
            authorizationTableLayoutPanel.Controls.Add(nameBox, 0, 1);
            authorizationTableLayoutPanel.Location = new Point(138, 211);
            authorizationTableLayoutPanel.RowCount = 2;
            authorizationTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            authorizationTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            authorizationTableLayoutPanel.Size = new Size(126, 74);

            nickNameLabel.Anchor = AnchorStyles.None;
            nickNameLabel.AutoSize = true;
            nickNameLabel.Location = new Point(15, 12);
            nickNameLabel.Size = new Size(96, 13);
            nickNameLabel.Text = "Введите никнейм";

            nameBox.Anchor = AnchorStyles.Top;
            nameBox.Location = new Point(13, 40);
            nameBox.MaxLength = 20;
            nameBox.Size = new Size(100, 20);

            parametrsLabel.Anchor = AnchorStyles.Bottom;
            parametrsLabel.AutoSize = true;
            parametrsLabel.Location = new Point(14, 41);
            parametrsLabel.Margin = new Padding(3, 0, 3, 2);
            parametrsLabel.Size = new Size(101, 13);
            parametrsLabel.Text = "Выберите уровень";

            rowsCountLabel.AutoSize = true;
            rowsCountLabel.Cursor = Cursors.IBeam;
            rowsCountLabel.Dock = DockStyle.Fill;
            rowsCountLabel.Location = new Point(3, 0);
            rowsCountLabel.Size = new Size(121, 19);
            rowsCountLabel.Text = "Количество строк";
            rowsCountLabel.TextAlign = ContentAlignment.MiddleCenter;

            columnsCountLabel.AutoSize = true;
            columnsCountLabel.Location = new Point(3, 38);
            columnsCountLabel.Size = new Size(116, 13);
            columnsCountLabel.Text = "Количество столбцов";
            columnsCountLabel.TextAlign = ContentAlignment.MiddleCenter;

            minesCountLabel.AutoSize = true;
            minesCountLabel.Location = new Point(3, 76);
            minesCountLabel.Size = new Size(89, 13);
            minesCountLabel.Text = "Количество мин";
            minesCountLabel.TextAlign = ContentAlignment.MiddleCenter;

            parametrsNamePanel.ColumnCount = 1;
            parametrsNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            parametrsNamePanel.Controls.Add(parametrsLabel, 0, 0);
            parametrsNamePanel.Controls.Add(parametrsBox, 0, 1);
            parametrsNamePanel.Dock = DockStyle.Fill;
            parametrsNamePanel.Location = new Point(3, 87);
            parametrsNamePanel.RowCount = 2;
            parametrsNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            parametrsNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            parametrsNamePanel.Size = new Size(129, 118);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 381);
            Controls.Add(parametrsPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(419, 384);
            Text = "Настройки";

            parametrsPanel.ResumeLayout(false);
            parametrsSetPanel.ResumeLayout(false);
            parametrsSetPanel.PerformLayout();
            ((ISupportInitialize)(rowsNumeric)).EndInit();
            ((ISupportInitialize)(columnsNumeric)).EndInit();
            ((ISupportInitialize)(minesNumeric)).EndInit();
            authorizationTableLayoutPanel.ResumeLayout(false);
            authorizationTableLayoutPanel.PerformLayout();
            parametrsNamePanel.ResumeLayout(false);
            parametrsNamePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel authorizationTableLayoutPanel;
        private TableLayoutPanel parametrsNamePanel;
        private TableLayoutPanel parametrsSetPanel;
        private TableLayoutPanel parametrsPanel;

        private NumericUpDown rowsNumeric;
        private NumericUpDown columnsNumeric;
        private NumericUpDown minesNumeric;

        private Label columnsCountLabel;
        private Label minesCountLabel;
        private Label parametrsLabel;
        private Label rowsCountLabel;
        private Label nickNameLabel;

        private ComboBox parametrsBox;
        private Button applyButton;
        private TextBox nameBox;
    }
}