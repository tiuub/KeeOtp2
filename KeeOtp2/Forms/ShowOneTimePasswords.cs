using System;
using System.Drawing;
using System.Windows.Forms;
using KeeOtp2.Properties;
using KeePass.Plugins;
using KeePass.Util;
using KeePassLib.Security;
using OtpNet;

namespace KeeOtp2
{
    public partial class ShowOneTimePasswords : Form
    {
        const int reloadDataDelay = 1; // in seconds

        private readonly KeePassLib.PwEntry entry;
        private readonly IPluginHost host;
        private OtpBase otp;

        private OtpAuthData data;

        private bool increaseHotpAfterClosing = false;
        private int reloadCount = 0;

        public ShowOneTimePasswords(KeePassLib.PwEntry entry, IPluginHost host)
        {
            InitializeComponent();

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
            linkLabelIncorrectNext.Text = KeeOtp2Statics.ShowOtpIncorrect;
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
        }

        private void ShowOneTimePasswords_Shown(object sender, EventArgs e)
        {
            loadData();
        }

        private void ShowOneTimePasswords_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (increaseHotpAfterClosing)
                OtpAuthUtils.increaseHotpCounter(host, data, entry);
        }

        private void linkLabelIncorrectNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
            {
                Troubleshooting troubleshooting = new Troubleshooting(this.host);
                troubleshooting.ShowDialog();
            }
            else if (data.Type == OtpType.Hotp)
            {
                OtpAuthUtils.increaseHotpCounter(host, data, entry);
                this.data = OtpAuthUtils.loadData(this.entry);
            }   
        }

        private void labelOtp_Click(object sender, EventArgs e)
        {
            CopyOtpAndClose();
        }

        private void buttonShowQR_Click(object sender, EventArgs e)
        {
            if (this.data.Encoding == OtpSecretEncoding.Base32)
            {
                ShowQrCode sqc = new ShowQrCode(this.data, this.entry, this.host);
                sqc.ShowDialog();
            }
            else
            {
                MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxShowQrWrongEncoding, this.data.Encoding.ToString()), KeeOtp2Statics.ShowOtpShowQr, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.AddEdit();
        }

        private void buttonCopyTotp_Click(object sender, EventArgs e)
        {
            CopyOtpAndClose();
        }

        private void timerUpdateOtp_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void AddEdit()
        {
            this.timerUpdateOtp.Enabled = false;
            this.groupboxTotp.Text = KeeOtp2Statics.TOTP;
            this.labelOtp.Text = insertSpaceInMiddle("000000");
            this.otp = null;

            OtpInformation addEditForm = new OtpInformation(this.data, this.entry, this.host);

            var result = addEditForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.data = OtpAuthUtils.loadData(this.entry);
                this.otp = OtpAuthUtils.getOtp(this.data);

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

        private void CopyOtpAndClose()
        {
            if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
            {
                if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.otp.getTotpString(OtpTime.getTime())), true, this.host.MainWindow, entry, this.host.Database))
                    this.host.MainWindow.StartClipboardCountdown();
            }
            else if (data.Type == OtpType.Hotp)
            {
                if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.otp.getHotpString(data.Counter)), true, this.host.MainWindow, entry, this.host.Database))
                    this.host.MainWindow.StartClipboardCountdown();
            }
            this.Close();
        }

        private void loadData()
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

        private void ShowCode()
        {
            this.otp = OtpAuthUtils.getOtp(data);
            if (data.Type == OtpType.Hotp)
                increaseHotpAfterClosing = true;
            this.timerUpdateOtp.Enabled = true;
        }

        private void UpdateDisplay()
        {
            if (reloadCount > reloadDataDelay * (1000 / timerUpdateOtp.Interval))
            {
                loadData();
                reloadCount = 0;
            }
            reloadCount++;

            if (this.otp != null)
            {
                if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                {
                    this.linkLabelIncorrectNext.Text = KeeOtp2Statics.ShowOtpIncorrect;
                    this.labelOtp.Text = insertSpaceInMiddle(otp.getTotpString(OtpTime.getTime()));
                    this.groupboxTotp.Text = String.Format(KeeOtp2Statics.ShowOtpNextRemaining, otp.getRemainingSeconds().ToString().PadLeft(2, '0'), insertSpaceInMiddle(otp.getTotpString(OtpTime.getTime().AddSeconds(data.Period))));
                }
                else if (data.Type == OtpType.Hotp)
                {
                    this.linkLabelIncorrectNext.Text = KeeOtp2Statics.ShowOtpNextCode;
                    this.labelOtp.Text = insertSpaceInMiddle(otp.getHotpString(data.Counter));
                    this.groupboxTotp.Text = String.Format(KeeOtp2Statics.ShowOtpNextCounter, data.Counter, insertSpaceInMiddle(otp.getHotpString(data.Counter + 1)));
                }
                if (labelOtp.Width - TextRenderer.MeasureText(labelOtp.Text, new Font(labelOtp.Font.FontFamily, labelOtp.Font.Size, labelOtp.Font.Style)).Width > 30)
                    labelOtp.Font = new Font(labelOtp.Font.FontFamily, 48f, labelOtp.Font.Style);
                else
                    while (labelOtp.Width < TextRenderer.MeasureText(labelOtp.Text, new Font(labelOtp.Font.FontFamily, labelOtp.Font.Size, labelOtp.Font.Style)).Width)
                    {
                        labelOtp.Font = new Font(labelOtp.Font.FontFamily, labelOtp.Font.Size - 0.5f, labelOtp.Font.Style);
                    }
            }
            else
            {
                if (MessageBox.Show(KeeOtp2Statics.MessageBoxOtpNotConfigured, KeeOtp2Statics.ShowOtp, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    AddEdit();
                else
                    this.Close();
            }
        }

        private string insertSpaceInMiddle(string s, int minimumLength = 6)
        {
            int l = s.Length;
            if (l >= minimumLength)
                return string.Format("{0}{1}{2}", s.Substring(0, (int)Math.Ceiling(Convert.ToDouble(l) / 2)), ' ', s.Substring((int)Math.Ceiling(Convert.ToDouble(l) / 2)));
            else
                return s;
        }
    }
}
