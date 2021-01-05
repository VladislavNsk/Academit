using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper.View
{
    partial class HighScoreTableForm
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
            var dataGridViewCellStyle1 = new DataGridViewCellStyle();
            var dataGridViewCellStyle2 = new DataGridViewCellStyle();

            highScoreTableDataGrid = new DataGridView();
            nickName = new DataGridViewTextBoxColumn();
            time = new DataGridViewTextBoxColumn();

            mainPanel = new TableLayoutPanel();
            parametersNamesPanel = new TableLayoutPanel();
            parametersNamesBox = new ComboBox();
            parametersNamesLabel = new Label();
            okButton = new Button();

            ((System.ComponentModel.ISupportInitialize)(highScoreTableDataGrid)).BeginInit();
            SuspendLayout();

            highScoreTableDataGrid.AllowUserToAddRows = false;
            highScoreTableDataGrid.AllowUserToDeleteRows = false;
            highScoreTableDataGrid.Columns.AddRange(new DataGridViewColumn[] { nickName, time });
            highScoreTableDataGrid.Dock = DockStyle.Fill;
            highScoreTableDataGrid.Enabled = false;
            highScoreTableDataGrid.EnableHeadersVisualStyles = false;
            highScoreTableDataGrid.GridColor = Color.LightGray;
            highScoreTableDataGrid.ImeMode = ImeMode.NoControl;
            highScoreTableDataGrid.Location = new Point(0, 0);
            highScoreTableDataGrid.Size = new Size(440, 281);

            nickName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            nickName.DefaultCellStyle = dataGridViewCellStyle1;
            nickName.HeaderText = "Никнейм";

            time.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            time.DefaultCellStyle = dataGridViewCellStyle2;
            time.HeaderText = "Время, сек";

            okButton.Text = "OK";
            okButton.Anchor = AnchorStyles.None;
            okButton.Click += OnOkButton;

            parametersNamesBox.Anchor = AnchorStyles.None;
            parametersNamesBox.SelectionChangeCommitted += OnSelectedIndexChanged;
            parametersNamesBox.DropDownStyle = ComboBoxStyle.DropDownList;

            mainPanel.Controls.Add(highScoreTableDataGrid, 2, 0);
            mainPanel.Controls.Add(parametersNamesPanel, 0, 0);
            mainPanel.Controls.Add(okButton, 0, 1);
            mainPanel.SetColumnSpan(okButton, 3);
            mainPanel.Dock = DockStyle.Fill;

            parametersNamesLabel.Text = "Уровень сложности";
            parametersNamesLabel.TextAlign = ContentAlignment.MiddleCenter;
            parametersNamesLabel.Anchor = AnchorStyles.Bottom;

            parametersNamesPanel.Controls.Add(parametersNamesLabel, 0, 0);
            parametersNamesPanel.Controls.Add(parametersNamesBox, 0, 1);
            parametersNamesPanel.Anchor = AnchorStyles.None;

            Shown += OnShowForm;

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 281);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(462, 370);
            Size = new Size(700, 315);
            base.Text = "Таблица рекордов";
            ((System.ComponentModel.ISupportInitialize)(highScoreTableDataGrid)).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView highScoreTableDataGrid;
        private DataGridViewTextBoxColumn nickName;
        private DataGridViewTextBoxColumn time;
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel parametersNamesPanel;
        private Button okButton;
        private Label parametersNamesLabel;
        private ComboBox parametersNamesBox;
    }
}