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
            this.labelInformation = new System.Windows.Forms.Label();
            this.buttonPingNTPServer = new System.Windows.Forms.Button();
            this.buttonTroubleshootingWebsite = new System.Windows.Forms.Button();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.groupBoxInformation = new System.Windows.Forms.GroupBox();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupBoxInformation.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInformation
            // 
            this.labelInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInformation.Location = new System.Drawing.Point(6, 16);
            this.labelInformation.MaximumSize = new System.Drawing.Size(350, 48);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(339, 29);
            this.labelInformation.TabIndex = 0;
            this.labelInformation.Text = "There are many things that can cause an incorrect code.  We\'ll go through the mos" +
    "t common things here.";
            // 
            // buttonPingNTPServer
            // 
            this.buttonPingNTPServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPingNTPServer.Location = new System.Drawing.Point(6, 19);
            this.buttonPingNTPServer.Name = "buttonPingNTPServer";
            this.buttonPingNTPServer.Size = new System.Drawing.Size(166, 23);
            this.buttonPingNTPServer.TabIndex = 1;
            this.buttonPingNTPServer.Text = "Ping NTP server*";
            this.buttonPingNTPServer.UseVisualStyleBackColor = true;
            this.buttonPingNTPServer.Click += new System.EventHandler(this.buttonPingGoogle_Click);
            // 
            // buttonTroubleshootingWebsite
            // 
            this.buttonTroubleshootingWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTroubleshootingWebsite.Location = new System.Drawing.Point(6, 48);
            this.buttonTroubleshootingWebsite.Name = "buttonTroubleshootingWebsite";
            this.buttonTroubleshootingWebsite.Size = new System.Drawing.Size(339, 23);
            this.buttonTroubleshootingWebsite.TabIndex = 2;
            this.buttonTroubleshootingWebsite.Text = "Check the troubleshooting website*";
            this.buttonTroubleshootingWebsite.UseVisualStyleBackColor = true;
            this.buttonTroubleshootingWebsite.Click += new System.EventHandler(this.buttonTroubleshootingWebsite_Click);
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(375, 58);
            this.pictureBoxBanner.TabIndex = 8;
            this.pictureBoxBanner.TabStop = false;
            // 
            // groupBoxInformation
            // 
            this.groupBoxInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInformation.Controls.Add(this.labelInformation);
            this.groupBoxInformation.Location = new System.Drawing.Point(12, 64);
            this.groupBoxInformation.Name = "groupBoxInformation";
            this.groupBoxInformation.Size = new System.Drawing.Size(351, 54);
            this.groupBoxInformation.TabIndex = 9;
            this.groupBoxInformation.TabStop = false;
            this.groupBoxInformation.Text = "Information";
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.Controls.Add(this.buttonSettings);
            this.groupBoxActions.Controls.Add(this.buttonPingNTPServer);
            this.groupBoxActions.Controls.Add(this.buttonTroubleshootingWebsite);
            this.groupBoxActions.Location = new System.Drawing.Point(12, 124);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(351, 77);
            this.groupBoxActions.TabIndex = 10;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSettings.Location = new System.Drawing.Point(179, 19);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(166, 23);
            this.buttonSettings.TabIndex = 11;
            this.buttonSettings.Text = "Change settings*";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(288, 207);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(207, 207);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Enabled = false;
            this.labelStatus.Location = new System.Drawing.Point(12, 212);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(141, 13);
            this.labelStatus.TabIndex = 13;
            this.labelStatus.Text = "(*Hover for more information)";
            // 
            // Troubleshooting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 243);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBoxInformation);
            this.Controls.Add(this.pictureBoxBanner);
            this.CancelButton = this.buttonCancel;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 48);
            this.Name = "Troubleshooting";
            this.Text = "Troubleshooting";
            this.Load += new System.EventHandler(this.Troubleshooting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupBoxInformation.ResumeLayout(false);
            this.groupBoxActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.Button buttonPingNTPServer;
        private System.Windows.Forms.Button buttonTroubleshootingWebsite;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.GroupBox groupBoxInformation;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelStatus;
    }
}
