using System;
using System.ComponentModel;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.Util;
using KeePass.Util.Spr;
using KeePassLib;
using KeePassLib.Utility;
using System.Runtime.InteropServices;
using OtpSharp;
using KeeOtp2.Properties;

namespace KeeOtp2
{
    public sealed class KeeOtp2Ext : Plugin
    {
        private IPluginHost host = null;

        private ToolStripMenuItem otpDialogToolStripItem;
        private ToolStripMenuItem otpCopyToolStripItem;

        private ToolStripMenuItem MainMenuToolStripItem;
        private ToolStripMenuItem SettingsToolStripItem;
        private ToolStripMenuItem AboutToolStripItem;

        private HotKeyProvider hotKeyProvider;

        public const string KeeOtp1PlaceHolder = "{TOTP}"; // Deprecated
        public const string BuiltInPlaceHolder = "{TIMEOTP}";

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            this.host = host;


            this.SettingsToolStripItem = new ToolStripMenuItem("Settings");
            this.SettingsToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Configuration];
            this.SettingsToolStripItem.Click += settingsToolStripitem_Click;

            this.AboutToolStripItem = new ToolStripMenuItem("About");
            this.AboutToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Info];
            this.AboutToolStripItem.Click += aboutToolStripitem_Click;

            this.MainMenuToolStripItem = new ToolStripMenuItem("KeeOtp2");
            this.MainMenuToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.MainMenuToolStripItem.DropDownItems.Add(this.SettingsToolStripItem);
            this.MainMenuToolStripItem.DropDownItems.Add(this.AboutToolStripItem);
            host.MainWindow.ToolsMenu.DropDownItems.Add(this.MainMenuToolStripItem);

            this.otpDialogToolStripItem = new ToolStripMenuItem("Timed One Time Password");
            this.otpDialogToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.otpDialogToolStripItem.Click += otpDialogToolStripItem_Click;
            host.MainWindow.EntryContextMenu.Items.Insert(11, this.otpDialogToolStripItem);

            this.otpCopyToolStripItem = new ToolStripMenuItem("Copy TOTP");
            this.otpCopyToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Clock];
            this.otpCopyToolStripItem.ShortcutKeys = Keys.T | Keys.Control;
            this.otpCopyToolStripItem.Click += otpCopyToolStripItem_Click;
            host.MainWindow.EntryContextMenu.Items.Insert(2, this.otpCopyToolStripItem);
            host.MainWindow.EntryContextMenu.Opening += entryContextMenu_Opening;

            SprEngine.FilterCompile += new EventHandler<SprEventArgs>(SprEngine_FilterCompile);
            SprEngine.FilterCompilePre += new EventHandler<SprEventArgs>(SprEngine_FilterCompilePre);

            loadConfig();

            return true; // Initialization successful
        }

        public override void Terminate()
        {
            if (hotKeyProvider != null)
                hotKeyProvider.unregisterHotKey();

            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(otpDialogToolStripItem);
            menu.Remove(otpCopyToolStripItem);
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
                            var totp = new Totp(data.Key, data.Period, data.Algorithm, data.Digits, null);
                            var text = totp.ComputeTotp(OtpTime.getTime()).ToString().PadLeft(data.Digits, '0');

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
                        var totp = new Totp(data.Key, data.Period, data.Algorithm, data.Digits, null);
                        var text = totp.ComputeTotp(OtpTime.getTime()).ToString().PadLeft(data.Digits, '0');

                        e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, KeeOtp1PlaceHolder, text);
                    }
                }
            }
        }

        private void entryContextMenu_Opening(object sender, CancelEventArgs e)
        {
            PwEntry[] selectedEntries = this.host.MainWindow.GetSelectedEntries();
            this.otpCopyToolStripItem.Enabled =
                this.otpDialogToolStripItem.Enabled =
                selectedEntries != null && selectedEntries.Length == 1;
        }

        void otpDialogToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (GetSelectedSingleEntry(out entry))
            {
                ShowOneTimePasswords form = new ShowOneTimePasswords(entry, host);
                form.ShowDialog();
            }
        }

        void otpCopyToolStripItem_Click(object sender, EventArgs e)
        {
            PwEntry entry;
            if (this.GetSelectedSingleEntry(out entry))
            {
                OtpAuthData data = OtpAuthUtils.loadData(entry);
                if (data == null)
                {
                    if (MessageBox.Show("Must configure TOTP on this entry.  Do you want to do this now?", "Not Configured", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ShowOneTimePasswords form = new ShowOneTimePasswords(entry, host);
                        form.ShowDialog();
                    }
                }
                else
                {
                    var totp = new Totp(data.Key, data.Period, data.Algorithm, data.Digits, null);
                    var text = totp.ComputeTotp(OtpTime.getTime()).ToString().PadLeft(data.Digits, '0');

                    if (ClipboardUtil.CopyAndMinimize(new KeePassLib.Security.ProtectedString(true, text), true, this.host.MainWindow, entry, this.host.Database))
                        this.host.MainWindow.StartClipboardCountdown();
                }
            }
        }

        void settingsToolStripitem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this.host);
            settings.ShowDialog();

            if (settings.DialogResult == DialogResult.OK)
                loadConfig();
        }

        void aboutToolStripitem_Click(object sender, EventArgs e)
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
                MessageBox.Show("Please select an entry", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (entries.Length > 1)
            {
                MessageBox.Show("Please select only one entry", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                // grab the entry that we care about
                entry = entries[0];
                return true;
            }
        }

        private void loadConfig()
        {
            if (hotKeyProvider == null)
                hotKeyProvider = new HotKeyProvider(host);
            hotKeyProvider.unregisterHotKey();

            Keys hotKey = KeeOtp2Config.HotKeyKeys;
            if (KeeOtp2Config.UseHotKey && hotKey != Keys.None)
            {
                hotKeyProvider.registerHotKey(hotKey);
            }

            if (OtpTime.getTimeType() == OtpTimeType.CustomNtpServer)
                OtpTime.pollCustomNtpServer();
        }

        private class HotKeyProvider : Form
        {
            private static readonly int AutoType = 101;
            private IPluginHost host;

            private _MethodInfo m_miAutoType = null;

            public HotKeyProvider(IPluginHost host)
            {
                this.host = host;

                this.m_miAutoType = host.MainWindow.GetType().GetMethod("ExecuteGlobalAutoType", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, new Type[] { typeof(string) }, null);
            }

            public void registerHotKey(Keys keys)
            {
                HotKeyManager.Initialize(this);
                HotKeyManager.RegisterHotKey(AutoType, keys);
            }

            public void unregisterHotKey()
            {
                HotKeyManager.UnregisterAll();
            }

            internal void HandleHotKey(int wParam)
            {
                if (wParam == AutoType)
                    if (m_miAutoType != null)
                        m_miAutoType.Invoke(this.host.MainWindow, new object[] { KeeOtp2Config.HotKeySequence });
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x0312)
                {
                    HandleHotKey((int)m.WParam);
                }
                base.WndProc(ref m);
            }
        }

        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/tiuub/KeeOtp2/master/VERSION"; }
        }
    }
}
