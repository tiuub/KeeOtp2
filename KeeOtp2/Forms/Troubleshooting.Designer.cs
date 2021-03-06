namespace KeeOtp2
{
    partial class Troubleshooting
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
            this.labelHeader = new System.Windows.Forms.Label();
            this.buttonPingNTPServer = new System.Windows.Forms.Button();
            this.buttonTroubleshootingWebsite = new System.Windows.Forms.Button();
            this.progressBarGettingTimeCorrection = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHeader.Location = new System.Drawing.Point(13, 13);
            this.labelHeader.MaximumSize = new System.Drawing.Size(350, 48);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(341, 48);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "There are many things that can cause an incorrect code.  We\'ll go through the mos" +
    "t common things here.";
            // 
            // buttonPingNTPServer
            // 
            this.buttonPingNTPServer.Location = new System.Drawing.Point(16, 55);
            this.buttonPingNTPServer.Name = "buttonPingNTPServer";
            this.buttonPingNTPServer.Size = new System.Drawing.Size(338, 23);
            this.buttonPingNTPServer.TabIndex = 1;
            this.buttonPingNTPServer.Text = "Ping NTP server for current time";
            this.buttonPingNTPServer.UseVisualStyleBackColor = true;
            this.buttonPingNTPServer.Click += new System.EventHandler(this.buttonPingGoogle_Click);
            // 
            // buttonTroubleshootingWebsite
            // 
            this.buttonTroubleshootingWebsite.Location = new System.Drawing.Point(16, 85);
            this.buttonTroubleshootingWebsite.Name = "buttonTroubleshootingWebsite";
            this.buttonTroubleshootingWebsite.Size = new System.Drawing.Size(338, 23);
            this.buttonTroubleshootingWebsite.TabIndex = 2;
            this.buttonTroubleshootingWebsite.Text = "Check the troubleshooting website";
            this.buttonTroubleshootingWebsite.UseVisualStyleBackColor = true;
            this.buttonTroubleshootingWebsite.Click += new System.EventHandler(this.buttonTroubleshootingWebsite_Click);
            // 
            // progressBarGettingTimeCorrection
            // 
            this.progressBarGettingTimeCorrection.Location = new System.Drawing.Point(16, 55);
            this.progressBarGettingTimeCorrection.Name = "progressBarGettingTimeCorrection";
            this.progressBarGettingTimeCorrection.Size = new System.Drawing.Size(338, 23);
            this.progressBarGettingTimeCorrection.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarGettingTimeCorrection.TabIndex = 3;
            this.progressBarGettingTimeCorrection.Visible = false;
            // 
            // Troubleshooting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 129);
            this.Controls.Add(this.buttonTroubleshootingWebsite);
            this.Controls.Add(this.buttonPingNTPServer);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.progressBarGettingTimeCorrection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 48);
            this.Name = "Troubleshooting";
            this.Text = "Troubleshooting";
            this.Load += new System.EventHandler(this.Troubleshooting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Button buttonPingNTPServer;
        private System.Windows.Forms.Button buttonTroubleshootingWebsite;
        private System.Windows.Forms.ProgressBar progressBarGettingTimeCorrection;
    }
}