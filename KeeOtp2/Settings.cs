using KeePass.Plugins;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeeOtp2
{
    public partial class Settings : Form
    {
        private IPluginHost host;
        private readonly BackgroundWorker backgroundWorkerMigrate;

        private bool migrateAutoType;

        public Settings(IPluginHost host)
        {
            InitializeComponent();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.lock_key.GetThumbnailImage(32, 32, null, IntPtr.Zero),
                "Settings",
                "Migrate all your entries to built-in OTP function.");

            this.Icon = host.MainWindow.Icon;

            this.host = host;

            this.backgroundWorkerMigrate = new BackgroundWorker();
            this.backgroundWorkerMigrate.DoWork += backgroundWorkerMigrate_DoWork;
            this.backgroundWorkerMigrate.RunWorkerCompleted += backgroundWorkerMigrate_RunWorkerCompleted;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.backgroundWorkerMigrate.IsBusy)
                this.backgroundWorkerMigrate.CancelAsync();
        }

        private void buttonMigrate_Click(object sender, EventArgs e)
        {
            this.migrateAutoType = ((MessageBox.Show("Do you want to replace the Auto-Type key {TOTP} with the built-in key {TIMEOTP}?", "Migrate Auto-Type", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) ? true : false);

            this.buttonOK.Enabled = false;
            this.buttonCancel.Enabled = true;
            this.buttonCancel.Visible = true;
            this.labelMigrationStatus.Enabled = true;
            this.labelMigrationStatus.Visible = true;

            this.backgroundWorkerMigrate.RunWorkerAsync();
        }

        private void backgroundWorkerMigrate_DoWork(object sender, DoWorkEventArgs e)
        {
            PwUuid RecycleBinUuid = this.host.Database.RecycleBinUuid;

            List<PwEntry> entries = new List<PwEntry>();
            entries = this.host.Database.RootGroup.GetEntries(true).ToList();

            int count = entries.Count;
            int counter = 0;

            labelMigrationStatus.Text = String.Format("Loaded {0} entrie(s)!", count);

            
            foreach (PwEntry entry in entries)
            {
                if (entry.ParentGroup.Uuid != RecycleBinUuid)
                {
                    if (OtpAuthUtils.checkKeeOtp1Mode(entry))
                    {
                        if (this.migrateAutoType)
                        {
                            entry.AutoType.DefaultSequence = entry.AutoType.DefaultSequence.Replace("{TOTP}", "{TIMEOTP}");
                            foreach (KeePassLib.Collections.AutoTypeAssociation ata in entry.AutoType.Associations)
                            {
                                ata.Sequence = ata.Sequence.Replace("{TOTP}", "{TIMEOTP}");
                            }
                        }

                        OtpAuthData data = OtpAuthUtils.loadDataFromKeeOtp1String(entry);
                        OtpAuthUtils.purgeLoadedFields(data, entry);
                        OtpAuthUtils.migrateToBuiltInOtp(data, entry);
                    }
                }
                counter++;
                labelMigrationStatus.Text = String.Format("Done {0} of {1} entries!", counter, count);
            }

        }
        private void backgroundWorkerMigrate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Visible = false;
            //this.labelMigrationStatus.Enabled = false;
            //this.labelMigrationStatus.Visible = false;
            this.buttonOK.Enabled = true;
        }
    }
}
