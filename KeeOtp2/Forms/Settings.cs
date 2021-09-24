using KeeOtp2.Properties;
using KeePass.Forms;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KeeOtp2
{
    public partial class Settings : Form
    {
        private IPluginHost host;
        private BackgroundWorker backgroundWorkerMigrate;

        private bool removeAfterMigration;
        private bool migrateAutoType;
        private MigrationProfile currentMigrationProfile;
        private bool encounteredForcedKeeOtp1Entries;

        private Dictionary<MigrationMode, string> migrateModeString = new Dictionary<MigrationMode, string>()
        {
            { MigrationMode.KeeOtp1ToBuiltIn, "KeeOtp(1) -> Built-In" },
            { MigrationMode.BuiltInToKeeOtp1, "Built-In -> KeeOtp(1)" }
        };

        private Dictionary<MigrationMode, Dictionary<string, Dictionary<OtpType, string>>> migrateModePlaceholder = new Dictionary<MigrationMode, Dictionary<string, Dictionary<OtpType, string>>>()
        {
            { MigrationMode.KeeOtp1ToBuiltIn, new Dictionary<string, Dictionary<OtpType, string>>() { { "find", new Dictionary<OtpType, string>() { { OtpType.Totp | OtpType.Hotp | OtpType.Steam, KeeOtp2Ext.KeeOtp1PlaceHolder } } }, { "replace", new Dictionary<OtpType, string>() { { OtpType.Totp | OtpType.Steam, KeeOtp2Ext.BuiltInTotpPlaceHolder }, { OtpType.Hotp, KeeOtp2Ext.BuiltInHotpPlaceHolder } } } } },
            { MigrationMode.BuiltInToKeeOtp1, new Dictionary<string, Dictionary<OtpType, string>>() { { "find", new Dictionary<OtpType, string>() { { OtpType.Totp, KeeOtp2Ext.BuiltInTotpPlaceHolder }, { OtpType.Hotp, KeeOtp2Ext.BuiltInHotpPlaceHolder } } }, { "replace", new Dictionary<OtpType, string>() { { OtpType.Totp | OtpType.Hotp | OtpType.Steam, KeeOtp2Ext.KeeOtp1PlaceHolder } } } } },
        };

        private Dictionary<int, MigrationProfile> comboBoxMigrationProfileIndexes = new Dictionary<int, MigrationProfile>() { };

        public Settings(IPluginHost host)
        {
            InitializeComponent();

            this.host = host;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Point location = this.Owner.Location;
            location.Offset(20, 20);
            this.Location = location;

            this.Icon = this.host.MainWindow.Icon;
            this.TopMost = this.host.MainWindow.TopMost;

            PluginUtils.CheckKeeTheme(this);
        }

        public void InitEx()
        {
            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.settings_white,
                KeeOtp2Statics.Settings,
                KeeOtp2Statics.SettingsSubline);

            long timeInSeconds = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            this.numericUpDownFixedTimeOffset.Maximum = timeInSeconds;
            this.numericUpDownFixedTimeOffset.Minimum = -timeInSeconds;

            this.backgroundWorkerMigrate = new BackgroundWorker();
            this.backgroundWorkerMigrate.DoWork += backgroundWorkerMigrate_DoWork;
            this.backgroundWorkerMigrate.RunWorkerCompleted += backgroundWorkerMigrate_RunWorkerCompleted;

            groupBoxMigration.Text = KeeOtp2Statics.Migration;
            labelMigrateButton.Text = KeeOtp2Statics.OtpInformationMigrate + KeeOtp2Statics.InformationChar + KeeOtp2Statics.SelectorChar;
            buttonMigrate.Text = KeeOtp2Statics.OK;
            groupBoxHotkey.Text = KeeOtp2Statics.HotKey;
            labelUseHotKey.Text = KeeOtp2Statics.SettingsTOTPGlobalAutoType + KeeOtp2Statics.SelectorChar;
            checkBoxUseHotkey.Text = KeeOtp2Statics.SettingsUseGlobalHotkey;
            labelHotKeySequence.Text = KeeOtp2Statics.SettingsHotKeySequence + KeeOtp2Statics.InformationChar + KeeOtp2Statics.SelectorChar;
            labelGlobalHotkey.Text = KeeOtp2Statics.SettingsGlobalHotKey;
            groupBoxTime.Text = KeeOtp2Statics.GlobalTime + KeeOtp2Statics.InformationChar + KeeOtp2Statics.SelectorChar;
            radioButtonSystemTime.Text = KeeOtp2Statics.SettingsUseSystemTime;
            labelTime.Text = String.Format(KeeOtp2Statics.SettingsPreviewUtc, "00:00:00");
            radioButtonFixedTimeOffset.Text = KeeOtp2Statics.SettingsFixedTimeOffset;
            radioButtonCustomNtpServer.Text = KeeOtp2Statics.SettingsCustomNtpServer;
            buttonCustomNTPServerOK.Text = KeeOtp2Statics.OK;
            labelOverrideBuiltInTime.Text = KeeOtp2Statics.SettingsOverrideBuiltIn + KeeOtp2Statics.InformationChar + KeeOtp2Statics.SelectorChar;
            checkBoxOverrideBuiltInTime.Text = KeeOtp2Statics.SettingsOverrideBuiltIn;
            labelStatus.Text = KeeOtp2Statics.HoverInformation;
            buttonOK.Text = KeeOtp2Statics.OK;
            buttonCancel.Text = KeeOtp2Statics.Cancel;

            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = KeeOtp2Statics.Settings;
            toolTip.IsBalloon = true;

            string toolTipMigration = KeeOtp2Statics.ToolTipMigrate;
            toolTip.SetToolTip(labelMigrateButton, toolTipMigration);
            toolTip.SetToolTip(comboBoxMigrate, toolTipMigration);
            toolTip.SetToolTip(buttonMigrate, toolTipMigration);

            string toolTipHotKeySequence = KeeOtp2Statics.ToolTipHotKeySequence;
            toolTip.SetToolTip(labelHotKeySequence, toolTipHotKeySequence);
            toolTip.SetToolTip(textBoxHotKeySequence, toolTipHotKeySequence);

            string toolTipHotKeyCombination = KeeOtp2Statics.ToolTipHotKeyCombination;
            toolTip.SetToolTip(labelGlobalHotkey, toolTipHotKeyCombination);
            toolTip.SetToolTip(hotKeyControlExGlobalHotkey, toolTipHotKeyCombination);

            string toolTipOverrideBuiltInTime = KeeOtp2Statics.ToolTipOverrideBuiltInTime;
            toolTip.SetToolTip(labelOverrideBuiltInTime, toolTipOverrideBuiltInTime);
            toolTip.SetToolTip(checkBoxOverrideBuiltInTime, toolTipOverrideBuiltInTime);

            foreach (MigrationMode migrationMode in Enum.GetValues(typeof(MigrationMode)))
            {
                MigrationProfile migrationProfile = new MigrationProfile(migrateModeString[migrationMode], migrationMode, migrateModePlaceholder[migrationMode]["find"], migrateModePlaceholder[migrationMode]["replace"]);
                comboBoxMigrationProfileIndexes.Add(comboBoxMigrate.Items.Add(migrationProfile.name), migrationProfile);
            }
            if (comboBoxMigrate.Items.Count > 0)
                comboBoxMigrate.SelectedIndex = 0;

            loadConfig();

            timerClock.Start();
        }

        private void loadConfig()
        {
            textBoxHotKeySequence.Text = KeeOtp2Config.HotKeySequence;
            Keys hotKey = KeeOtp2Config.HotKeyKeys;
            hotKeyControlExGlobalHotkey.HotKey = hotKey;
            if (KeeOtp2Config.UseHotKey && hotKey != Keys.None)
            {
                checkBoxUseHotkey.Checked = true;
                textBoxHotKeySequence.Enabled = true;
                hotKeyControlExGlobalHotkey.Enabled = true;
            }
            else
            {
                checkBoxUseHotkey.Checked = false;
                textBoxHotKeySequence.Enabled = false;
                hotKeyControlExGlobalHotkey.Enabled = false;
            }

            radioButtonSystemTime.Checked =
                radioButtonFixedTimeOffset.Checked =
                numericUpDownFixedTimeOffset.Enabled =
                radioButtonCustomNtpServer.Checked =
                textBoxCustomNTPServerAddress.Enabled = false;
            switch (OtpTime.getTimeType())
            {
                case OtpTimeType.SystemTime:
                    radioButtonSystemTime.Checked = true;
                    break;
                case OtpTimeType.FixedOffset:
                    radioButtonFixedTimeOffset.Checked = true;
                    numericUpDownFixedTimeOffset.Enabled = true;
                    break;
                case OtpTimeType.CustomNtpServer:
                    radioButtonCustomNtpServer.Checked = true;
                    textBoxCustomNTPServerAddress.Enabled = true;
                    OtpTime.pollCustomNtpServer();
                    break;
                default:
                    radioButtonSystemTime.Checked = true;
                    break;
            }
            numericUpDownFixedTimeOffset.Value = OtpTime.getFixedTimeOffset();
            textBoxCustomNTPServerAddress.Text = OtpTime.getCustomNtpServer();
            checkBoxOverrideBuiltInTime.Checked = OtpTime.getOverrideBuiltInTime();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorkerMigrate.IsBusy)
                this.backgroundWorkerMigrate.CancelAsync();

            if (this.DialogResult == DialogResult.OK)
            {
                KeeOtp2Config.UseHotKey = checkBoxUseHotkey.Checked;
                KeeOtp2Config.HotKeySequence = textBoxHotKeySequence.Text;
                KeeOtp2Config.HotKeyKeys = hotKeyControlExGlobalHotkey.HotKey;

                if (radioButtonSystemTime.Checked)
                    KeeOtp2Config.TimeType = OtpTimeType.SystemTime;
                else if (radioButtonFixedTimeOffset.Checked)
                {
                    KeeOtp2Config.TimeType = OtpTimeType.FixedOffset;
                    KeeOtp2Config.FixedTimeOffset = (long)numericUpDownFixedTimeOffset.Value;
                    KeeOtp2Config.OverrideBuiltInTime = checkBoxOverrideBuiltInTime.Checked;
                }
                else if (radioButtonCustomNtpServer.Checked)
                {
                    KeeOtp2Config.TimeType = OtpTimeType.CustomNtpServer;
                    KeeOtp2Config.CustomNTPServer = textBoxCustomNTPServerAddress.Text;
                    KeeOtp2Config.OverrideBuiltInTime = checkBoxOverrideBuiltInTime.Checked;
                }
            }
        }

        // Interaction events

        private void buttonMigrate_Click(object sender, EventArgs e)
        {
            if (comboBoxMigrate.SelectedIndex > -1 &&
                comboBoxMigrationProfileIndexes.ContainsKey(comboBoxMigrate.SelectedIndex))
            {
                this.currentMigrationProfile = comboBoxMigrationProfileIndexes[comboBoxMigrate.SelectedIndex];
                if (!string.IsNullOrEmpty(currentMigrationProfile.name) && MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxMigrationConfirmation, currentMigrationProfile.name), KeeOtp2Statics.Migration, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.removeAfterMigration = ((MessageBox.Show(KeeOtp2Statics.MessageBoxMigrationRemoveStringAfterMigration, KeeOtp2Statics.Migration, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ? true : false);
                    if (currentMigrationProfile.findPlaceholder.Count > 0 && currentMigrationProfile.replacePlaceholder.Count > 0)
                    {
                        this.migrateAutoType = ((MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxMigrationReplacePlaceholder, string.Join("/", currentMigrationProfile.findPlaceholder.Values), string.Join("/", currentMigrationProfile.replacePlaceholder.Values)), KeeOtp2Statics.Migration, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ? true : false);
                    }
                    else
                        this.migrateAutoType = false;

                    this.buttonOK.Enabled = false;

                    this.backgroundWorkerMigrate.RunWorkerAsync();
                }
            }
        }

        private void textBoxHotKeySequence_Click(object sender, EventArgs e)
        {
            AutoTypeConfig autoTypeConfig = new AutoTypeConfig();
            if (textBoxHotKeySequence.Text != KeeOtp2Ext.BuiltInTotpPlaceHolder)
                autoTypeConfig.DefaultSequence = textBoxHotKeySequence.Text;
            EditAutoTypeItemForm eatf = new EditAutoTypeItemForm();
            eatf.InitEx(autoTypeConfig, -1, true, KeeOtp2Ext.BuiltInTotpPlaceHolder, null);
            eatf.ShowDialog();

            if (eatf.DialogResult == DialogResult.OK && autoTypeConfig.DefaultSequence != string.Empty)
                textBoxHotKeySequence.Text = autoTypeConfig.DefaultSequence;
            else
                textBoxHotKeySequence.Text = KeeOtp2Ext.BuiltInTotpPlaceHolder;
        }

        private void checkBoxUseHotkey_CheckedChanged(object sender, EventArgs e)
        {
            hotKeyControlExGlobalHotkey.Enabled =
                textBoxHotKeySequence.Enabled = checkBoxUseHotkey.Checked;
        }

        private void radioButtonsTime_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFixedTimeOffset.Checked)
                numericUpDownFixedTimeOffset.Enabled = true;
            else
                numericUpDownFixedTimeOffset.Enabled = false;

            if (radioButtonCustomNtpServer.Checked)
                textBoxCustomNTPServerAddress.Enabled = true;
            else
                textBoxCustomNTPServerAddress.Enabled = false;

            if (radioButtonFixedTimeOffset.Checked || radioButtonCustomNtpServer.Checked)
                checkBoxOverrideBuiltInTime.Enabled = true;
            else
                checkBoxOverrideBuiltInTime.Enabled = false;
        }

        private void buttonCustomNTPServerOK_Click(object sender, EventArgs e)
        {
            textBoxCustomNTPServerAddress.Enabled = false;
            OtpTime.pollCustomNtpServer(textBoxCustomNTPServerAddress.Text);
            textBoxCustomNTPServerAddress.Enabled = true;
        }

        private void backgroundWorkerMigrate_DoWork(object sender, DoWorkEventArgs e)
        {
            PwUuid RecycleBinUuid = this.host.Database.RecycleBinUuid;

            List<PwEntry> entries = new List<PwEntry>();
            entries = this.host.MainWindow.ActiveDatabase.RootGroup.GetEntries(true).ToList();

            int count = entries.Count;
            int counter = 0;
            int succeeded = 0;

            labelStatus.Text = String.Format(KeeOtp2Statics.SettingsLoadedNEntries, count);
            
            foreach (PwEntry entry in entries)
            {
                if (entry.ParentGroup.Uuid != RecycleBinUuid)
                {
                    if (OtpAuthUtils.checkEntryMigratable(entry, currentMigrationProfile.migrationMode))
                    {
                        OtpAuthData data = OtpAuthUtils.loadData(entry);
                        if (data != null) {
                            entry.CreateBackup(this.host.Database);
                            switch (currentMigrationProfile.migrationMode)
                            {
                                case MigrationMode.KeeOtp1ToBuiltIn:
                                    if (!data.isForcedKeeOtp1Mode())
                                    {
                                        OtpAuthUtils.migrateToBuiltInOtp(data, entry);
                                        if (OtpAuthUtils.loadDataFromKeeOtp1String(entry) != null)
                                        {
                                            if (removeAfterMigration)
                                                OtpAuthUtils.purgeLoadedFields(data, entry);
                                        }
                                    }
                                    else
                                        encounteredForcedKeeOtp1Entries = true;
                                    break;
                                case MigrationMode.BuiltInToKeeOtp1:
                                    OtpAuthUtils.migrateToKeeOtp1String(data, entry);
                                    if (OtpAuthUtils.loadDataFromKeeOtp1String(entry) != null)
                                    {
                                        if (removeAfterMigration)
                                            OtpAuthUtils.purgeLoadedFields(data, entry);
                                    }
                                    break;
                                default:
                                    break;
                            }
                                
                            if (migrateAutoType)
                            {
                                foreach(string currentPlaceholder in currentMigrationProfile.findPlaceholder.Values)
                                {
                                    OtpAuthUtils.replacePlaceholder(entry, currentPlaceholder, currentMigrationProfile.replacePlaceholder[data.Type]);
                                }
                            }
                            entry.Touch(true);
                            succeeded++;
                        }
                        else
                        {
                            this.Invoke((Action)(() => 
                            {
                                if (MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxCantParseEntry, entry.ParentGroup.Name, entry.Strings.ReadSafe(PwDefs.TitleField), entry.Strings.ReadSafe(PwDefs.UserNameField)), KeeOtp2Statics.Migration, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                                    backgroundWorkerMigrate.CancelAsync();
                            }));
                        }
                    }
                }
                counter++;
                labelStatus.Text = String.Format(KeeOtp2Statics.SettingsDoneNofNEntries, counter, count);
            }

            if (succeeded > 0)
            {
                this.host.MainWindow.ActiveDatabase.Modified = true;
                this.host.MainWindow.UpdateUI(false, null, false, null, false, null, true);
            }
        }
        private void backgroundWorkerMigrate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (encounteredForcedKeeOtp1Entries)
                MessageBox.Show(KeeOtp2Statics.MessageBoxMigrationForcedKeeOtp1Mode, KeeOtp2Statics.Migration, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.buttonOK.Enabled = true;
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            DateTime dateTime;
            if (radioButtonSystemTime.Checked)
                dateTime = OtpTime.getTime(OtpTimeType.SystemTime);
            else if (radioButtonFixedTimeOffset.Checked)
                dateTime = OtpTime.getTimeFixedOffset((long)numericUpDownFixedTimeOffset.Value);
            else if (radioButtonCustomNtpServer.Checked)
                dateTime = OtpTime.getTimeCustomNTPServer(true);
            else
                dateTime = OtpTime.getTime();
            labelTime.Text = String.Format(KeeOtp2Statics.SettingsPreviewUtc, dateTime.ToLongTimeString());
        }
    }
}
