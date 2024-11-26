using KeeOtp2.Properties;
using KeePass.Plugins;
using KeePass.Util;
using KeePassLib;
using KeePassLib.Security;
using ZXing;
using ZXing.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeeOtp2
{
    public partial class ShowQrCode : Form
    {
        const int SHARINGTIMEOUT = 180;
        private bool sharingExpired = false;

        private IPluginHost host;
        private PwEntry entry;
        private OtpAuthData data;
        private Uri uri;
        private string issuer;
        private string username;

        private ToolTip copyUriToolTip;
        private ToolTip reloadToolTip;

        public ShowQrCode(IPluginHost host, PwEntry entry, OtpAuthData data)
        {
            InitializeComponent();
            // this.ClientSize = new System.Drawing.Size(560, 680);

            this.host = host;
            this.data = data;
            issuer = entry.Strings.ReadSafe(PwDefs.TitleField);
            username = entry.Strings.ReadSafe(PwDefs.UserNameField);
            uri = OtpAuthUtils.otpAuthDataToUri(entry, data, issuer, username);
            this.entry = entry;
        }

        private void ShowQrCode_Load(object sender, EventArgs e)
        {
            Point location = this.Owner.Location;
            location.Offset(20, 20);
            this.Location = location;

            this.Icon = this.host.MainWindow.Icon;
            this.TopMost = this.host.MainWindow.TopMost;

            PluginUtils.CheckKeeTheme(this);
        }

        public void InitEx()
        {
            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.qr_white,
                KeeOtp2Statics.ShowQr,
                KeeOtp2Statics.ShowQrSubline);

            groupBoxQRCode.Text = KeeOtp2Statics.ShowQrDisclamer;
            buttonOk.Text = KeeOtp2Statics.OK;


            copyUriToolTip = new ToolTip();
            copyUriToolTip.ToolTipTitle = KeeOtp2Statics.ShowQrCopyUri;
            copyUriToolTip.IsBalloon = true;

            string toolTipCopyUri = String.Format(KeeOtp2Statics.ToolTipShowQrCodeCopyUri, this.uri.AbsoluteUri);
            copyUriToolTip.SetToolTip(buttonCopyUriReload, toolTipCopyUri);

            reloadToolTip = new ToolTip();
            reloadToolTip.ToolTipTitle = KeeOtp2Statics.ShowQr;
            reloadToolTip.IsBalloon = true;

            string toolTipReload = KeeOtp2Statics.ToolTipShowQrCodeReload;
            reloadToolTip.SetToolTip(buttonCopyUriReload, toolTipReload);

            textBoxIssuer.Text = issuer;
            textBoxUsername.Text = username;

            if (!data.Proprietary)
                MessageBox.Show(KeeOtp2Statics.MessageBoxOtpNotProprietary, KeeOtp2Statics.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            ShowQrCodeImage();
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            if (sharingExpired)
            {
                return;
            }
            GenerateQRCode();
        }

        private void ShowQrCodeImage()
        {
            sharingExpired = false;
            buttonCopyUriReload.Text = KeeOtp2Statics.ShowQrCopyUri + KeeOtp2Statics.InformationChar;
            reloadToolTip.Active = false;
            copyUriToolTip.Active = true;

            formTimeout = SHARINGTIMEOUT;
            GenerateQRCode();

            timerFormTimeout.Start();
        }

        private void GenerateQRCode()
        {
            if (pictureBoxQrCode == null || uri == null)
            {
                return;
            }
            EncodingOptions options = new EncodingOptions();
            options.Width = pictureBoxQrCode.Width;
            options.Height = pictureBoxQrCode.Height;

            IBarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
            var result = writer.Write(this.uri.AbsoluteUri);
            pictureBoxQrCode.Image = new Bitmap(result);
        }

        private void buttonCopyUriReload_Click(object sender, EventArgs e)
        {
            if (sharingExpired)
            {
                ShowQrCodeImage();
            }
            else
            {
                if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.uri.AbsoluteUri), true, this.host.MainWindow, this.entry, this.host.Database))
                    this.host.MainWindow.StartClipboardCountdown();

                this.Close();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            issuer = textBoxIssuer.Text;
            username = textBoxUsername.Text;
            this.uri = OtpAuthUtils.otpAuthDataToUri(entry, data, issuer, username);
            string toolTipCopyUri = String.Format(KeeOtp2Statics.ToolTipShowQrCodeCopyUri, this.uri.AbsoluteUri);
            copyUriToolTip.SetToolTip(buttonCopyUriReload, toolTipCopyUri);
            ShowQrCodeImage();
        }

        private int formTimeout = SHARINGTIMEOUT;
        private void timerFormTimeout_Tick(object sender, EventArgs e)
        {
            this.Text = String.Format(KeeOtp2Statics.ShowQrTimeout, formTimeout);

            if (formTimeout < 1)
            {
                timerFormTimeout.Stop();
                Rectangle rectangle = new Rectangle(0, 0, pictureBoxQrCode.Width, pictureBoxQrCode.Height);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                Bitmap bitmap = new Bitmap(pictureBoxQrCode.Width, pictureBoxQrCode.Height);
                using (Font font = new Font("Arial", 14))
                {
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.DrawString(String.Format(KeeOtp2Statics.ShowQrExpired, SHARINGTIMEOUT), font, Brushes.Black, rectangle, sf);
                    pictureBoxQrCode.Image = bitmap;
                }
                sharingExpired = true;
                buttonCopyUriReload.Text = KeeOtp2Statics.Reload + KeeOtp2Statics.InformationChar;
                copyUriToolTip.Active = false;
                reloadToolTip.Active = true;
            }
            formTimeout--;
        }
    }
}
