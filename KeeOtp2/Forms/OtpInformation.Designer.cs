namespace KeeOtp2
{
    partial class OtpInformation
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
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.radioButtonSix = new System.Windows.Forms.RadioButton();
            this.labelKey = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonEight = new System.Windows.Forms.RadioButton();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonBase32 = new System.Windows.Forms.RadioButton();
            this.radioButtonBase64 = new System.Windows.Forms.RadioButton();
            this.radioButtonHex = new System.Windows.Forms.RadioButton();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.checkBoxCustomSettings = new System.Windows.Forms.CheckBox();
            this.radioButtonSha256 = new System.Windows.Forms.RadioButton();
            this.radioButtonSha512 = new System.Windows.Forms.RadioButton();
            this.radioButtonSha1 = new System.Windows.Forms.RadioButton();
            this.groupboxHashAlgorithm = new System.Windows.Forms.GroupBox();
            this.groupboxTimeStep = new System.Windows.Forms.GroupBox();
            this.groupboxEncoding = new System.Windows.Forms.GroupBox();
            this.radioButtonUtf8 = new System.Windows.Forms.RadioButton();
            this.groupboxSize = new System.Windows.Forms.GroupBox();
            this.groupboxInfo = new System.Windows.Forms.GroupBox();
            this.checkboxOldKeeOtp = new System.Windows.Forms.CheckBox();
            this.buttonMigrate = new System.Windows.Forms.Button();
            this.pictureBoxMigrateQuestionmark = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupboxHashAlgorithm.SuspendLayout();
            this.groupboxTimeStep.SuspendLayout();
            this.groupboxEncoding.SuspendLayout();
            this.groupboxSize.SuspendLayout();
            this.groupboxInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMigrateQuestionmark)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(23, 85);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(342, 20);
            this.textBoxKey.TabIndex = 0;
            this.textBoxKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxKey_KeyDown);
            // 
            // textBoxStep
            // 
            this.textBoxStep.Location = new System.Drawing.Point(118, 13);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(23, 20);
            this.textBoxStep.TabIndex = 1;
            // 
            // radioButtonSix
            // 
            this.radioButtonSix.AutoSize = true;
            this.radioButtonSix.Checked = true;
            this.radioButtonSix.Location = new System.Drawing.Point(6, 19);
            this.radioButtonSix.Name = "radioButtonSix";
            this.radioButtonSix.Size = new System.Drawing.Size(60, 17);
            this.radioButtonSix.TabIndex = 2;
            this.radioButtonSix.TabStop = true;
            this.radioButtonSix.Text = "6 Digits";
            this.radioButtonSix.UseVisualStyleBackColor = true;
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(20, 69);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(25, 13);
            this.labelKey.TabIndex = 3;
            this.labelKey.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time Step (Seconds)";
            // 
            // radioButtonEight
            // 
            this.radioButtonEight.AutoSize = true;
            this.radioButtonEight.Location = new System.Drawing.Point(6, 42);
            this.radioButtonEight.Name = "radioButtonEight";
            this.radioButtonEight.Size = new System.Drawing.Size(60, 17);
            this.radioButtonEight.TabIndex = 6;
            this.radioButtonEight.Text = "8 Digits";
            this.radioButtonEight.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(209, 304);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(290, 304);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonBase32
            // 
            this.radioButtonBase32.AutoSize = true;
            this.radioButtonBase32.Checked = true;
            this.radioButtonBase32.Location = new System.Drawing.Point(6, 19);
            this.radioButtonBase32.Name = "radioButtonBase32";
            this.radioButtonBase32.Size = new System.Drawing.Size(64, 17);
            this.radioButtonBase32.TabIndex = 9;
            this.radioButtonBase32.TabStop = true;
            this.radioButtonBase32.Text = "Base 32";
            this.radioButtonBase32.UseVisualStyleBackColor = true;
            // 
            // radioButtonBase64
            // 
            this.radioButtonBase64.AutoSize = true;
            this.radioButtonBase64.Location = new System.Drawing.Point(6, 42);
            this.radioButtonBase64.Name = "radioButtonBase64";
            this.radioButtonBase64.Size = new System.Drawing.Size(64, 17);
            this.radioButtonBase64.TabIndex = 10;
            this.radioButtonBase64.Text = "Base 64";
            this.radioButtonBase64.UseVisualStyleBackColor = true;
            // 
            // radioButtonHex
            // 
            this.radioButtonHex.AutoSize = true;
            this.radioButtonHex.Location = new System.Drawing.Point(6, 65);
            this.radioButtonHex.Name = "radioButtonHex";
            this.radioButtonHex.Size = new System.Drawing.Size(44, 17);
            this.radioButtonHex.TabIndex = 11;
            this.radioButtonHex.Text = "Hex";
            this.radioButtonHex.UseVisualStyleBackColor = true;
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, -1);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(383, 58);
            this.pictureBoxBanner.TabIndex = 12;
            this.pictureBoxBanner.TabStop = false;
            // 
            // checkBoxCustomSettings
            // 
            this.checkBoxCustomSettings.AutoSize = true;
            this.checkBoxCustomSettings.Location = new System.Drawing.Point(23, 112);
            this.checkBoxCustomSettings.Name = "checkBoxCustomSettings";
            this.checkBoxCustomSettings.Size = new System.Drawing.Size(124, 17);
            this.checkBoxCustomSettings.TabIndex = 13;
            this.checkBoxCustomSettings.Text = "Use Custom Settings";
            this.checkBoxCustomSettings.UseVisualStyleBackColor = true;
            this.checkBoxCustomSettings.CheckedChanged += new System.EventHandler(this.checkBoxCustomSettings_CheckedChanged);
            // 
            // radioButtonSha256
            // 
            this.radioButtonSha256.AutoSize = true;
            this.radioButtonSha256.Location = new System.Drawing.Point(6, 42);
            this.radioButtonSha256.Name = "radioButtonSha256";
            this.radioButtonSha256.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSha256.TabIndex = 14;
            this.radioButtonSha256.Text = "Sha-256";
            this.radioButtonSha256.UseVisualStyleBackColor = true;
            // 
            // radioButtonSha512
            // 
            this.radioButtonSha512.AutoSize = true;
            this.radioButtonSha512.Location = new System.Drawing.Point(6, 65);
            this.radioButtonSha512.Name = "radioButtonSha512";
            this.radioButtonSha512.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSha512.TabIndex = 15;
            this.radioButtonSha512.Text = "Sha-512";
            this.radioButtonSha512.UseVisualStyleBackColor = true;
            // 
            // radioButtonSha1
            // 
            this.radioButtonSha1.AutoSize = true;
            this.radioButtonSha1.Checked = true;
            this.radioButtonSha1.Location = new System.Drawing.Point(6, 19);
            this.radioButtonSha1.Name = "radioButtonSha1";
            this.radioButtonSha1.Size = new System.Drawing.Size(53, 17);
            this.radioButtonSha1.TabIndex = 16;
            this.radioButtonSha1.TabStop = true;
            this.radioButtonSha1.Text = "Sha-1";
            this.radioButtonSha1.UseVisualStyleBackColor = true;
            // 
            // groupboxHashAlgorithm
            // 
            this.groupboxHashAlgorithm.Controls.Add(this.radioButtonSha1);
            this.groupboxHashAlgorithm.Controls.Add(this.radioButtonSha512);
            this.groupboxHashAlgorithm.Controls.Add(this.radioButtonSha256);
            this.groupboxHashAlgorithm.Location = new System.Drawing.Point(255, 182);
            this.groupboxHashAlgorithm.Name = "groupboxHashAlgorithm";
            this.groupboxHashAlgorithm.Size = new System.Drawing.Size(110, 113);
            this.groupboxHashAlgorithm.TabIndex = 17;
            this.groupboxHashAlgorithm.TabStop = false;
            this.groupboxHashAlgorithm.Text = "Hash Algorithm";
            // 
            // groupboxTimeStep
            // 
            this.groupboxTimeStep.Controls.Add(this.label2);
            this.groupboxTimeStep.Controls.Add(this.textBoxStep);
            this.groupboxTimeStep.Location = new System.Drawing.Point(23, 135);
            this.groupboxTimeStep.Name = "groupboxTimeStep";
            this.groupboxTimeStep.Size = new System.Drawing.Size(168, 41);
            this.groupboxTimeStep.TabIndex = 17;
            this.groupboxTimeStep.TabStop = false;
            this.groupboxTimeStep.Text = "Time Step";
            // 
            // groupboxEncoding
            // 
            this.groupboxEncoding.Controls.Add(this.radioButtonUtf8);
            this.groupboxEncoding.Controls.Add(this.radioButtonBase32);
            this.groupboxEncoding.Controls.Add(this.radioButtonBase64);
            this.groupboxEncoding.Controls.Add(this.radioButtonHex);
            this.groupboxEncoding.Location = new System.Drawing.Point(23, 182);
            this.groupboxEncoding.Name = "groupboxEncoding";
            this.groupboxEncoding.Size = new System.Drawing.Size(110, 113);
            this.groupboxEncoding.TabIndex = 5;
            this.groupboxEncoding.TabStop = false;
            this.groupboxEncoding.Text = "Encoding";
            // 
            // radioButtonUtf8
            // 
            this.radioButtonUtf8.AutoSize = true;
            this.radioButtonUtf8.Location = new System.Drawing.Point(6, 88);
            this.radioButtonUtf8.Name = "radioButtonUtf8";
            this.radioButtonUtf8.Size = new System.Drawing.Size(48, 17);
            this.radioButtonUtf8.TabIndex = 12;
            this.radioButtonUtf8.Text = "Utf-8";
            this.radioButtonUtf8.UseVisualStyleBackColor = true;
            // 
            // groupboxSize
            // 
            this.groupboxSize.Controls.Add(this.radioButtonSix);
            this.groupboxSize.Controls.Add(this.radioButtonEight);
            this.groupboxSize.Location = new System.Drawing.Point(139, 182);
            this.groupboxSize.Name = "groupboxSize";
            this.groupboxSize.Size = new System.Drawing.Size(110, 113);
            this.groupboxSize.TabIndex = 12;
            this.groupboxSize.TabStop = false;
            this.groupboxSize.Text = "Size";
            // 
            // groupboxInfo
            // 
            this.groupboxInfo.Controls.Add(this.checkboxOldKeeOtp);
            this.groupboxInfo.Location = new System.Drawing.Point(197, 135);
            this.groupboxInfo.Name = "groupboxInfo";
            this.groupboxInfo.Size = new System.Drawing.Size(168, 41);
            this.groupboxInfo.TabIndex = 5;
            this.groupboxInfo.TabStop = false;
            this.groupboxInfo.Text = "KeeOtp1 String (Deprecated)";
            // 
            // checkboxOldKeeOtp
            // 
            this.checkboxOldKeeOtp.AutoSize = true;
            this.checkboxOldKeeOtp.Location = new System.Drawing.Point(6, 15);
            this.checkboxOldKeeOtp.Name = "checkboxOldKeeOtp";
            this.checkboxOldKeeOtp.Size = new System.Drawing.Size(156, 17);
            this.checkboxOldKeeOtp.TabIndex = 0;
            this.checkboxOldKeeOtp.Text = "Use old KeeOtp save mode";
            this.checkboxOldKeeOtp.UseVisualStyleBackColor = true;
            // 
            // buttonMigrate
            // 
            this.buttonMigrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMigrate.Location = new System.Drawing.Point(23, 304);
            this.buttonMigrate.Name = "buttonMigrate";
            this.buttonMigrate.Size = new System.Drawing.Size(97, 23);
            this.buttonMigrate.TabIndex = 18;
            this.buttonMigrate.Text = "Migrate to Built-In";
            this.buttonMigrate.UseVisualStyleBackColor = true;
            this.buttonMigrate.Visible = false;
            this.buttonMigrate.Click += new System.EventHandler(this.buttonMigrate_Click);
            // 
            // pictureBoxMigrateQuestionmark
            // 
            this.pictureBoxMigrateQuestionmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxMigrateQuestionmark.Location = new System.Drawing.Point(126, 304);
            this.pictureBoxMigrateQuestionmark.Name = "pictureBoxMigrateQuestionmark";
            this.pictureBoxMigrateQuestionmark.Size = new System.Drawing.Size(23, 23);
            this.pictureBoxMigrateQuestionmark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMigrateQuestionmark.TabIndex = 19;
            this.pictureBoxMigrateQuestionmark.TabStop = false;
            this.pictureBoxMigrateQuestionmark.Visible = false;
            // 
            // OtpInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 339);
            this.Controls.Add(this.pictureBoxMigrateQuestionmark);
            this.Controls.Add(this.buttonMigrate);
            this.Controls.Add(this.groupboxInfo);
            this.Controls.Add(this.groupboxSize);
            this.Controls.Add(this.groupboxEncoding);
            this.Controls.Add(this.groupboxTimeStep);
            this.Controls.Add(this.groupboxHashAlgorithm);
            this.Controls.Add(this.checkBoxCustomSettings);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.textBoxKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "OtpInformation";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OtpInformation_FormClosing);
            this.Load += new System.EventHandler(this.OtpInformation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupboxHashAlgorithm.ResumeLayout(false);
            this.groupboxHashAlgorithm.PerformLayout();
            this.groupboxTimeStep.ResumeLayout(false);
            this.groupboxTimeStep.PerformLayout();
            this.groupboxEncoding.ResumeLayout(false);
            this.groupboxEncoding.PerformLayout();
            this.groupboxSize.ResumeLayout(false);
            this.groupboxSize.PerformLayout();
            this.groupboxInfo.ResumeLayout(false);
            this.groupboxInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMigrateQuestionmark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.RadioButton radioButtonSix;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonEight;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonBase32;
        private System.Windows.Forms.RadioButton radioButtonBase64;
        private System.Windows.Forms.RadioButton radioButtonHex;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.CheckBox checkBoxCustomSettings;
        private System.Windows.Forms.RadioButton radioButtonSha256;
        private System.Windows.Forms.RadioButton radioButtonSha512;
        private System.Windows.Forms.RadioButton radioButtonSha1;
        private System.Windows.Forms.GroupBox groupboxHashAlgorithm;
        private System.Windows.Forms.GroupBox groupboxTimeStep;
        private System.Windows.Forms.GroupBox groupboxEncoding;
        private System.Windows.Forms.GroupBox groupboxSize;
        private System.Windows.Forms.GroupBox groupboxInfo;
        private System.Windows.Forms.RadioButton radioButtonUtf8;
        private System.Windows.Forms.CheckBox checkboxOldKeeOtp;
        private System.Windows.Forms.Button buttonMigrate;
        private System.Windows.Forms.PictureBox pictureBoxMigrateQuestionmark;
    }
}