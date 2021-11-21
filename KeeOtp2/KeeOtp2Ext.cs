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
using KeePassLib.Security;
using KeePassLib.Native;

namespace KeeOtp2
{
    public sealed class KeeOtp2Ext : Plugin
    {
        private IPluginHost host = null;

        private ToolStripMenuItem EntryContextMenuMainItem;
        private ToolStripMenuItem EntryContextMenuCopyItem;
        private ToolStripMenuItem EntryContextMenuConfigureSubItem;
        private ToolStripMenuItem EntryContextMenuShowOtpSubItem;
        private ToolStripMenuItem EntryContextMenuCopySubItem;

        private ToolStripMenuItem ToolsMenuMainItem;
        private ToolStripMenuItem ToolsMenuSettingsSubMenuItem;
        private ToolStripMenuItem ToolsMenuAboutSubMenuItem;

        public const string KeeOtp1PlaceHolder = "{TOTP}"; // Deprecated
        public const string BuiltInTotpPlaceHolder = "{TIMEOTP}";
        public const string BuiltInHotpPlaceHolder = "{HMACOTP}";

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            this.host = host;


            this.ToolsMenuSettingsSubMenuItem = new ToolStripMenuItem(KeeOtp2Statics.Settings);
            this.ToolsMenuSettingsSubMenuItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Configuration];
            this.ToolsMenuSettingsSubMenuItem.Click += settingsToolStripitem_Click;

