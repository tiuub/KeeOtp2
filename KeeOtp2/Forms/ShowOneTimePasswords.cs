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
        private OtpTotp totp;
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
                KeeOtp2Statics.ShowOtp,
                KeeOtp2Statics.ShowOtpSubline);

            this.Icon = host.MainWindow.Icon;
            this.TopMost = host.MainWindow.TopMost;

            this.host = host;
            this.entry = entry;

            groupboxTotp.Text = KeeOtp2Statics.TOTP;
            linkLabelIncorrect.Text = KeeOtp2Statics.ShowOtpIncorrect;
            buttonShowQR.Text = KeeOtp2Statics.ShowOtpShowQr + KeeOtp2Statics.InformationChar;
            buttonEdit.Text = KeeOtp2Statics.Edit;
            buttonCopyTotp.Text = KeeOtp2Statics.Copy;
            buttonClose.Text = KeeOtp2Statics.Close;

            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = KeeOtp2Statics.ShowOtp;
            toolTip.IsBalloon = true;
            toolTip.SetToolTip(buttonShowQR, KeeOtp2Statics.ToolTipShowQrCode);
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
                string code = totp.getTotpString(OtpTime.getTime());
                string nextCode = totp.getTotpString(OtpTime.getTime().AddSeconds(data.Period));
                var remaining = totp.getRemainingSeconds();

                if (code != lastCode)
                {
                    lastCode = code;
                    this.labelOtp.Text = code;
                }
                if (remaining != lastRemainingTime)
                {
                    lastRemainingTime = remaining;
                    this.groupboxTotp.Text = String.Format(KeeOtp2Statics.ShowOtpNextRemaining, remaining.ToString().PadLeft(2, '0'), nextCode);
                }
            }
            else
            {
                if (MessageBox.Show(KeeOtp2Statics.MessageBoxOtpNotConfigured, KeeOtp2Statics.ShowOtp, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AddEdit();
                }
                else
                {
                    this.Close();
                }
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

            this.totp = OtpAuthUtils.getTotp(data);
            this.timerUpdateTotp.Enabled = true;
        }

        private void AddEdit()
        {
            this.timerUpdateTotp.Enabled = false;
            this.groupboxTotp.Text = KeeOtp2Statics.TOTP;
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
            if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.totp.getTotpString(OtpTime.getTime())), true, this.host.MainWindow, entry, this.host.Database))
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
                MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxShowQrWrongEncoding, this.data.Encoding.ToString()), KeeOtp2Statics.ShowOtpShowQr, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void buttonCopyTotp_Click(object sender, EventArgs e)
        {
            if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.totp.getTotpString(OtpTime.getTime())), true, this.host.MainWindow, entry, this.host.Database))
                this.host.MainWindow.StartClipboardCountdown();
            this.Close();
        }
    }
}
