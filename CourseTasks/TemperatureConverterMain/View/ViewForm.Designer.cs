using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace TemperatureConverterMain
{
    partial class ViewForm
    {
        private string[] шкалы =
         {
            "Цельсия",
            "Фаренгейта",
            "Кельвина"
        };

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
            sourceDegreesBox = new TextBox();
            eventLog1 = new EventLog();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            resultDegreesBox = new TextBox();
            convertButton = new Button();
            sourceScaleDegreesBox = new ComboBox();
            resultScaleDegrees = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)(eventLog1)).BeginInit();
            SuspendLayout();

            // sourceDegreesBox

            sourceDegreesBox.Location = new Point(216, 224);
            sourceDegreesBox.Name = "sourceDegreesBox";
            sourceDegreesBox.Size = new Size(109, 20);
            sourceDegreesBox.TabIndex = 1;

            // eventLog1

            eventLog1.SynchronizingObject = this;

            // label1

            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            label1.Location = new Point(227, 9);
            label1.Name = "label1";
            label1.Size = new Size(345, 33);
            label1.TabIndex = 2;
            label1.Text = "Конвертер температуры";

            // label2

            label2.AutoSize = true;
            label2.Location = new Point(213, 208);
            label2.Name = "label2";
            label2.Size = new Size(99, 13);
            label2.TabIndex = 3;
            label2.Text = "Введите значение";

            // label3

            label3.AutoSize = true;
            label3.Location = new Point(486, 208);
            label3.Name = "label3";
            label3.Size = new Size(59, 13);
            label3.TabIndex = 4;
            label3.Text = "Результат";

            // resultDegreesBox

            resultDegreesBox.Enabled = false;
            resultDegreesBox.Location = new Point(462, 224);
            resultDegreesBox.Name = "resultDegreesBox";
            resultDegreesBox.Size = new Size(110, 20);
            resultDegreesBox.TabIndex = 5;

            // convertButton

            convertButton.Location = new Point(363, 277);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(75, 23);
            convertButton.TabIndex = 6;
            convertButton.Text = "Конвертировать";
            convertButton.UseVisualStyleBackColor = true;
            convertButton.Click += new System.EventHandler(ConvertButton_Click);

            // sourceScaleDegreesBox

            sourceScaleDegreesBox.Items.AddRange(шкалы);
            sourceScaleDegreesBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceScaleDegreesBox.Location = new Point(216, 141);
            sourceScaleDegreesBox.Name = "sourceScaleDegreesBox";
            sourceScaleDegreesBox.Size = new Size(109, 21);
            sourceScaleDegreesBox.TabIndex = 7;

            // resultScaleDegrees

            resultScaleDegrees.Items.AddRange(шкалы);
            resultScaleDegrees.DropDownStyle = ComboBoxStyle.DropDownList;
            resultScaleDegrees.FormattingEnabled = true;
            resultScaleDegrees.Location = new Point(462, 141);
            resultScaleDegrees.Name = "resultScaleDegrees";
            resultScaleDegrees.Size = new Size(110, 21);
            resultScaleDegrees.TabIndex = 8;


            // Form1

            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(resultScaleDegrees);
            Controls.Add(sourceScaleDegreesBox);
            Controls.Add(convertButton);
            Controls.Add(resultDegreesBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(sourceDegreesBox);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(eventLog1)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox sourceDegreesBox;
        private EventLog eventLog1;
        private Label label1;
        private Label label2;
        private TextBox resultDegreesBox;
        private Label label3;
        private Button convertButton;
        private ComboBox resultScaleDegrees;
        private ComboBox sourceScaleDegreesBox;
    }
}

