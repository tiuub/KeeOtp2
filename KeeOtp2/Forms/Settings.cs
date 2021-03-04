﻿using KeePass.Forms;
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

        private enum MigrateMode
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
                Resources.clock.GetThumbnailImage(32, 32, null, IntPtr.Zero),
                "Settings",
                "Configure the KeeOtp2 plugin.");

            this.Icon = host.MainWindow.Icon;

            this.host = host;
            this.TopMost = host.MainWindow.TopMost;

            this.backgroundWorkerMigrate = new BackgroundWorker();
            this.backgroundWorkerMigrate.DoWork += backgroundWorkerMigrate_DoWork;
            this.backgroundWorkerMigrate.RunWorkerCompleted += backgroundWorkerMigrate_RunWorkerCompleted;
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
            }
        }

        private bool checkEntryMigratable(PwEntry entry, MigrateMode migrateMode)
        {
            if (migrateMode == MigrateMode.KeeOtp1ToBuiltIn)
                return !OtpAuthUtils.checkBuiltInMode(entry);
            else if (migrateMode == MigrateMode.BuiltInToKeeOtp1)
                return !OtpAuthUtils.checkKeeOtp1Mode(entry);
            return false;
        }

        private void backgroundWorkerMigrate_DoWork(object sender, DoWorkEventArgs e)
        {
            PwUuid RecycleBinUuid = this.host.Database.RecycleBinUuid;

            List<PwEntry> entries = new List<PwEntry>();
            entries = this.host.MainWindow.ActiveDatabase.RootGroup.GetEntries(true).ToList();

            int count = entries.Count;
            int counter = 0;
            int succeeded = 0;

            labelMigrationStatus.Text = String.Format("Loaded {0} entrie(s)!", count);
            
            foreach (PwEntry entry in entries)
            {
                if (entry.ParentGroup.Uuid != RecycleBinUuid)
                {
                    if (checkEntryMigratable(entry, migrateMode))
                    {
                        if (this.migrateAutoType && migrateModePlaceholder.ContainsKey(migrateMode))
                        {
                            List<string> placeholder = migrateModePlaceholder[migrateMode];
                            entry.AutoType.DefaultSequence = entry.AutoType.DefaultSequence.Replace(placeholder.ElementAt(0), placeholder.ElementAt(1));
                            foreach (AutoTypeAssociation ata in entry.AutoType.Associations)
                            {
                                ata.Sequence = ata.Sequence.Replace(placeholder.ElementAt(0), placeholder.ElementAt(1));
                            }
                        }

                        OtpAuthData data = OtpAuthUtils.loadData(entry);
                        if (data != null) {
                            entry.CreateBackup(this.host.Database);
                            if (migrateMode == MigrateMode.KeeOtp1ToBuiltIn)
                            {
                                OtpAuthUtils.migrateToBuiltInOtp(data, entry);
                                if (removeAfterMigration && OtpAuthUtils.loadDataFromBuiltInOtp(entry) != null)
                                    OtpAuthUtils.purgeLoadedFields(data, entry);
                            }
                            else if (migrateMode == MigrateMode.BuiltInToKeeOtp1)
                            {
                                OtpAuthUtils.migrateToKeeOtp1String(data, entry);
                                if (removeAfterMigration && OtpAuthUtils.loadDataFromKeeOtp1String(entry) != null)
                                    OtpAuthUtils.purgeLoadedFields(data, entry);
                            }
                                
                            entry.Touch(true);
                            succeeded++;
                        }
                        else
                        {
                            MessageBox.Show(String.Format("Cant migrate \"{0}\" - \"{1}\"\n(Username: {2})\n\nJust check the format of the \"key\" string.", entry.ParentGroup.Name, entry.Strings.ReadSafe(PwDefs.TitleField), entry.Strings.ReadSafe(PwDefs.UserNameField)), "Migration Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                counter++;
                labelMigrationStatus.Text = String.Format("Done {0} of {1} entries!", counter, count);
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
                    this.labelMigrationStatus.Enabled = true;
                    this.labelMigrationStatus.Visible = true;

                    this.backgroundWorkerMigrate.RunWorkerAsync();
                }
            }
        }

        private void textBoxHotKeySequence_Click(object sender, EventArgs e)
        {
            AutoTypeConfig autoTypeConfig = new AutoTypeConfig();
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
    }
}