            this.ToolsMenuAboutSubMenuItem = new ToolStripMenuItem(KeeOtp2Statics.About);
            this.ToolsMenuAboutSubMenuItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Info];
            this.ToolsMenuAboutSubMenuItem.Click += aboutToolStripitem_Click;

            this.ToolsMenuMainItem = new ToolStripMenuItem(KeeOtp2Statics.PluginName);
            this.ToolsMenuMainItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.ToolsMenuMainItem.DropDownItems.Add(this.ToolsMenuSettingsSubMenuItem);
            this.ToolsMenuMainItem.DropDownItems.Add(this.ToolsMenuAboutSubMenuItem);
            host.MainWindow.ToolsMenu.DropDownItems.Add(this.ToolsMenuMainItem);

            this.EntryContextMenuConfigureSubItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuConfigure);
            this.EntryContextMenuConfigureSubItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.EntryContextMenuConfigureSubItem.Click += otpConfigureToolStripItem_Click;

            this.EntryContextMenuShowOtpSubItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuShowOtp);
            this.EntryContextMenuShowOtpSubItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.EntryContextMenuShowOtpSubItem.Click += otpDialogToolStripItem_Click;

            this.EntryContextMenuCopySubItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuCopyOtp);
            this.EntryContextMenuCopySubItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.EntryContextMenuCopySubItem.Click += otpCopyToolStripItem_Click;

            this.EntryContextMenuMainItem = new ToolStripMenuItem(KeeOtp2Statics.PluginName);
            this.EntryContextMenuMainItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.EntryContextMenuMainItem.DropDownItems.Add(this.EntryContextMenuCopySubItem);
            this.EntryContextMenuMainItem.DropDownItems.Add(this.EntryContextMenuShowOtpSubItem);
            this.EntryContextMenuMainItem.DropDownItems.Add(this.EntryContextMenuConfigureSubItem);

            if (KeeOtp2Config.ShowContextMenuItem)
            {
                this.EntryContextMenuCopyItem = new ToolStripMenuItem(KeeOtp2Statics.ToolStripMenuCopyOtp);
                this.EntryContextMenuCopyItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
                this.EntryContextMenuCopyItem.Click += otpCopyToolStripItem_Click;
                if (KeeOtp2Config.UseLocalHotKey && KeeOtp2Config.LocalHotKeyKeys != Keys.None)
                {
                    this.EntryContextMenuCopyItem.ShortcutKeys = KeeOtp2Config.LocalHotKeyKeys;
                }
                host.MainWindow.EntryContextMenu.Items.Insert(2, this.EntryContextMenuCopyItem);
            }


            host.MainWindow.EntryContextMenu.Items.Add(this.EntryContextMenuMainItem);
            host.MainWindow.EntryContextMenu.Opening += entryContextMenu_Opening;


            SprEngine.FilterCompile += new EventHandler<SprEventArgs>(SprEngine_FilterCompile);
            SprEngine.FilterCompilePre += new EventHandler<SprEventArgs>(SprEngine_FilterCompilePre);

            KeeOtp2Config.handler = onHotKeyTriggered;
            KeeOtp2Config.loadConfig();

            return true; // Initialization successful
        }

        public override void Terminate()
        {
            if (!NativeLib.IsUnix())
                KeeOtp2Config.unregisterHotKey();

            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(EntryContextMenuMainItem);
            menu = host.MainWindow.ToolsMenu.DropDownItems;
            menu.Remove(ToolsMenuMainItem);
        }

        // If built-in {TIMEOTP} placeholder is used, but KeeOtp1 Save Mode is used
        private void SprEngine_FilterCompilePre(object sender, SprEventArgs e)
        {
            if ((e.Context.Flags & SprCompileFlags.ExtActive) == SprCompileFlags.ExtActive)
            {
                string currentPlaceHolder = null;
                if (e.Text.IndexOf(BuiltInTotpPlaceHolder, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    currentPlaceHolder = BuiltInTotpPlaceHolder;
                else if (e.Text.IndexOf(BuiltInHotpPlaceHolder, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    currentPlaceHolder = BuiltInHotpPlaceHolder;
                if (!string.IsNullOrEmpty(currentPlaceHolder))
                {
                    PwEntry entry = e.Context.Entry;
                    OtpAuthData data = OtpAuthUtils.loadData(entry);
                    if (data != null)
                    {
                        
                        if (!OtpAuthUtils.checkBuiltInMode(entry) ||
                        (OtpAuthUtils.checkEntry(entry) && OtpTime.getOverrideBuiltInTime() && (OtpTime.getTimeType() == OtpTimeType.FixedOffset || OtpTime.getTimeType() == OtpTimeType.CustomNtpServer)) ||
                        !data.Proprietary)
                        {
                            OtpBase otp = OtpAuthUtils.getOtp(data);
                            if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                            {
                                e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, currentPlaceHolder, otp.getTotpString(OtpTime.getTime()));
                            }
                            else if (data.Type == OtpType.Hotp)
                            {
                                e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, currentPlaceHolder, otp.getHotpString(data.Counter));
                                OtpAuthUtils.increaseHotpCounter(host, data, entry);
                            }
                        }
                    }
                }
            }
        }

        private void SprEngine_FilterCompile(object sender, SprEventArgs e)
        {
            if ((e.Context.Flags & SprCompileFlags.ExtActive) == SprCompileFlags.ExtActive)
            {
                if (e.Text.IndexOf(KeeOtp1PlaceHolder, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    PwEntry entry = e.Context.Entry;
                    OtpAuthData data = OtpAuthUtils.loadData(entry);
                    if (data != null)
                    {
                        OtpBase otp = OtpAuthUtils.getOtp(data);
                        if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                        {
                            e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, KeeOtp1PlaceHolder, otp.getTotpString(OtpTime.getTime()));
                        }
                        else if (data.Type == OtpType.Hotp)
                        {
                            e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, KeeOtp1PlaceHolder, otp.getHotpString(data.Counter));
                            OtpAuthUtils.increaseHotpCounter(host, data, entry);
                        }
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
                this.EntryContextMenuCopySubItem.Enabled = configured;
                this.EntryContextMenuShowOtpSubItem.Enabled = configured;
            }
            if (this.EntryContextMenuCopyItem != null)
            {
                this.EntryContextMenuCopyItem.Enabled = selectedOne;
            }
            this.EntryContextMenuMainItem.Enabled =
            this.EntryContextMenuConfigureSubItem.Enabled =
            this.EntryContextMenuCopySubItem.Enabled = 
            this.EntryContextMenuShowOtpSubItem.Enabled = selectedOne;
        }

        private void otpConfigureToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                OtpAuthData data = OtpAuthUtils.loadData(entry);
                OtpInformation addEditForm = new OtpInformation(this.host, entry, data);
                addEditForm.InitEx();
                addEditForm.ShowDialog(this.host.MainWindow);
            }
        }

        private void otpDialogToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                ShowOneTimePasswords sotp = new ShowOneTimePasswords(this.host, entry);
                sotp.InitEx();
                sotp.ShowDialog(this.host.MainWindow);
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
                        ShowOneTimePasswords sotp = new ShowOneTimePasswords(this.host, entry);
                        sotp.InitEx();
                        sotp.ShowDialog(this.host.MainWindow);
                    }
                }
                else
                {
                    OtpBase otp = OtpAuthUtils.getOtp(data);
                    if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                    {
                        if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, otp.getTotpString(OtpTime.getTime())), true, this.host.MainWindow, entry, this.host.Database))
                            this.host.MainWindow.StartClipboardCountdown();
                    }
                    else if (data.Type == OtpType.Hotp)
                    {
                        if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, otp.getHotpString(data.Counter)), true, this.host.MainWindow, entry, this.host.Database))
                            this.host.MainWindow.StartClipboardCountdown();
                        OtpAuthUtils.increaseHotpCounter(host, data, entry);
                    }
                }
            }
        }

        private void settingsToolStripitem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this.host);
            settings.InitEx();
            settings.ShowDialog(this.host.MainWindow);

            if (settings.DialogResult == DialogResult.OK)
            {
                KeeOtp2Config.handler = onHotKeyTriggered;
                KeeOtp2Config.loadConfig();
            }
        }

        private void aboutToolStripitem_Click(object sender, EventArgs e)
        {
            About about = new About(this.host);
            about.InitEx();
            about.ShowDialog(this.host.MainWindow);
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
