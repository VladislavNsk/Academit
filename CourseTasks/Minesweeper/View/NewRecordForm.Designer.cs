using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper.View
{
    partial class NewRecordForm
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
            playerNameBox = new TextBox();
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            okButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();

            playerNameBox.Anchor = AnchorStyles.None;
            playerNameBox.CausesValidation = false;
            playerNameBox.Location = new System.Drawing.Point(158, 141);
            playerNameBox.MaxLength = 20;
            playerNameBox.Size = new System.Drawing.Size(100, 20);

            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.ImageAlign = ContentAlignment.TopRight;
            label1.Location = new Point(35, 75);
            label1.Size = new Size(347, 26);
            label1.Text = "Поздравляем! \r\nВы установили новый рекорд, пожалуйста, введите свой никнейм.";
            label1.TextAlign = ContentAlignment.MiddleCenter;

            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(playerNameBox, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(okButton, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
            tableLayoutPanel1.Size = new Size(417, 307);

            okButton.Anchor = AnchorStyles.None;
            okButton.Location = new Point(171, 243);
            okButton.Size = new Size(75, 23);
            okButton.Text = "Ок";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OnOkButton;

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 307);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "NewRecordForm";
            Text = "Новый рекорд";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private TextBox playerNameBox;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button okButton;
    }
}