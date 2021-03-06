using KeeOtp2.Properties;
using KeePass.Plugins;
using KeePass.Util;
using KeePassLib;
using KeePassLib.Security;
using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeeOtp2
{
    public partial class ShowQrCode : Form
    {
        const int QRCODETIMEOUT = 180;

        IPluginHost host;
        PwEntry entry;
        Uri uri;

        public ShowQrCode(Uri uri, PwEntry entry, IPluginHost host)
        {
            InitializeComponent();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.clock.GetThumbnailImage(32, 32, null, IntPtr.Zero),
                "Show QR Code",
                "Set up your TOTP on other devices.");

            this.Icon = host.MainWindow.Icon;
            this.TopMost = host.MainWindow.TopMost;

            this.uri = uri;
            this.entry = entry;
            this.host = host;
        }

        private void showQrCodeImage()
        {
            buttonReload.Visible = false;
            formTimeout = QRCODETIMEOUT;

            PayloadGenerator.Url url = new PayloadGenerator.Url(this.uri.AbsoluteUri);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            pictureBoxQrCode.Image = qrCode.GetGraphic(40);

            timerFormTimeout.Start();
        }

        private void ShowQrCode_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;

            showQrCodeImage();
        }

        private void buttonCopyUri_Click(object sender, EventArgs e)
        {
            if (ClipboardUtil.CopyAndMinimize(new ProtectedString(true, this.uri.AbsoluteUri), true, this.host.MainWindow, this.entry, this.host.Database))
                this.host.MainWindow.StartClipboardCountdown();
            
            this.Close();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            showQrCodeImage();
        }

        private int formTimeout = QRCODETIMEOUT;
        private void timerFormTimeout_Tick(object sender, EventArgs e)
        {
            this.Text = String.Format("QR Code - Timeout in {0} seconds.", formTimeout);

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
                    graphics.DrawString(String.Format("Due to security reasons,\r\nthe QR Code was removed\r\nafter {0} seconds.\r\n\r\nPress Reload,\r\nto show it again!", QRCODETIMEOUT), font, Brushes.Black, rectangle, sf);
                    pictureBoxQrCode.Image = bitmap;
                }
                buttonReload.Visible = true;
            }
            formTimeout--;
        }
    }
}
