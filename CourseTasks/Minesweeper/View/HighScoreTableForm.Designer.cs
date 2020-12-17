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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();

            highScoreTableDataGrid = new DataGridView();
            nickName = new DataGridViewTextBoxColumn();
            Score = new DataGridViewTextBoxColumn();

            ((System.ComponentModel.ISupportInitialize)(highScoreTableDataGrid)).BeginInit();
            SuspendLayout();

            highScoreTableDataGrid.AllowUserToAddRows = false;
            highScoreTableDataGrid.AllowUserToDeleteRows = false;
            highScoreTableDataGrid.Columns.AddRange(new DataGridViewColumn[] { nickName, Score });
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

            Score.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            Score.DefaultCellStyle = dataGridViewCellStyle2;
            Score.HeaderText = "Очки";

            Shown += HighScoreTableForm_ShowForm;

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 281);
            Controls.Add(highScoreTableDataGrid);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimumSize = new Size(456, 315);
            Size = new Size(456, 315);
            Text = "Таблица рекордов";
            ((System.ComponentModel.ISupportInitialize)(highScoreTableDataGrid)).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView highScoreTableDataGrid;
        private DataGridViewTextBoxColumn nickName;
        private DataGridViewTextBoxColumn Score;
    }
}