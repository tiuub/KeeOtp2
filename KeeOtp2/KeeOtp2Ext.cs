using System;
using System.ComponentModel;
using System.Windows.Forms;
using KeePass.Plugins;
 using KeePass.Util;
using KeePass.Util.Spr;
using KeePassLib;
using KeePassLib.Utility;
using System.Runtime.InteropServices;
using NHotkey;

namespace KeeOtp2
{
    public sealed class KeeOtp2Ext : Plugin
    {
        private IPluginHost host = null;

        private ToolStripMenuItem otpMenuToolStripItem;
        private ToolStripMenuItem otpConfigureToolStripItem;
        private ToolStripMenuItem otpDialogToolStripItem;
        private ToolStripMenuItem otpCopyToolStripItem;

        private ToolStripMenuItem MainMenuToolStripItem;
        private ToolStripMenuItem SettingsToolStripItem;
        private ToolStripMenuItem AboutToolStripItem;

        public const string KeeOtp1PlaceHolder = "{TOTP}"; // Deprecated
        public const string BuiltInPlaceHolder = "{TIMEOTP}";

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            this.host = host;


            this.SettingsToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.Settings);
            this.SettingsToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Configuration];
            this.SettingsToolStripItem.Click += settingsToolStripitem_Click;

            this.AboutToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.About);
            this.AboutToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Info];
            this.AboutToolStripItem.Click += aboutToolStripitem_Click;

            this.MainMenuToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.PluginName);
            this.MainMenuToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.MainMenuToolStripItem.DropDownItems.Add(this.SettingsToolStripItem);
            this.MainMenuToolStripItem.DropDownItems.Add(this.AboutToolStripItem);
            host.MainWindow.ToolsMenu.DropDownItems.Add(this.MainMenuToolStripItem);

            this.otpConfigureToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuConfigure);
            this.otpConfigureToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.otpConfigureToolStripItem.Click += otpConfigureToolStripItem_Click;

            this.otpDialogToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuShowOtp);
            this.otpDialogToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.otpDialogToolStripItem.Click += otpDialogToolStripItem_Click;

            this.otpCopyToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuCopyOtp);
            this.otpCopyToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.otpCopyToolStripItem.Click += otpCopyToolStripItem_Click;            

            this.otpMenuToolStripItem = new ToolStripMenuItem(KeeOtp2Statics.PluginName);
            this.otpMenuToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.otpMenuToolStripItem.DropDownItems.Add(this.otpCopyToolStripItem);
            this.otpMenuToolStripItem.DropDownItems.Add(this.otpDialogToolStripItem);
            this.otpMenuToolStripItem.DropDownItems.Add(this.otpConfigureToolStripItem);


            host.MainWindow.EntryContextMenu.Items.Add(this.otpMenuToolStripItem);
            host.MainWindow.EntryContextMenu.Opening += entryContextMenu_Opening;


            SprEngine.FilterCompile += new EventHandler<SprEventArgs>(SprEngine_FilterCompile);
            SprEngine.FilterCompilePre += new EventHandler<SprEventArgs>(SprEngine_FilterCompilePre);

            KeeOtp2Config.handler = onHotKeyTriggered;
            KeeOtp2Config.loadConfig();

            return true; // Initialization successful
        }

        public override void Terminate()
        {
            KeeOtp2Config.unregisterHotKey();

            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(otpMenuToolStripItem);
            menu = host.MainWindow.ToolsMenu.DropDownItems;
            menu.Remove(MainMenuToolStripItem);
        }

        // If built-in {TIMEOTP} placeholder is used, but KeeOtp1 Save Mode is used
        void SprEngine_FilterCompilePre(object sender, SprEventArgs e)
        {
            if ((e.Context.Flags & SprCompileFlags.ExtActive) == SprCompileFlags.ExtActive)
            {
                if (e.Text.IndexOf(BuiltInPlaceHolder, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    PwEntry entry = e.Context.Entry;
                    if (!OtpAuthUtils.checkBuiltInMode(entry) || 
                        (OtpAuthUtils.checkEntry(entry) && OtpTime.getOverrideBuiltInTime() && (OtpTime.getTimeType() == OtpTimeType.FixedOffset || OtpTime.getTimeType() == OtpTimeType.CustomNtpServer)))
                    {
                        OtpAuthData data = OtpAuthUtils.loadData(entry);
                        if (data != null)
                        {
                            var text = OtpAuthUtils.getTotpString(data, OtpTime.getTime());

                            e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, BuiltInPlaceHolder, text);
                        }
                    }
                }
            }
        }

        void SprEngine_FilterCompile(object sender, SprEventArgs e)
        {
            if ((e.Context.Flags & SprCompileFlags.ExtActive) == SprCompileFlags.ExtActive)
            {
                if (e.Text.IndexOf(KeeOtp1PlaceHolder, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    PwEntry entry = e.Context.Entry;
                    OtpAuthData data = OtpAuthUtils.loadData(entry);
                    if (data != null)
                    {
                        var text = OtpAuthUtils.getTotpString(data, OtpTime.getTime());

                        e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, KeeOtp1PlaceHolder, text);
                    }
                }
            }
        }

        private void entryContextMenu_Opening(object sender, CancelEventArgs e)
        {
            PwEntry[] selectedEntries = this.host.MainWindow.GetSelectedEntries();
            bool selectedOne = selectedEntries != null && selectedEntries.Length == 1;
            if (selectedOne)
            {
                bool configured = OtpAuthUtils.checkEntry(selectedEntries[0]);
                this.otpCopyToolStripItem.Enabled = configured;
                this.otpDialogToolStripItem.Enabled = configured;
            }
            this.otpMenuToolStripItem.Enabled = selectedOne;
        }

        private void otpConfigureToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                OtpAuthData data = OtpAuthUtils.loadData(entry);
                OtpInformation addEditForm = new OtpInformation(data, entry, host);
                addEditForm.ShowDialog();
            }
        }

        private void otpDialogToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                ShowOneTimePasswords form = new ShowOneTimePasswords(entry, host);
                form.ShowDialog();
            }
        }

        private void otpCopyToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (this.GetSelectedSingleEntry(out entry))
            {
                OtpAuthData data = OtpAuthUtils.loadData(entry);
                if (data == null)
                {
                    if (MessageBox.Show(KeeOtp2Statics.MessageBoxOtpNotConfigured, KeeOtp2Statics.PluginName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ShowOneTimePasswords form = new ShowOneTimePasswords(entry, host);
                        form.ShowDialog();
                    }
                }
                else
                {
                    var text = OtpAuthUtils.getTotpString(data, OtpTime.getTime());

                    if (ClipboardUtil.CopyAndMinimize(new KeePassLib.Security.ProtectedString(true, text), true, this.host.MainWindow, entry, this.host.Database))
                        this.host.MainWindow.StartClipboardCountdown();
                }
            }
        }

        private void settingsToolStripitem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this.host);
            settings.ShowDialog();

            if (settings.DialogResult == DialogResult.OK)
            {
                KeeOtp2Config.handler = onHotKeyTriggered;
                KeeOtp2Config.loadConfig();
            }
        }

        private void aboutToolStripitem_Click(object sender, EventArgs e)
        {
            About about = new About(this.host);
            about.ShowDialog();
        }

        private bool GetSelectedSingleEntry(out PwEntry entry)
        {
            entry = null;

            var entries = this.host.MainWindow.GetSelectedEntries();
            if (entries == null || entries.Length == 0)
            {
                MessageBox.Show(KeeOtp2Statics.MessageBoxSelectedMultipleEntries, KeeOtp2Statics.Failure, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (entries.Length > 1)
            {
                MessageBox.Show(KeeOtp2Statics.MessageBoxSelectedMultipleEntries, KeeOtp2Statics.Failure, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                // grab the entry that we care about
                entry = entries[0];
                return true;
            }
        }

        private void onHotKeyTriggered(object sender, HotkeyEventArgs e)
        {

            _MethodInfo m_miAutoType = host.MainWindow.GetType().GetMethod("ExecuteGlobalAutoType", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
            if (m_miAutoType != null)
                m_miAutoType.Invoke(this.host.MainWindow, new object[] { KeeOtp2Config.HotKeySequence });
        }

        public override string UpdateUrl
        {
            get { return KeeOtp2Statics.PluginUpdateUrl; }
        }
    }
}
