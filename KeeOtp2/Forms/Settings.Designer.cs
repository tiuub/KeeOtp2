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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.groupBoxMigration = new System.Windows.Forms.GroupBox();
            this.comboBoxMigrate = new System.Windows.Forms.ComboBox();
            this.buttonMigrate = new System.Windows.Forms.Button();
            this.labelMigrateButton = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxHotkey = new System.Windows.Forms.GroupBox();
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.textBoxHotKeySequence = new System.Windows.Forms.TextBox();
            this.labelHotKeySequence = new System.Windows.Forms.Label();
            this.labelUseHotKey = new System.Windows.Forms.Label();
            this.hotKeyControlExGlobalHotkey = new KeePass.UI.HotKeyControlEx();
            this.labelGlobalHotkey = new System.Windows.Forms.Label();
            this.labelBeforeScanQr = new System.Windows.Forms.Label();
            this.checkBoxUseHotkey = new System.Windows.Forms.CheckBox();
            this.checkBoxAskConfirmScanQr = new System.Windows.Forms.CheckBox();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.labelOverrideBuiltInTime = new System.Windows.Forms.Label();
            this.checkBoxOverrideBuiltInTime = new System.Windows.Forms.CheckBox();
            this.buttonCustomNTPServerOK = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.numericUpDownFixedTimeOffset = new System.Windows.Forms.NumericUpDown();
            this.textBoxCustomNTPServerAddress = new System.Windows.Forms.TextBox();
            this.radioButtonCustomNtpServer = new System.Windows.Forms.RadioButton();
            this.radioButtonFixedTimeOffset = new System.Windows.Forms.RadioButton();
            this.radioButtonSystemTime = new System.Windows.Forms.RadioButton();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.groupBoxContextMenu = new System.Windows.Forms.GroupBox();
            this.labelShowContextMenuItem = new System.Windows.Forms.Label();
            this.checkBoxShowContextMenuItem = new System.Windows.Forms.CheckBox();
            this.labelUseLocalHotkey = new System.Windows.Forms.Label();
            this.hotKeyControlExLocalHotkey = new KeePass.UI.HotKeyControlEx();
            this.labelLocalHotkey = new System.Windows.Forms.Label();
            this.checkBoxUseLocalHotkey = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupBoxMigration.SuspendLayout();
            this.groupBoxHotkey.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.groupBoxTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFixedTimeOffset)).BeginInit();
            this.groupBoxContextMenu.SuspendLayout();
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
            this.groupBoxMigration.Controls.Add(this.buttonMigrate);
            this.groupBoxMigration.Controls.Add(this.labelMigrateButton);
            this.groupBoxMigration.Location = new System.Drawing.Point(12, 56);
            this.groupBoxMigration.Name = "groupBoxMigration";
            this.groupBoxMigration.Size = new System.Drawing.Size(303, 48);
            this.groupBoxMigration.TabIndex = 1;
            this.groupBoxMigration.TabStop = false;
            this.groupBoxMigration.Text = "Migration";
            // 
            // comboBoxMigrate
            // 
            this.comboBoxMigrate.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.comboBoxMigrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMigrate.FormattingEnabled = true;
            this.comboBoxMigrate.Location = new System.Drawing.Point(147, 16);
            this.comboBoxMigrate.Name = "comboBoxMigrate";
            this.comboBoxMigrate.Size = new System.Drawing.Size(107, 21);
            this.comboBoxMigrate.TabIndex = 8;
            // 
            // buttonMigrate
            // 
            this.buttonMigrate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMigrate.Location = new System.Drawing.Point(260, 15);
            this.buttonMigrate.Name = "buttonMigrate";
            this.buttonMigrate.Size = new System.Drawing.Size(37, 23);
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
            this.buttonOK.Location = new System.Drawing.Point(159, 445);
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
            this.buttonCancel.Location = new System.Drawing.Point(240, 445);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Enabled = false;
            this.labelStatus.Location = new System.Drawing.Point(9, 450);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(141, 13);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "(*Hover for more information)";
            // 
            // groupBoxHotkey
            // 
            this.groupBoxHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHotkey.Controls.Add(this.textBoxHotKeySequence);
            this.groupBoxHotkey.Controls.Add(this.labelHotKeySequence);
            this.groupBoxHotkey.Controls.Add(this.labelUseHotKey);
            this.groupBoxHotkey.Controls.Add(this.hotKeyControlExGlobalHotkey);
            this.groupBoxHotkey.Controls.Add(this.labelGlobalHotkey);
            this.groupBoxHotkey.Controls.Add(this.checkBoxUseHotkey);
            this.groupBoxHotkey.Location = new System.Drawing.Point(12, 110);
            this.groupBoxHotkey.Name = "groupBoxHotkey";
            this.groupBoxHotkey.Size = new System.Drawing.Size(303, 99);
            this.groupBoxHotkey.TabIndex = 7;
            this.groupBoxHotkey.TabStop = false;
            this.groupBoxHotkey.Text = "HotKey";
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOther.Controls.Add(this.labelBeforeScanQr);
            this.groupBoxOther.Controls.Add(this.checkBoxAskConfirmScanQr);
            this.groupBoxOther.Location = new System.Drawing.Point(12, 445);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(303, 45);
            this.groupBoxOther.TabIndex = 10;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Other";
            // 
            // textBoxHotKeySequence
            // 
            this.textBoxHotKeySequence.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.textBoxHotKeySequence.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxHotKeySequence.Location = new System.Drawing.Point(147, 42);
            this.textBoxHotKeySequence.Name = "textBoxHotKeySequence";
            this.textBoxHotKeySequence.ReadOnly = true;
            this.textBoxHotKeySequence.Size = new System.Drawing.Size(150, 20);
            this.textBoxHotKeySequence.TabIndex = 6;
            this.textBoxHotKeySequence.Click += new System.EventHandler(this.textBoxHotKeySequence_Click);
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
            this.checkBoxUseHotkey.Size = new System.Drawing.Size(128, 17);
            this.checkBoxUseHotkey.TabIndex = 1;
            this.checkBoxUseHotkey.Text = "Use global Hotkey";
            this.checkBoxUseHotkey.UseVisualStyleBackColor = true;
            this.checkBoxUseHotkey.CheckedChanged += new System.EventHandler(this.checkBoxUseHotkey_CheckedChanged);
            // 
            // labelBeforeScanQr
            // 
            this.labelBeforeScanQr.AutoSize = true;
            this.labelBeforeScanQr.Location = new System.Drawing.Point(6, 20);
            this.labelBeforeScanQr.Name = "labelBeforeScanQr";
            this.labelBeforeScanQr.Size = new System.Drawing.Size(117, 13);
            this.labelBeforeScanQr.TabIndex = 2;
            this.labelBeforeScanQr.Text = "Before scanning the QR code:";
            // 
            // checkBoxAskConfirmScan
            // 
            this.checkBoxAskConfirmScanQr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAskConfirmScanQr.AutoSize = true;
            this.checkBoxAskConfirmScanQr.Location = new System.Drawing.Point(147, 19);
            this.checkBoxAskConfirmScanQr.Name = "checkBoxAskConfirmScan";
            this.checkBoxAskConfirmScanQr.Size = new System.Drawing.Size(153, 17);
            this.checkBoxAskConfirmScanQr.TabIndex = 1;
            this.checkBoxAskConfirmScanQr.Text = "Display confirmation prompt";
            this.checkBoxAskConfirmScanQr.UseVisualStyleBackColor = true;
            this.checkBoxAskConfirmScanQr.CheckedChanged += new System.EventHandler(this.checkBoxUseHotkey_CheckedChanged);
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTime.Controls.Add(this.labelOverrideBuiltInTime);
            this.groupBoxTime.Controls.Add(this.checkBoxOverrideBuiltInTime);
            this.groupBoxTime.Controls.Add(this.buttonCustomNTPServerOK);
            this.groupBoxTime.Controls.Add(this.labelTime);
            this.groupBoxTime.Controls.Add(this.numericUpDownFixedTimeOffset);
            this.groupBoxTime.Controls.Add(this.textBoxCustomNTPServerAddress);
            this.groupBoxTime.Controls.Add(this.radioButtonCustomNtpServer);
            this.groupBoxTime.Controls.Add(this.radioButtonFixedTimeOffset);
            this.groupBoxTime.Controls.Add(this.radioButtonSystemTime);
            this.groupBoxTime.Location = new System.Drawing.Point(12, 319);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Size = new System.Drawing.Size(303, 120);
            this.groupBoxTime.TabIndex = 8;
            this.groupBoxTime.TabStop = false;
            this.groupBoxTime.Text = "Global Time";
            // 
            // labelOverrideBuiltInTime
            // 
            this.labelOverrideBuiltInTime.AutoSize = true;
            this.labelOverrideBuiltInTime.Location = new System.Drawing.Point(6, 97);
            this.labelOverrideBuiltInTime.Name = "labelOverrideBuiltInTime";
            this.labelOverrideBuiltInTime.Size = new System.Drawing.Size(109, 13);
            this.labelOverrideBuiltInTime.TabIndex = 11;
            this.labelOverrideBuiltInTime.Text = "Override built-in time*:";
            // 
            // checkBoxOverrideBuiltInTime
            // 
            this.checkBoxOverrideBuiltInTime.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.checkBoxOverrideBuiltInTime.AutoSize = true;
            this.checkBoxOverrideBuiltInTime.Location = new System.Drawing.Point(147, 96);
            this.checkBoxOverrideBuiltInTime.Name = "checkBoxOverrideBuiltInTime";
            this.checkBoxOverrideBuiltInTime.Size = new System.Drawing.Size(155, 17);
            this.checkBoxOverrideBuiltInTime.TabIndex = 9;
            this.checkBoxOverrideBuiltInTime.Text = "Override built-in time";
            this.checkBoxOverrideBuiltInTime.UseVisualStyleBackColor = true;
            // 
            // buttonCustomNTPServerOK
            // 
            this.buttonCustomNTPServerOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCustomNTPServerOK.Location = new System.Drawing.Point(267, 68);
            this.buttonCustomNTPServerOK.Name = "buttonCustomNTPServerOK";
            this.buttonCustomNTPServerOK.Size = new System.Drawing.Size(30, 23);
            this.buttonCustomNTPServerOK.TabIndex = 9;
            this.buttonCustomNTPServerOK.Text = "OK";
            this.buttonCustomNTPServerOK.UseVisualStyleBackColor = true;
            this.buttonCustomNTPServerOK.Click += new System.EventHandler(this.buttonCustomNTPServerOK_Click);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(147, 21);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(119, 13);
            this.labelTime.TabIndex = 10;
            this.labelTime.Text = "Preview (Utc): 00:00:00";
            // 
            // numericUpDownFixedTimeOffset
            // 
            this.numericUpDownFixedTimeOffset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFixedTimeOffset.Location = new System.Drawing.Point(147, 44);
            this.numericUpDownFixedTimeOffset.Maximum = new decimal(new int[] {
            -1,
            2147483647,
            0,
            0});
            this.numericUpDownFixedTimeOffset.Minimum = new decimal(new int[] {
            0,
            -2147483648,
            0,
            -2147483648});
            this.numericUpDownFixedTimeOffset.Name = "numericUpDownFixedTimeOffset";
            this.numericUpDownFixedTimeOffset.Size = new System.Drawing.Size(150, 20);
            this.numericUpDownFixedTimeOffset.TabIndex = 9;
            // 
            // textBoxCustomNTPServerAddress
            // 
            this.textBoxCustomNTPServerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCustomNTPServerAddress.Location = new System.Drawing.Point(147, 70);
            this.textBoxCustomNTPServerAddress.Name = "textBoxCustomNTPServerAddress";
            this.textBoxCustomNTPServerAddress.Size = new System.Drawing.Size(114, 20);
            this.textBoxCustomNTPServerAddress.TabIndex = 3;
            // 
            // radioButtonCustomNtpServer
            // 
            this.radioButtonCustomNtpServer.AutoSize = true;
            this.radioButtonCustomNtpServer.Location = new System.Drawing.Point(9, 71);
            this.radioButtonCustomNtpServer.Name = "radioButtonCustomNtpServer";
            this.radioButtonCustomNtpServer.Size = new System.Drawing.Size(117, 17);
            this.radioButtonCustomNtpServer.TabIndex = 2;
            this.radioButtonCustomNtpServer.TabStop = true;
            this.radioButtonCustomNtpServer.Text = "Custom NTP server";
            this.radioButtonCustomNtpServer.UseVisualStyleBackColor = true;
            this.radioButtonCustomNtpServer.CheckedChanged += new System.EventHandler(this.radioButtonsTime_CheckedChanged);
            // 
            // radioButtonFixedTimeOffset
            // 
            this.radioButtonFixedTimeOffset.AutoSize = true;
            this.radioButtonFixedTimeOffset.Location = new System.Drawing.Point(9, 44);
            this.radioButtonFixedTimeOffset.Name = "radioButtonFixedTimeOffset";
            this.radioButtonFixedTimeOffset.Size = new System.Drawing.Size(130, 17);
            this.radioButtonFixedTimeOffset.TabIndex = 1;
            this.radioButtonFixedTimeOffset.TabStop = true;
            this.radioButtonFixedTimeOffset.Text = "Fixed time offset (sec.)";
            this.radioButtonFixedTimeOffset.UseVisualStyleBackColor = true;
            this.radioButtonFixedTimeOffset.CheckedChanged += new System.EventHandler(this.radioButtonsTime_CheckedChanged);
            // 
            // radioButtonSystemTime
            // 
            this.radioButtonSystemTime.AutoSize = true;
            this.radioButtonSystemTime.Location = new System.Drawing.Point(9, 19);
            this.radioButtonSystemTime.Name = "radioButtonSystemTime";
            this.radioButtonSystemTime.Size = new System.Drawing.Size(113, 17);
            this.radioButtonSystemTime.TabIndex = 0;
            this.radioButtonSystemTime.TabStop = true;
            this.radioButtonSystemTime.Text = "Use time of system";
            this.radioButtonSystemTime.UseVisualStyleBackColor = true;
            this.radioButtonSystemTime.CheckedChanged += new System.EventHandler(this.radioButtonsTime_CheckedChanged);
            // 
            // timerClock
            // 
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // groupBoxContextMenu
            // 
            this.groupBoxContextMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxContextMenu.Controls.Add(this.labelShowContextMenuItem);
            this.groupBoxContextMenu.Controls.Add(this.checkBoxShowContextMenuItem);
            this.groupBoxContextMenu.Controls.Add(this.labelUseLocalHotkey);
            this.groupBoxContextMenu.Controls.Add(this.hotKeyControlExLocalHotkey);
            this.groupBoxContextMenu.Controls.Add(this.labelLocalHotkey);
            this.groupBoxContextMenu.Controls.Add(this.checkBoxUseLocalHotkey);
            this.groupBoxContextMenu.Location = new System.Drawing.Point(12, 215);
            this.groupBoxContextMenu.Name = "groupBoxContextMenu";
            this.groupBoxContextMenu.Size = new System.Drawing.Size(303, 98);
            this.groupBoxContextMenu.TabIndex = 8;
            this.groupBoxContextMenu.TabStop = false;
            this.groupBoxContextMenu.Text = "Context Menu (changes apply after restart)";
            // 
            // labelShowContextMenuItem
            // 
            this.labelShowContextMenuItem.AutoSize = true;
            this.labelShowContextMenuItem.Location = new System.Drawing.Point(6, 20);
            this.labelShowContextMenuItem.Name = "labelShowContextMenuItem";
            this.labelShowContextMenuItem.Size = new System.Drawing.Size(111, 13);
            this.labelShowContextMenuItem.TabIndex = 6;
            this.labelShowContextMenuItem.Text = "Copy TOTP shortcut*:";
            // 
            // checkBoxShowContextMenuItem
            // 
            this.checkBoxShowContextMenuItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowContextMenuItem.AutoSize = true;
            this.checkBoxShowContextMenuItem.Location = new System.Drawing.Point(147, 19);
            this.checkBoxShowContextMenuItem.Name = "checkBoxShowContextMenuItem";
            this.checkBoxShowContextMenuItem.Size = new System.Drawing.Size(122, 17);
            this.checkBoxShowContextMenuItem.TabIndex = 5;
            this.checkBoxShowContextMenuItem.Text = "Show \"Copy TOTP\"";
            this.checkBoxShowContextMenuItem.UseVisualStyleBackColor = true;
            this.checkBoxShowContextMenuItem.CheckedChanged += new System.EventHandler(this.checkBoxShowCopyTotp_CheckedChanged);
            // 
            // labelUseLocalHotkey
            // 
            this.labelUseLocalHotkey.AutoSize = true;
            this.labelUseLocalHotkey.Location = new System.Drawing.Point(6, 45);
            this.labelUseLocalHotkey.Name = "labelUseLocalHotkey";
            this.labelUseLocalHotkey.Size = new System.Drawing.Size(101, 13);
            this.labelUseLocalHotkey.TabIndex = 4;
            this.labelUseLocalHotkey.Text = "TOTP to clipboard*:";
            // 
            // hotKeyControlExLocalHotkey
            // 
            this.hotKeyControlExLocalHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeyControlExLocalHotkey.Location = new System.Drawing.Point(147, 67);
            this.hotKeyControlExLocalHotkey.Name = "hotKeyControlExLocalHotkey";
            this.hotKeyControlExLocalHotkey.Size = new System.Drawing.Size(150, 20);
            this.hotKeyControlExLocalHotkey.TabIndex = 3;
            // 
            // labelLocalHotkey
            // 
            this.labelLocalHotkey.AutoSize = true;
            this.labelLocalHotkey.Location = new System.Drawing.Point(6, 70);
            this.labelLocalHotkey.Name = "labelLocalHotkey";
            this.labelLocalHotkey.Size = new System.Drawing.Size(78, 13);
            this.labelLocalHotkey.TabIndex = 2;
            this.labelLocalHotkey.Text = "Local HotKey*:";
            // 
            // checkBoxUseLocalHotkey
            // 
            this.checkBoxUseLocalHotkey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxUseLocalHotkey.AutoSize = true;
            this.checkBoxUseLocalHotkey.Location = new System.Drawing.Point(147, 44);
            this.checkBoxUseLocalHotkey.Name = "checkBoxUseLocalHotkey";
            this.checkBoxUseLocalHotkey.Size = new System.Drawing.Size(122, 17);
            this.checkBoxUseLocalHotkey.TabIndex = 1;
            this.checkBoxUseLocalHotkey.Text = "Use local Hotkey";
            this.checkBoxUseLocalHotkey.UseVisualStyleBackColor = true;
            this.checkBoxUseLocalHotkey.CheckedChanged += new System.EventHandler(this.checkBoxUseLocalHotkey_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 476);
            this.Controls.Add(this.groupBoxContextMenu);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.groupBoxHotkey);
            this.Controls.Add(this.groupBoxOther);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxMigration);
            this.Controls.Add(this.pictureBoxBanner);
            // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.CancelButton = this.buttonCancel;
            this.Name = "Settings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupBoxMigration.ResumeLayout(false);
            this.groupBoxMigration.PerformLayout();
            this.groupBoxHotkey.ResumeLayout(false);
            this.groupBoxHotkey.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFixedTimeOffset)).EndInit();
            this.groupBoxContextMenu.ResumeLayout(false);
            this.groupBoxContextMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.GroupBox groupBoxMigration;
        private System.Windows.Forms.Button buttonMigrate;
        private System.Windows.Forms.Label labelMigrateButton;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.GroupBox groupBoxHotkey;
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.Label labelUseHotKey;
        private KeePass.UI.HotKeyControlEx hotKeyControlExGlobalHotkey;
        private System.Windows.Forms.Label labelGlobalHotkey;
        private System.Windows.Forms.Label labelBeforeScanQr;
        private System.Windows.Forms.CheckBox checkBoxUseHotkey;
        private System.Windows.Forms.CheckBox checkBoxAskConfirmScanQr;
        private System.Windows.Forms.ComboBox comboBoxMigrate;
        private System.Windows.Forms.TextBox textBoxHotKeySequence;
        private System.Windows.Forms.Label labelHotKeySequence;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.RadioButton radioButtonSystemTime;
        private System.Windows.Forms.TextBox textBoxCustomNTPServerAddress;
        private System.Windows.Forms.RadioButton radioButtonCustomNtpServer;
        private System.Windows.Forms.RadioButton radioButtonFixedTimeOffset;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.NumericUpDown numericUpDownFixedTimeOffset;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Button buttonCustomNTPServerOK;
        private System.Windows.Forms.CheckBox checkBoxOverrideBuiltInTime;
        private System.Windows.Forms.Label labelOverrideBuiltInTime;
        private System.Windows.Forms.GroupBox groupBoxContextMenu;
        private System.Windows.Forms.Label labelShowContextMenuItem;
        private System.Windows.Forms.CheckBox checkBoxShowContextMenuItem;
        private System.Windows.Forms.Label labelUseLocalHotkey;
        private KeePass.UI.HotKeyControlEx hotKeyControlExLocalHotkey;
        private System.Windows.Forms.Label labelLocalHotkey;
        private System.Windows.Forms.CheckBox checkBoxUseLocalHotkey;
    }
}
