using System;
using System.Windows.Forms;
using KeeOtp2.Properties;
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
        private string lastCode;
        private int lastRemainingTime;

        OtpAuthData data;

        public ShowOneTimePasswords(KeePassLib.PwEntry entry, IPluginHost host)
        {
            InitializeComponent();
            this.timerUpdateTotp.Tick += (sender, e) => UpdateDisplay();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.clock_white,
                "Timed Passwords",
                "Enter this code in the verification system.");

            this.Icon = host.MainWindow.Icon;
            this.TopMost = host.MainWindow.TopMost;

            this.host = host;
            this.entry = entry;

            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = "Show QR Code";
            toolTip.IsBalloon = true;
            toolTip.SetToolTip(buttonShowQR, "Through this QR Cdoe you can configure this OTP on other devices. You can scan this QR Code with Google Authenticator for example.");
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
                string code = totp.ComputeTotp(OtpTime.getTime());
                string nextCode = totp.ComputeTotp(OtpTime.getTime().AddSeconds(data.Period));
                var remaining = totp.RemainingSeconds();

                if (code != lastCode)
                {
                    lastCode = code;
                    this.labelOtp.Text = code.ToString().PadLeft(this.data.Digits, '0');
                }
                if (remaining != lastRemainingTime)
                {
                    lastRemainingTime = remaining;
                    this.groupboxTotp.Text = String.Format("TOTP - Time remaining: {0} - Next code: {1}", remaining.ToString().PadLeft(2, '0'), nextCode.ToString().PadLeft(this.data.Digits, '0'));
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
            this.lastCode = "";
            this.lastRemainingTime = 0;

            this.totp = new Totp(data.Key, data.Period, data.Algorithm, data.Digits, null);
            this.timerUpdateTotp.Enabled = true;
        }

        private void AddEdit()
        {
            this.timerUpdateTotp.Enabled = false;
            this.groupboxTotp.Text = "TOTP";
            this.labelOtp.Text = "000000";
            this.totp = null;

            OtpInformation addEditForm = new OtpInformation(this.data, this.entry, this.host);

            var result = addEditForm.ShowDialog();
            if (result == DialogResult.OK)
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

        private void linkLabelWrong_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Troubleshooting troubleshooting = new Troubleshooting(this.host);
            troubleshooting.ShowDialog();
        }

        private void labelOtp_Click(object sender, EventArgs e)
        {
            if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.totp.ComputeTotp(OtpTime.getTime()).ToString().PadLeft(data.Digits, '0')), true, this.host.MainWindow, entry, this.host.Database))
                this.host.MainWindow.StartClipboardCountdown();
            this.Close();
        }

        private void buttonShowQR_Click(object sender, EventArgs e)
        {
            if (this.data.Encoding == OtpSecretEncoding.Base32)
            {
                Uri uri = OtpAuthUtils.otpAuthDataToUri(this.entry, this.data);
                ShowQrCode sqc = new ShowQrCode(uri, this.entry, this.host);
                sqc.ShowDialog();
            }
            else
            {
                MessageBox.Show("QRCodes can only be used with Base32 secret encoding.\n\nYour encoding: " + this.data.Encoding.ToString(), "Failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void buttonCopyTotp_Click(object sender, EventArgs e)
        {
            if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.totp.ComputeTotp(OtpTime.getTime()).ToString().PadLeft(data.Digits, '0')), true, this.host.MainWindow, entry, this.host.Database))
                this.host.MainWindow.StartClipboardCountdown();
            this.Close();
        }
    }
}
