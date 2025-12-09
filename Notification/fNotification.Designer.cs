namespace Notification
{
    partial class fNotification
    {
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMinutes;
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
        /// 
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // fNotification
            // 
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(20, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 35);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Đang tải...";
            // 
            // lblMinutes
            // 
            this.lblMinutes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMinutes.Location = new System.Drawing.Point(20, 70);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(300, 25);
            this.lblMinutes.TabIndex = 1;
            this.lblMinutes.Text = "";
            // 
            // fNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 130);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.lblStatus);
            this.Name = "fNotification";
            this.Text = "Server Notification";
            this.Load += new System.EventHandler(this.fNotification_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}