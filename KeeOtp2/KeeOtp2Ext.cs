using System;
using System.ComponentModel;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.Util;
using KeePass.Util.Spr;
using KeePassLib;
using KeePassLib.Utility;
using OtpSharp;

namespace KeeOtp2
{
    public sealed class KeeOtp2Ext : Plugin
    {
        private IPluginHost host = null;

        private ToolStripMenuItem otpDialogToolStripItem;
        private ToolStripMenuItem otpCopyToolStripItem;

        private ToolStripMenuItem MainMenuToolStripItem;
        private ToolStripMenuItem SettingsToolStripItem;

        private const string totpPlaceHolder = "{TOTP}";

        public override bool Initialize(IPluginHost host)
        {
            if (host == null)
                return false;
            this.host = host;

            


            this.SettingsToolStripItem = new ToolStripMenuItem("Settings");
            this.SettingsToolStripItem.Image = host.MainWindow.ClientIcons.Images[(int)PwIcon.Tool];
            this.SettingsToolStripItem.Click += settingsToolStripitem_Click;

            this.MainMenuToolStripItem = new ToolStripMenuItem("KeeOtp2");
            this.MainMenuToolStripItem.Image = Resources.clock;
            this.MainMenuToolStripItem.DropDownItems.Add(this.SettingsToolStripItem);
            host.MainWindow.ToolsMenu.DropDownItems.Add(this.MainMenuToolStripItem);

            this.otpDialogToolStripItem = new ToolStripMenuItem("Timed One Time Password");
            this.otpDialogToolStripItem.Image = Resources.clock;
            this.otpDialogToolStripItem.Click += otpDialogToolStripItem_Click;
            host.MainWindow.EntryContextMenu.Items.Insert(11, this.otpDialogToolStripItem);

            this.otpCopyToolStripItem = new ToolStripMenuItem("Copy TOTP");
            this.otpCopyToolStripItem.ShortcutKeys = Keys.T | Keys.Control;
            this.otpCopyToolStripItem.Click += otpCopyToolStripItem_Click;
            host.MainWindow.EntryContextMenu.Items.Insert(2, this.otpCopyToolStripItem);
            host.MainWindow.EntryContextMenu.Opening += entryContextMenu_Opening;

            SprEngine.FilterCompile += new EventHandler<SprEventArgs>(SprEngine_FilterCompile);

            // this adds a hint on the placeholder form under the "plugin provided" section of placeholders
            SprEngine.FilterPlaceholderHints.Add(totpPlaceHolder);

            return true; // Initialization successful
        }

        void SprEngine_FilterCompile(object sender, SprEventArgs e)
        {
            if ((e.Context.Flags & SprCompileFlags.ExtActive) == SprCompileFlags.ExtActive)
            {
                if (e.Text.IndexOf(totpPlaceHolder, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    OtpAuthData data = OtpAuthUtils.loadData(e.Context.Entry);
                    if (data != null)
                    {
                        var totp = new Totp(data.Key, step: data.Step, mode: data.OtpHashMode, totpSize: data.Size);
                        var text = totp.ComputeTotp().ToString().PadLeft(data.Size, '0');

                        e.Text = StrUtil.ReplaceCaseInsensitive(e.Text, "{TOTP}", text);
                    }
                }
            }
        }

        public override void Terminate()
        {
            // Remove all of our menu items
            ToolStripItemCollection menu = host.MainWindow.EntryContextMenu.Items;
            menu.Remove(otpDialogToolStripItem);
            menu.Remove(otpCopyToolStripItem);

            SprEngine.FilterPlaceholderHints.Remove(totpPlaceHolder);
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
                    var totp = new Totp(data.Key, step: data.Step, mode: data.OtpHashMode, totpSize: data.Size);
                    var text = totp.ComputeTotp().ToString().PadLeft(data.Size, '0');

                    if (ClipboardUtil.CopyAndMinimize(new KeePassLib.Security.ProtectedString(true, text), true, this.host.MainWindow, entry, this.host.Database))
                        this.host.MainWindow.StartClipboardCountdown();
                }
            }
        }

        void settingsToolStripitem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this.host);
            settings.ShowDialog();
        }

        private bool GetSelectedSingleEntry(out PwEntry entry)
        {
            entry = null;

            var entries = this.host.MainWindow.GetSelectedEntries();
            if (entries == null || entries.Length == 0)
            {
                MessageBox.Show("Please select an entry");
                return false;
            }
            else if (entries.Length > 1)
            {
                MessageBox.Show("Please select only one entry");
                return false;
            }
            else
            {
                // grab the entry that we care about
                entry = entries[0];
                return true;
            }
        }

        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/tiuub/KeeOtp2/master/VERSION"; }
        }
    }
}
