namespace BombTimer
{
    partial class UpdateNotifier
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateNotifier));
            label1 = new Label();
            label2 = new Label();
            declineBtn = new Button();
            updateBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(221, 24);
            label1.TabIndex = 0;
            label1.Text = "New version available!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(51, 88);
            label2.Name = "label2";
            label2.Size = new Size(398, 23);
            label2.TabIndex = 1;
            label2.Text = "Do you want to update to the latest version?";
            // 
            // declineBtn
            // 
            declineBtn.FlatStyle = FlatStyle.Popup;
            declineBtn.ForeColor = Color.White;
            declineBtn.Location = new Point(12, 154);
            declineBtn.Name = "declineBtn";
            declineBtn.Size = new Size(112, 34);
            declineBtn.TabIndex = 3;
            declineBtn.Text = "Decline";
            declineBtn.UseVisualStyleBackColor = true;
            declineBtn.Click += declineBtn_Click;
            // 
            // updateBtn
            // 
            updateBtn.FlatStyle = FlatStyle.Popup;
            updateBtn.ForeColor = Color.White;
            updateBtn.Location = new Point(376, 154);
            updateBtn.Name = "updateBtn";
            updateBtn.Size = new Size(112, 34);
            updateBtn.TabIndex = 2;
            updateBtn.Text = "Update";
            updateBtn.UseVisualStyleBackColor = true;
            updateBtn.Click += updateBtn_Click;
            // 
            // UpdateNotifier
            // 
            AutoScaleDimensions = new SizeF(11F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(76, 88, 68);
            ClientSize = new Size(500, 200);
            Controls.Add(updateBtn);
            Controls.Add(declineBtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "UpdateNotifier";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "New version available";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button declineBtn;
        private Button updateBtn;
    }
}