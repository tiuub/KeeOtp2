namespace KeeOtp2
{
    partial class ShowOneTimePasswords
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
            this.components = new System.ComponentModel.Container();
            this.timerUpdateOtp = new System.Windows.Forms.Timer(this.components);
            this.labelOtp = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.groupboxTotp = new System.Windows.Forms.GroupBox();
            this.linkLabelIncorrectNext = new System.Windows.Forms.LinkLabel();
            this.buttonCopyTotp = new System.Windows.Forms.Button();
            this.buttonShowQR = new System.Windows.Forms.Button();
            this.linkLabelDonate = new System.Windows.Forms.LinkLabel();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupboxTotp.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdateOtp
            // 
            this.timerUpdateOtp.Tick += new System.EventHandler(this.timerUpdateOtp_Tick);
            // 
            // labelOtp
            // 
            this.labelOtp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOtp.BackColor = System.Drawing.SystemColors.Control;
            this.labelOtp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelOtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOtp.Location = new System.Drawing.Point(8, 20);
            this.labelOtp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOtp.Name = "labelOtp";
            this.labelOtp.Size = new System.Drawing.Size(452, 90);
            this.labelOtp.TabIndex = 0;
            this.labelOtp.Text = "000000";
            this.labelOtp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelOtp.Click += new System.EventHandler(this.labelOtp_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.Location = new System.Drawing.Point(124, 221);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(100, 28);
            this.buttonEdit.TabIndex = 5;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBanner.Location = new System.Drawing.Point(-3, 0);
            this.pictureBoxBanner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(505, 71);
            this.pictureBoxBanner.TabIndex = 7;
            this.pictureBoxBanner.TabStop = false;
            // 
            // groupboxTotp
            // 
            this.groupboxTotp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupboxTotp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupboxTotp.Controls.Add(this.linkLabelIncorrectNext);
            this.groupboxTotp.Controls.Add(this.labelOtp);
            this.groupboxTotp.Location = new System.Drawing.Point(16, 79);
            this.groupboxTotp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupboxTotp.Name = "groupboxTotp";
            this.groupboxTotp.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupboxTotp.Size = new System.Drawing.Size(468, 118);
            this.groupboxTotp.TabIndex = 8;
            this.groupboxTotp.TabStop = false;
            this.groupboxTotp.Text = "TOTP";
            // 
            // linkLabelIncorrectNext
            // 
            this.linkLabelIncorrectNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelIncorrectNext.AutoSize = true;
            this.linkLabelIncorrectNext.Location = new System.Drawing.Point(387, -1);
            this.linkLabelIncorrectNext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelIncorrectNext.Name = "linkLabelIncorrectNext";
            this.linkLabelIncorrectNext.Size = new System.Drawing.Size(65, 16);
            this.linkLabelIncorrectNext.TabIndex = 11;
            this.linkLabelIncorrectNext.TabStop = true;
            this.linkLabelIncorrectNext.Text = "Incorrect?";
            this.linkLabelIncorrectNext.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelIncorrectNext_LinkClicked);
            // 
            // buttonCopyTotp
            // 
            this.buttonCopyTotp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyTotp.Location = new System.Drawing.Point(384, 221);
            this.buttonCopyTotp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCopyTotp.Name = "buttonCopyTotp";
            this.buttonCopyTotp.Size = new System.Drawing.Size(100, 28);
            this.buttonCopyTotp.TabIndex = 9;
            this.buttonCopyTotp.Text = "Copy";
            this.buttonCopyTotp.UseVisualStyleBackColor = true;
            this.buttonCopyTotp.Click += new System.EventHandler(this.buttonCopyTotp_Click);
            // 
            // buttonShowQR
            // 
            this.buttonShowQR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowQR.Location = new System.Drawing.Point(16, 221);
            this.buttonShowQR.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonShowQR.Name = "buttonShowQR";
            this.buttonShowQR.Size = new System.Drawing.Size(100, 28);
            this.buttonShowQR.TabIndex = 10;
            this.buttonShowQR.Text = "Show QR*";
            this.buttonShowQR.UseVisualStyleBackColor = true;
            this.buttonShowQR.Click += new System.EventHandler(this.buttonShowQR_Click);
            // 
            // linkLabelDonate
            // 
            this.linkLabelDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelDonate.AutoSize = true;
            this.linkLabelDonate.Location = new System.Drawing.Point(325, 227);
            this.linkLabelDonate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelDonate.Name = "linkLabelDonate";
            this.linkLabelDonate.Size = new System.Drawing.Size(51, 16);
            this.linkLabelDonate.TabIndex = 11;
            this.linkLabelDonate.TabStop = true;
            this.linkLabelDonate.Text = "Donate";
            this.linkLabelDonate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDonate_LinkClicked);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Enabled = false;
            this.labelStatus.Location = new System.Drawing.Point(15, 201);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(177, 16);
            this.labelStatus.TabIndex = 25;
            this.labelStatus.Text = "(*Hover for more information)";
            // 
            // ShowOneTimePasswords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(500, 263);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.linkLabelDonate);
            this.Controls.Add(this.buttonShowQR);
            this.Controls.Add(this.buttonCopyTotp);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.groupboxTotp);
            this.Controls.Add(this.pictureBoxBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowOneTimePasswords";
            this.Text = "Timed Passwords";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowOneTimePasswords_FormClosing);
            this.Load += new System.EventHandler(this.ShowOneTimePasswords_Load);
            this.Shown += new System.EventHandler(this.ShowOneTimePasswords_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupboxTotp.ResumeLayout(false);
            this.groupboxTotp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdateOtp;
        private System.Windows.Forms.Label labelOtp;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.GroupBox groupboxTotp;
        private System.Windows.Forms.Button buttonCopyTotp;
        private System.Windows.Forms.Button buttonShowQR;
        private System.Windows.Forms.LinkLabel linkLabelIncorrectNext;
        private System.Windows.Forms.LinkLabel linkLabelDonate;
        private System.Windows.Forms.Label labelStatus;
    }
}