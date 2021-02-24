using System;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.Util;
using KeePassLib.Security;
using OtpSharp;

namespace KeeOtp2
{
    public partial class ShowOneTimePasswords : Form
    {
        private readonly KeePassLib.PwEntry entry;
        private readonly IPluginHost host ;
        private Totp totp;
        private int lastCode;
        private int lastRemainingTime;

        OtpAuthData data;

        public ShowOneTimePasswords(KeePassLib.PwEntry entry, IPluginHost host)
        {
            this.host = host;
            this.entry = entry;
            InitializeComponent();
            this.timerUpdateTotp.Tick += (sender, e) => UpdateDisplay();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.clock,
                "Timed Passwords",
                "Enter this code in the verification system.");

            this.Icon = host.MainWindow.Icon;
            this.TopMost = host.MainWindow.TopMost;
        }

        private void ShowOneTimePasswords_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;
            FormWasShown();
        }

        private void UpdateDisplay()
        {
            var totp = this.totp;
            if (totp != null)
            {
                var code = totp.ComputeTotp();
                var remaining = totp.RemainingSeconds();

                if (code != lastCode)
                {
                    lastCode = code;
                    this.labelOtp.Text = code.ToString().PadLeft(this.data.Size, '0');
                }
                if (remaining != lastRemainingTime)
                {
                    lastRemainingTime = remaining;
                    this.groupboxTotp.Text = "TOTP - Time remaining: " + remaining.ToString();
                }
            }
            else
            {
                MessageBox.Show("Please add a one time password field");
                this.Close();
            }
        }

        private void FormWasShown()
        {
            this.data = OtpAuthUtils.loadData(this.entry);
            if (this.data == null)
            {
                this.AddEdit();
            }
            else
            {
                ShowCode();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.AddEdit();
        }

        private void ShowCode()
        {
            this.lastCode = 0;
            this.lastRemainingTime = 0;

            this.totp = new Totp(data.Key, step: data.Step, mode: data.OtpHashMode, totpSize: data.Size);
            this.timerUpdateTotp.Enabled = true;
        }

        private void AddEdit()
        {
            this.timerUpdateTotp.Enabled = false;
            this.groupboxTotp.Text = "TOTP";
            this.labelOtp.Text = "000000";
            this.totp = null;

            var addEditForm = new OtpInformation(this.data, this.entry, this.host);

            var result = addEditForm.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.data = OtpAuthUtils.loadData(this.entry);

                if (this.data != null)
                    this.ShowCode();
                else
                    this.Close();
            }
            else if (this.data == null)
                this.Close();
            else
                this.ShowCode();
        }

        private void buttonIncorrect_Click(object sender, EventArgs e)
        {
            Troubleshooting troubleshooting = new Troubleshooting(this.host);
            troubleshooting.ShowDialog();
        }

        private void buttonCopyTotp_Click(object sender, EventArgs e)
        {
            if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.totp.ComputeTotp().ToString().PadLeft(data.Size, '0')), true, this.host.MainWindow, entry, this.host.Database))
                this.host.MainWindow.StartClipboardCountdown();
            this.Close();
        }
    }
}
