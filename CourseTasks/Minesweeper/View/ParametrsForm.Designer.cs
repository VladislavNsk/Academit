using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper.View
{
    partial class ParametersForm
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
            authorizationPanel = new TableLayoutPanel();
            parametersNamePanel = new TableLayoutPanel();
            parametersSetPanel = new TableLayoutPanel();
            parametersPanel = new TableLayoutPanel();

            columnsNumeric = new NumericUpDown();
            minesNumeric = new NumericUpDown();
            rowsNumeric = new NumericUpDown();

            columnsCountLabel = new Label();
            minesCountLabel = new Label();
            parametersLabel = new Label();
            rowsCountLabel = new Label();
            nickNameLabel = new Label();

            parametersBox = new ComboBox();
            applyButton = new Button();
            nameBox = new TextBox();

            parametersPanel.SuspendLayout();
            parametersSetPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(rowsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(columnsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(minesNumeric)).BeginInit();
            authorizationPanel.SuspendLayout();
            parametersNamePanel.SuspendLayout();
            SuspendLayout();

            parametersPanel.BackColor = SystemColors.ActiveCaption;
            parametersPanel.BorderStyle = BorderStyle.FixedSingle;
            parametersPanel.ColumnCount = 3;
            parametersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33332F));
            parametersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.33334F));
            parametersPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));
            parametersPanel.Controls.Add(applyButton, 1, 3);
            parametersPanel.Controls.Add(parametersSetPanel, 1, 1);
            parametersPanel.Controls.Add(authorizationPanel, 1, 2);
            parametersPanel.Controls.Add(parametersNamePanel, 1, 0);
            parametersPanel.Dock = DockStyle.Fill;
            parametersPanel.Location = new Point(0, 0);
            parametersPanel.Margin = new Padding(0);
            parametersPanel.RowCount = 4;
            parametersPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.37141F));
            parametersPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            parametersPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.37142F));
            parametersPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 22.37142F));
            parametersPanel.Size = new Size(410, 381);

            applyButton.Anchor = AnchorStyles.None;
            applyButton.Location = new Point(165, 324);
            applyButton.Margin = new Padding(0);
            applyButton.Size = new System.Drawing.Size(75, 23);
            applyButton.Text = "Принять";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += OnApplyButton;

            parametersSetPanel.ColumnCount = 1;
            parametersSetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            parametersSetPanel.Controls.Add(rowsNumeric, 0, 1);
            parametersSetPanel.Controls.Add(columnsNumeric, 0, 3);
            parametersSetPanel.Controls.Add(minesNumeric, 0, 5);
            parametersSetPanel.Controls.Add(rowsCountLabel, 0, 0);
            parametersSetPanel.Controls.Add(columnsCountLabel, 0, 2);
            parametersSetPanel.Controls.Add(minesCountLabel, 0, 4);
            parametersSetPanel.Dock = DockStyle.Fill;
            parametersSetPanel.Location = new Point(276, 89);
            parametersSetPanel.Margin = new Padding(5);
            parametersSetPanel.RowCount = 6;
            parametersSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametersSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametersSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametersSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametersSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametersSetPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
            parametersSetPanel.Size = new Size(127, 114);

            rowsNumeric.Enabled = false;
            rowsNumeric.Location = new Point(0, 19);
            rowsNumeric.Margin = new Padding(0, 0, 0, 5);
            rowsNumeric.Maximum = 16;
            rowsNumeric.Minimum = 9;
            rowsNumeric.Size = new Size(120, 20);
            rowsNumeric.Anchor = AnchorStyles.None;

            columnsNumeric.Enabled = false;
            columnsNumeric.Location = new Point(0, 57);
            columnsNumeric.Margin = new Padding(0, 0, 0, 5);
            columnsNumeric.Maximum = 30;
            columnsNumeric.Minimum = 9;
            columnsNumeric.Size = new Size(120, 20);
            columnsNumeric.Anchor = AnchorStyles.None;

            minesNumeric.Enabled = false;
            minesNumeric.Location = new Point(0, 95);
            minesNumeric.Margin = new Padding(0, 0, 0, 5);
            minesNumeric.Minimum = 10;
            minesNumeric.Size = new Size(120, 20);
            minesNumeric.Anchor = AnchorStyles.None;

            parametersBox.Anchor = AnchorStyles.Top;
            parametersBox.DropDownStyle = ComboBoxStyle.DropDownList;
            parametersBox.FormattingEnabled = true;
            parametersBox.Location = new Point(4, 56);
            parametersBox.Margin = new Padding(0, 0, 0, 2);
            parametersBox.Size = new Size(121, 21);
            parametersBox.SelectionChangeCommitted += OnSelectionChangeCommitted;

            authorizationPanel.ColumnCount = 1;
            authorizationPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            authorizationPanel.Controls.Add(nickNameLabel, 0, 0);
            authorizationPanel.Controls.Add(nameBox, 0, 1);
            authorizationPanel.Location = new Point(138, 211);
            authorizationPanel.RowCount = 2;
            authorizationPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            authorizationPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            authorizationPanel.Size = new Size(126, 74);
            authorizationPanel.Dock = DockStyle.Fill;

            nickNameLabel.Anchor = AnchorStyles.None;
            nickNameLabel.AutoSize = true;
            nickNameLabel.Location = new Point(15, 12);
            nickNameLabel.Size = new Size(96, 13);
            nickNameLabel.Text = "Введите никнейм";

            nameBox.Location = new Point(13, 40);
            nameBox.MaxLength = 20;
            nameBox.Size = new Size(100, 20);
            nameBox.Anchor = AnchorStyles.None;

            parametersLabel.Anchor = AnchorStyles.Bottom;
            parametersLabel.AutoSize = true;
            parametersLabel.Location = new Point(14, 41);
            parametersLabel.Margin = new Padding(3, 0, 3, 2);
            parametersLabel.Size = new Size(101, 13);
            parametersLabel.Text = "Выберите уровень";

            rowsCountLabel.AutoSize = true;
            rowsCountLabel.Cursor = Cursors.IBeam;
            rowsCountLabel.Dock = DockStyle.Fill;
            rowsCountLabel.Location = new Point(3, 0);
            rowsCountLabel.Size = new Size(121, 19);
            rowsCountLabel.Text = "Количество строк";
            rowsCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            rowsCountLabel.Anchor = AnchorStyles.None;

            columnsCountLabel.AutoSize = true;
            columnsCountLabel.Location = new Point(3, 38);
            columnsCountLabel.Size = new Size(116, 13);
            columnsCountLabel.Text = "Количество столбцов";
            columnsCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            columnsCountLabel.Anchor = AnchorStyles.None;

            minesCountLabel.AutoSize = true;
            minesCountLabel.Location = new Point(3, 76);
            minesCountLabel.Size = new Size(89, 13);
            minesCountLabel.Text = "Количество мин";
            minesCountLabel.TextAlign = ContentAlignment.MiddleCenter;
            minesCountLabel.Anchor = AnchorStyles.None;

            parametersNamePanel.ColumnCount = 1;
            parametersNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            parametersNamePanel.Controls.Add(parametersLabel, 0, 0);
            parametersNamePanel.Controls.Add(parametersBox, 0, 1);
            parametersNamePanel.Dock = DockStyle.Fill;
            parametersNamePanel.Location = new Point(3, 87);
            parametersNamePanel.RowCount = 2;
            parametersNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            parametersNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));
            parametersNamePanel.Size = new Size(129, 118);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 381);
            Controls.Add(parametersPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(419, 384);
            Text = "Настройки";

            parametersPanel.ResumeLayout(false);
            parametersSetPanel.ResumeLayout(false);
            parametersSetPanel.PerformLayout();
            ((ISupportInitialize)(rowsNumeric)).EndInit();
            ((ISupportInitialize)(columnsNumeric)).EndInit();
            ((ISupportInitialize)(minesNumeric)).EndInit();
            authorizationPanel.ResumeLayout(false);
            authorizationPanel.PerformLayout();
            parametersNamePanel.ResumeLayout(false);
            parametersNamePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel authorizationPanel;
        private TableLayoutPanel parametersNamePanel;
        private TableLayoutPanel parametersSetPanel;
        private TableLayoutPanel parametersPanel;

        private NumericUpDown rowsNumeric;
        private NumericUpDown columnsNumeric;
        private NumericUpDown minesNumeric;

        private Label columnsCountLabel;
        private Label minesCountLabel;
        private Label parametersLabel;
        private Label rowsCountLabel;
        private Label nickNameLabel;

        private ComboBox parametersBox;
        private Button applyButton;
        private TextBox nameBox;
    }
}