namespace KeeOtp2
{
    partial class ShowQrCode
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
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.groupBoxQRCode = new System.Windows.Forms.GroupBox();
            this.pictureBoxQrCode = new System.Windows.Forms.PictureBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCopyUriReload = new System.Windows.Forms.Button();
            this.timerFormTimeout = new System.Windows.Forms.Timer(this.components);
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupBoxQRCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQrCode)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(336, 58);
            this.pictureBoxBanner.TabIndex = 13;
            this.pictureBoxBanner.TabStop = false;
            // 
            // groupBoxQRCode
            // 
            this.groupBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxQRCode.Controls.Add(this.pictureBoxQrCode);
            this.groupBoxQRCode.Location = new System.Drawing.Point(12, 64);
            this.groupBoxQRCode.Name = "groupBoxQRCode";
            this.groupBoxQRCode.Size = new System.Drawing.Size(312, 325);
            this.groupBoxQRCode.TabIndex = 14;
            this.groupBoxQRCode.TabStop = false;
            this.groupBoxQRCode.Text = "QRCode - Scan with your favourite Authenticator App";
            // 
            // pictureBoxQrCode
            // 
            this.pictureBoxQrCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQrCode.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxQrCode.Name = "pictureBoxQrCode";
            this.pictureBoxQrCode.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxQrCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxQrCode.TabIndex = 17;
            this.pictureBoxQrCode.TabStop = false;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(249, 395);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 15;
            this.buttonOk.Text = "Close";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCopyUriReload
            // 
            this.buttonCopyUriReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyUriReload.Location = new System.Drawing.Point(168, 395);
            this.buttonCopyUriReload.Name = "buttonCopyUriReload";
            this.buttonCopyUriReload.Size = new System.Drawing.Size(75, 23);
            this.buttonCopyUriReload.TabIndex = 16;
            this.buttonCopyUriReload.Text = "Copy URI";
            this.buttonCopyUriReload.UseVisualStyleBackColor = true;
            this.buttonCopyUriReload.Click += new System.EventHandler(this.buttonCopyUriReload_Click);
            // 
            // timerFormTimeout
            // 
            this.timerFormTimeout.Interval = 1000;
            this.timerFormTimeout.Tick += new System.EventHandler(this.timerFormTimeout_Tick);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Enabled = false;
            this.labelStatus.Location = new System.Drawing.Point(15, 400);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(141, 13);
            this.labelStatus.TabIndex = 18;
            this.labelStatus.Text = "(*Hover for more information)";
            // 
            // ShowQrCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 426);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonCopyUriReload);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxQRCode);
            this.Controls.Add(this.pictureBoxBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.CancelButton = this.buttonOk;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowQrCode";
            this.Text = "QR Code";
            this.Load += new System.EventHandler(this.ShowQrCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupBoxQRCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQrCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.GroupBox groupBoxQRCode;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCopyUriReload;
        private System.Windows.Forms.PictureBox pictureBoxQrCode;
        private System.Windows.Forms.Timer timerFormTimeout;
        private System.Windows.Forms.Label labelStatus;
    }
}
