using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace TemperatureConverterMain
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
            nameProjectLabel = new Label();
            sourceDegreesLabel = new Label();
            resultDegreesLabel = new Label();
            sourceScaleLabel = new Label();
            resultScaleLabel = new Label();

            resultDegreesTb = new TextBox();
            sourceDegreesTb = new TextBox();

            resultScaleDegreesCb = new ComboBox();
            sourceScaleDegreesCb = new ComboBox();

            tableLayoutPanel = new TableLayoutPanel();
            eventLog1 = new EventLog();
            convertButton = new Button();

            ((System.ComponentModel.ISupportInitialize)(eventLog1)).BeginInit();
            SuspendLayout();

            eventLog1.SynchronizingObject = this;

            sourceDegreesLabel.AutoSize = true;
            sourceDegreesLabel.Text = "Введите значение градусов:";
            sourceDegreesLabel.Anchor = AnchorStyles.Bottom;
            sourceDegreesLabel.Margin = new Padding(0, 20, 0,0);

            resultDegreesLabel.AutoSize = true;
            resultDegreesLabel.Text = "Результат:";
            resultDegreesLabel.Anchor = AnchorStyles.Bottom;
            resultDegreesLabel.Margin = new Padding(0, 20, 0, 0);

            resultDegreesTb.Enabled = false;
            resultDegreesTb.Size = new Size(110, 20);
            resultDegreesTb.Anchor = AnchorStyles.Top;

            sourceDegreesTb.Size = new Size(110, 20);
            sourceDegreesTb.Anchor = AnchorStyles.Top;

            convertButton.AutoSize = true;
            convertButton.Text = "Конвертировать";
            convertButton.UseVisualStyleBackColor = true;
            convertButton.Click += new System.EventHandler(ConvertButton_Click);
            convertButton.Anchor = AnchorStyles.None;

            sourceScaleDegreesCb.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceScaleDegreesCb.Size = new Size(109, 21);
            sourceScaleDegreesCb.Anchor = AnchorStyles.Top;

            resultScaleDegreesCb.DropDownStyle = ComboBoxStyle.DropDownList;
            resultScaleDegreesCb.FormattingEnabled = true;
            resultScaleDegreesCb.Size = new Size(110, 21);
            resultScaleDegreesCb.Anchor = AnchorStyles.Top;

            sourceScaleLabel.AutoSize = true;
            sourceScaleLabel.Text = "Из какой шкалы конвертировать:";
            sourceScaleLabel.Anchor = AnchorStyles.Bottom;
            sourceScaleLabel.Margin = new Padding(0, 20, 0, 0);

            resultScaleLabel.AutoSize = true;
            resultScaleLabel.Text = "В какую шкалу конвертировать:";
            resultScaleLabel.Anchor = AnchorStyles.Bottom;
            resultScaleLabel.Margin = new Padding(0, 20, 0, 0);

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);

            Controls.Add(tableLayoutPanel);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.AutoSize = true;
            Load += ViewForm_Load;
            Text = "Конвертер температуры";
            MinimumSize = new Size(650, 450);

            nameProjectLabel.Text = "Конвертер температуры";
            nameProjectLabel.Anchor = AnchorStyles.None;
            nameProjectLabel.AutoSize = true;
            nameProjectLabel.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));

            ((System.ComponentModel.ISupportInitialize)(eventLog1)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox sourceDegreesTb;
        private EventLog eventLog1;
        private Label nameProjectLabel;
        private Label sourceDegreesLabel;
        private TextBox resultDegreesTb;
        private Label resultDegreesLabel;
        private Button convertButton;
        private ComboBox resultScaleDegreesCb;
        private ComboBox sourceScaleDegreesCb;
        private Label sourceScaleLabel;
        private Label resultScaleLabel;
        private TableLayoutPanel tableLayoutPanel;
    }
}

