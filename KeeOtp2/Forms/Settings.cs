using KeeOtp2.Properties;
using KeePass.Forms;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace KeeOtp2
{
    public partial class Settings : Form
    {
        private IPluginHost host;
        private readonly BackgroundWorker backgroundWorkerMigrate;

        private bool removeAfterMigration;
        private bool migrateAutoType;
        private MigrateMode migrateMode;

        public enum MigrateMode
        {
            KeeOtp1ToBuiltIn,
            BuiltInToKeeOtp1
        }

        private Dictionary<MigrateMode, string> migrateModeString = new Dictionary<MigrateMode, string>()
        {
            { MigrateMode.KeeOtp1ToBuiltIn, "KeeOtp(1) -> Built-In" },
            { MigrateMode.BuiltInToKeeOtp1, "Built-In -> KeeOtp(1)" }
        };

        private Dictionary<MigrateMode, List<string>> migrateModePlaceholder = new Dictionary<MigrateMode, List<string>>()
        {
            { MigrateMode.KeeOtp1ToBuiltIn, new List<string>() { KeeOtp2Ext.KeeOtp1PlaceHolder, KeeOtp2Ext.BuiltInPlaceHolder } },
            { MigrateMode.BuiltInToKeeOtp1, new List<string>() { KeeOtp2Ext.BuiltInPlaceHolder, KeeOtp2Ext.KeeOtp1PlaceHolder } }
        };

        private Dictionary<int, MigrateMode> comboBoxMigrateIndexes = new Dictionary<int, MigrateMode>() { };

        public Settings(IPluginHost host)
        {
            InitializeComponent();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.settings_white,
                KeeOtp2Statics.Settings,
                KeeOtp2Statics.SettingsSubline);

            this.Icon = host.MainWindow.Icon;

            this.host = host;
            this.TopMost = host.MainWindow.TopMost;

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

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;

            foreach (MigrateMode migrateMode in Enum.GetValues(typeof(MigrateMode)))
            {
                if (migrateModeString.ContainsKey(migrateMode))
                    comboBoxMigrateIndexes.Add(comboBoxMigrate.Items.Add(migrateModeString[migrateMode]), migrateMode);
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
                comboBoxMigrateIndexes.ContainsKey(comboBoxMigrate.SelectedIndex))
            {
                this.migrateMode = comboBoxMigrateIndexes[comboBoxMigrate.SelectedIndex];
                if (migrateModeString.ContainsKey(migrateMode) && MessageBox.Show(String.Format("Do you really want to migrate?\n\nMigration: {0}", migrateModeString[migrateMode]), "Migrate", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.removeAfterMigration = ((MessageBox.Show("Do you want to remove the string fields after successful migration?", "Remove after migration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ? true : false);
                    if (migrateModePlaceholder.ContainsKey(migrateMode))
                    {
                        List<string> placeholder = migrateModePlaceholder[migrateMode];
                        this.migrateAutoType = ((MessageBox.Show(String.Format("Do you want to replace the Auto-Type key {0} with the key {1}?", placeholder.ElementAt(0), placeholder.ElementAt(1)), "Migrate Auto-Type", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ? true : false);
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
            if (textBoxHotKeySequence.Text != KeeOtp2Ext.BuiltInPlaceHolder)
                autoTypeConfig.DefaultSequence = textBoxHotKeySequence.Text;
            EditAutoTypeItemForm eatf = new EditAutoTypeItemForm();
            eatf.InitEx(autoTypeConfig, -1, true, KeeOtp2Ext.BuiltInPlaceHolder, null);
            eatf.ShowDialog();

            if (eatf.DialogResult == DialogResult.OK && autoTypeConfig.DefaultSequence != string.Empty)
                textBoxHotKeySequence.Text = autoTypeConfig.DefaultSequence;
            else
                textBoxHotKeySequence.Text = KeeOtp2Ext.BuiltInPlaceHolder;
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

            string oldPlaceholder = string.Empty;
            string newPlaceholder = string.Empty;

            if (this.migrateAutoType && migrateModePlaceholder.ContainsKey(migrateMode))
            {
                List<string> placeholder = migrateModePlaceholder[migrateMode];
                oldPlaceholder = placeholder.ElementAt(0);
                newPlaceholder = placeholder.ElementAt(1);
            }

            labelStatus.Text = String.Format(KeeOtp2Statics.SettingsLoadedNEntries, count);
            
            foreach (PwEntry entry in entries)
            {
                if (entry.ParentGroup.Uuid != RecycleBinUuid)
                {
                    if (checkEntryMigratable(entry, migrateMode))
                    {
                        OtpAuthData data = OtpAuthUtils.loadData(entry);
                        if (data != null) {
                            entry.CreateBackup(this.host.Database);
                            switch (migrateMode)
                            {
                                case MigrateMode.KeeOtp1ToBuiltIn:
                                    OtpAuthUtils.migrateToBuiltInOtp(data, entry);
                                    if (OtpAuthUtils.loadDataFromKeeOtp1String(entry) != null)
                                    {
                                        if (removeAfterMigration)
                                            OtpAuthUtils.purgeLoadedFields(data, entry);
                                        if (migrateAutoType && oldPlaceholder != string.Empty && newPlaceholder != string.Empty)
                                            OtpAuthUtils.replacePlaceholder(entry, oldPlaceholder, newPlaceholder);
                                    }
                                    break;
                                case MigrateMode.BuiltInToKeeOtp1:
                                    OtpAuthUtils.migrateToKeeOtp1String(data, entry);
                                    if (OtpAuthUtils.loadDataFromKeeOtp1String(entry) != null)
                                    {
                                        if (removeAfterMigration)
                                            OtpAuthUtils.purgeLoadedFields(data, entry);
                                        if (migrateAutoType && oldPlaceholder != string.Empty && newPlaceholder != string.Empty)
                                            OtpAuthUtils.replacePlaceholder(entry, oldPlaceholder, newPlaceholder);
                                    }
                                    break;
                                default:
                                    break;
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

        public static bool checkEntryMigratable(PwEntry entry, MigrateMode migrateMode)
        {
            switch (migrateMode)
            {
                case MigrateMode.KeeOtp1ToBuiltIn:
                    return OtpAuthUtils.checkKeeOtp1Mode(entry);
                case MigrateMode.BuiltInToKeeOtp1:
                    return OtpAuthUtils.checkBuiltInMode(entry);
                default:
                    return false;
            }
        }
    }
}
