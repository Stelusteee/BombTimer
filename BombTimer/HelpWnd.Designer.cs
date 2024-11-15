namespace BombTimer
{
    partial class HelpWnd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // TITLEBAR
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Color lightColor = Color.FromArgb(136, 145, 128);
            Color darkColor = Color.FromArgb(40, 46, 34);
            int borderThickness = 2;

            PointF scaleFactor = new PointF((float)Width / 400, (float)Height / 300);

            using (Pen borderPen = new Pen(lightColor, borderThickness))
            {
                g.DrawRectangle(borderPen, 0, 0, 400, borderThickness);
                g.DrawRectangle(borderPen, 0, 0, borderThickness, 300);
            }

            using (Pen borderPen = new Pen(darkColor, borderThickness))
            {
                g.DrawRectangle(borderPen, 0, 297 * scaleFactor.Y, 400, borderThickness);
                g.DrawRectangle(borderPen, 397 * scaleFactor.X, 0, borderThickness, 300);
            }

            //var titleClr = new SolidBrush(Color.FromArgb(40, 46, 34));
            //e.Graphics.FillRectangle(titleClr, 0, 0, 400, 40);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            HelpLabel = new Label();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // HelpLabel
            // 
            HelpLabel.AutoSize = true;
            HelpLabel.Location = new Point(12, 9);
            HelpLabel.Name = "HelpLabel";
            HelpLabel.Size = new Size(49, 25);
            HelpLabel.TabIndex = 0;
            HelpLabel.Text = "Help";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.ForeColor = SystemColors.HighlightText;
            button1.Location = new Point(276, 254);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 2;
            button1.Text = "Ok!";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 80);
            label1.Name = "label1";
            label1.Size = new Size(361, 25);
            label1.TabIndex = 3;
            label1.Text = "Insert time and press Enter to start the timer";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 105);
            label2.Name = "label2";
            label2.Size = new Size(288, 25);
            label2.TabIndex = 4;
            label2.Text = "Press Backspace to clear time input";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 130);
            label3.Name = "label3";
            label3.Size = new Size(364, 25);
            label3.TabIndex = 5;
            label3.Text = "Press and hold the lmb to move the window";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 155);
            label4.Name = "label4";
            label4.Size = new Size(225, 25);
            label4.TabIndex = 6;
            label4.Text = "Good luck on your project!";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 180);
            label5.Name = "label5";
            label5.Size = new Size(59, 25);
            label5.TabIndex = 7;
            label5.Text = "v1.0.0";
            // 
            // HelpWnd
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(76, 88, 68);
            ClientSize = new Size(400, 300);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(HelpLabel);
            ForeColor = SystemColors.HighlightText;
            FormBorderStyle = FormBorderStyle.None;
            Name = "HelpWnd";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label HelpLabel;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        public Label label5;
    }
}