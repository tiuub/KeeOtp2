namespace KeeOtp2
{
    partial class Settings
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
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.groupBoxMigration = new System.Windows.Forms.GroupBox();
            this.comboBoxMigrate = new System.Windows.Forms.ComboBox();
            this.labelMigrateInfo = new System.Windows.Forms.Label();
            this.buttonMigrate = new System.Windows.Forms.Button();
            this.labelMigrateButton = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelMigrationStatus = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.groupBoxHotkey = new System.Windows.Forms.GroupBox();
            this.labelHotKeyInformation = new System.Windows.Forms.Label();
            this.labelUseHotKey = new System.Windows.Forms.Label();
            this.hotKeyControlExGlobalHotkey = new KeePass.UI.HotKeyControlEx();
            this.labelGlobalHotkey = new System.Windows.Forms.Label();
            this.checkBoxUseHotkey = new System.Windows.Forms.CheckBox();
            this.labelHotKeySequence = new System.Windows.Forms.Label();
            this.textBoxHotKeySequence = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupBoxMigration.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxHotkey.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(327, 50);
            this.pictureBoxBanner.TabIndex = 0;
            this.pictureBoxBanner.TabStop = false;
            // 
            // groupBoxMigration
            // 
            this.groupBoxMigration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMigration.Controls.Add(this.comboBoxMigrate);
            this.groupBoxMigration.Controls.Add(this.labelMigrateInfo);
            this.groupBoxMigration.Controls.Add(this.buttonMigrate);
            this.groupBoxMigration.Controls.Add(this.labelMigrateButton);
            this.groupBoxMigration.Location = new System.Drawing.Point(12, 56);
            this.groupBoxMigration.Name = "groupBoxMigration";
            this.groupBoxMigration.Size = new System.Drawing.Size(303, 90);
            this.groupBoxMigration.TabIndex = 1;
            this.groupBoxMigration.TabStop = false;
            this.groupBoxMigration.Text = "Migration";
            // 
            // comboBoxMigrate
            // 
            this.comboBoxMigrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrate.FormattingEnabled = true;
            this.comboBoxMigrate.Location = new System.Drawing.Point(147, 16);
            this.comboBoxMigrate.Name = "comboBoxMigrate";
            this.comboBoxMigrate.Size = new System.Drawing.Size(114, 21);
            this.comboBoxMigrate.TabIndex = 8;
            // 
            // labelMigrateInfo
            // 
            this.labelMigrateInfo.AutoSize = true;
            this.labelMigrateInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMigrateInfo.Location = new System.Drawing.Point(6, 41);
            this.labelMigrateInfo.Name = "labelMigrateInfo";
            this.labelMigrateInfo.Size = new System.Drawing.Size(267, 39);
            this.labelMigrateInfo.TabIndex = 2;
            this.labelMigrateInfo.Text = "(*Since KeePass 2.47, OTPs can generated by a built-in\r\nfunction. This makes it o" +
    "bsolete to keep your OTP keys\r\nsaved in KeeOtp(1) format. It is also recommended" +
    "!)";
            // 
            // buttonMigrate
            // 
            this.buttonMigrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMigrate.Location = new System.Drawing.Point(267, 15);
            this.buttonMigrate.Name = "buttonMigrate";
            this.buttonMigrate.Size = new System.Drawing.Size(30, 23);
            this.buttonMigrate.TabIndex = 3;
            this.buttonMigrate.Text = "OK";
            this.buttonMigrate.UseVisualStyleBackColor = true;
            this.buttonMigrate.Click += new System.EventHandler(this.buttonMigrate_Click);
            // 
            // labelMigrateButton
            // 
            this.labelMigrateButton.AutoSize = true;
            this.labelMigrateButton.Location = new System.Drawing.Point(6, 20);
            this.labelMigrateButton.Name = "labelMigrateButton";
            this.labelMigrateButton.Size = new System.Drawing.Size(113, 13);
            this.labelMigrateButton.TabIndex = 2;
            this.labelMigrateButton.Text = "Migrate every enty to*:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(240, 332);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(159, 332);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelMigrationStatus
            // 
            this.labelMigrationStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelMigrationStatus.AutoSize = true;
            this.labelMigrationStatus.Enabled = false;
            this.labelMigrationStatus.Location = new System.Drawing.Point(18, 337);
            this.labelMigrationStatus.Name = "labelMigrationStatus";
            this.labelMigrationStatus.Size = new System.Drawing.Size(89, 13);
            this.labelMigrationStatus.TabIndex = 5;
            this.labelMigrationStatus.Text = "Loaded 0 entries!";
            this.labelMigrationStatus.Visible = false;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.labelInfo);
            this.groupBoxInfo.Location = new System.Drawing.Point(12, 272);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(303, 54);
            this.groupBoxInfo.TabIndex = 6;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(6, 20);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(248, 26);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "The auto-type placeholder {TOTP} is deprecated.\r\nYou should rather use the {TIMEO" +
    "TP} placeholder.";
            // 
            // groupBoxHotkey
            // 
            this.groupBoxHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHotkey.Controls.Add(this.textBoxHotKeySequence);
            this.groupBoxHotkey.Controls.Add(this.labelHotKeySequence);
            this.groupBoxHotkey.Controls.Add(this.labelHotKeyInformation);
            this.groupBoxHotkey.Controls.Add(this.labelUseHotKey);
            this.groupBoxHotkey.Controls.Add(this.hotKeyControlExGlobalHotkey);
            this.groupBoxHotkey.Controls.Add(this.labelGlobalHotkey);
            this.groupBoxHotkey.Controls.Add(this.checkBoxUseHotkey);
            this.groupBoxHotkey.Location = new System.Drawing.Point(12, 152);
            this.groupBoxHotkey.Name = "groupBoxHotkey";
            this.groupBoxHotkey.Size = new System.Drawing.Size(303, 114);
            this.groupBoxHotkey.TabIndex = 7;
            this.groupBoxHotkey.TabStop = false;
            this.groupBoxHotkey.Text = "HotKey";
            // 
            // labelHotKeyInformation
            // 
            this.labelHotKeyInformation.AutoSize = true;
            this.labelHotKeyInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHotKeyInformation.Location = new System.Drawing.Point(6, 91);
            this.labelHotKeyInformation.Name = "labelHotKeyInformation";
            this.labelHotKeyInformation.Size = new System.Drawing.Size(249, 13);
            this.labelHotKeyInformation.TabIndex = 4;
            this.labelHotKeyInformation.Text = "(*Click inside the textbox to change the parameters.)";
            // 
            // labelUseHotKey
            // 
            this.labelUseHotKey.AutoSize = true;
            this.labelUseHotKey.Location = new System.Drawing.Point(6, 20);
            this.labelUseHotKey.Name = "labelUseHotKey";
            this.labelUseHotKey.Size = new System.Drawing.Size(117, 13);
            this.labelUseHotKey.TabIndex = 4;
            this.labelUseHotKey.Text = "TOTP global auto-type:";
            // 
            // hotKeyControlExGlobalHotkey
            // 
            this.hotKeyControlExGlobalHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeyControlExGlobalHotkey.Location = new System.Drawing.Point(147, 68);
            this.hotKeyControlExGlobalHotkey.Name = "hotKeyControlExGlobalHotkey";
            this.hotKeyControlExGlobalHotkey.Size = new System.Drawing.Size(150, 20);
            this.hotKeyControlExGlobalHotkey.TabIndex = 3;
            // 
            // labelGlobalHotkey
            // 
            this.labelGlobalHotkey.AutoSize = true;
            this.labelGlobalHotkey.Location = new System.Drawing.Point(6, 71);
            this.labelGlobalHotkey.Name = "labelGlobalHotkey";
            this.labelGlobalHotkey.Size = new System.Drawing.Size(82, 13);
            this.labelGlobalHotkey.TabIndex = 2;
            this.labelGlobalHotkey.Text = "Global HotKey*:";
            // 
            // checkBoxUseHotkey
            // 
            this.checkBoxUseHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseHotkey.AutoSize = true;
            this.checkBoxUseHotkey.Location = new System.Drawing.Point(147, 19);
            this.checkBoxUseHotkey.Name = "checkBoxUseHotkey";
            this.checkBoxUseHotkey.Size = new System.Drawing.Size(113, 17);
            this.checkBoxUseHotkey.TabIndex = 1;
            this.checkBoxUseHotkey.Text = "Use global Hotkey";
            this.checkBoxUseHotkey.UseVisualStyleBackColor = true;
            this.checkBoxUseHotkey.CheckedChanged += new System.EventHandler(this.checkBoxUseHotkey_CheckedChanged);
            // 
            // labelHotKeySequence
            // 
            this.labelHotKeySequence.AutoSize = true;
            this.labelHotKeySequence.Location = new System.Drawing.Point(6, 45);
            this.labelHotKeySequence.Name = "labelHotKeySequence";
            this.labelHotKeySequence.Size = new System.Drawing.Size(101, 13);
            this.labelHotKeySequence.TabIndex = 5;
            this.labelHotKeySequence.Text = "HotKey Sequence*:";
            // 
            // textBoxHotKeySequence
            // 
            this.textBoxHotKeySequence.Location = new System.Drawing.Point(147, 42);
            this.textBoxHotKeySequence.Name = "textBoxHotKeySequence";
            this.textBoxHotKeySequence.ReadOnly = true;
            this.textBoxHotKeySequence.Size = new System.Drawing.Size(150, 20);
            this.textBoxHotKeySequence.TabIndex = 6;
            this.textBoxHotKeySequence.Click += new System.EventHandler(this.textBoxHotKeySequence_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 363);
            this.Controls.Add(this.groupBoxHotkey);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.labelMigrationStatus);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxMigration);
            this.Controls.Add(this.pictureBoxBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupBoxMigration.ResumeLayout(false);
            this.groupBoxMigration.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxHotkey.ResumeLayout(false);
            this.groupBoxHotkey.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.GroupBox groupBoxMigration;
        private System.Windows.Forms.Label labelMigrateInfo;
        private System.Windows.Forms.Button buttonMigrate;
        private System.Windows.Forms.Label labelMigrateButton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelMigrationStatus;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.GroupBox groupBoxHotkey;
        private System.Windows.Forms.Label labelUseHotKey;
        private KeePass.UI.HotKeyControlEx hotKeyControlExGlobalHotkey;
        private System.Windows.Forms.Label labelGlobalHotkey;
        private System.Windows.Forms.CheckBox checkBoxUseHotkey;
        private System.Windows.Forms.Label labelHotKeyInformation;
        private System.Windows.Forms.ComboBox comboBoxMigrate;
        private System.Windows.Forms.TextBox textBoxHotKeySequence;
        private System.Windows.Forms.Label labelHotKeySequence;
    }
}