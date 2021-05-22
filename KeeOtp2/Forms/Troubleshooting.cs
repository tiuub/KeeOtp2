using System;
using System.Diagnostics;
using System.Windows.Forms;
using KeeOtp2.Properties;
using KeePass.Plugins;
using Yort.Ntp;

namespace KeeOtp2
{
    public partial class Troubleshooting : Form
    {
        IPluginHost host;

        public Troubleshooting(IPluginHost host)
        {
            InitializeComponent();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.help_white,
                KeeOtp2Statics.Troubleshooting,
                KeeOtp2Statics.TroubleshootingSubline);

            this.Icon = host.MainWindow.Icon;
            this.TopMost = host.MainWindow.TopMost;

            this.host = host;

            groupBoxInformation.Text = KeeOtp2Statics.Information;
            labelInformation.Text = KeeOtp2Statics.TroubleshootingInformation;
            groupBoxActions.Text = KeeOtp2Statics.Actions;
            buttonPingNTPServer.Text = KeeOtp2Statics.TroubleshootingPingNtpServer;
            buttonSettings.Text = KeeOtp2Statics.TroubleshootingChangeSettings;
            buttonTroubleshootingWebsite.Text = KeeOtp2Statics.TroubleshootingCheckWebsite;
            labelStatus.Text = KeeOtp2Statics.HoverInformation;
            buttonOK.Text = KeeOtp2Statics.OK;
            buttonCancel.Text = KeeOtp2Statics.Cancel;

            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = KeeOtp2Statics.Troubleshooting;
            toolTip.IsBalloon = true;

            string toolTipPingNTPServer = KeeOtp2Statics.ToolTipTroubleshootingPingNtpServer;
            toolTip.SetToolTip(buttonPingNTPServer, toolTipPingNTPServer);

            string toolTipChangeSettings = KeeOtp2Statics.ToolTipTroubleshootingChangeSettings;
            toolTip.SetToolTip(buttonSettings, toolTipChangeSettings);

            string toolTipTroubleshootingWebsite = KeeOtp2Statics.ToolTipTroubleshootingWebsite;
            toolTip.SetToolTip(buttonTroubleshootingWebsite, toolTipTroubleshootingWebsite);

        }

        private void Troubleshooting_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;
        }

        private void buttonPingGoogle_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.buttonPingNTPServer.Visible = false;

            var client = new NtpClient();
            client.TimeReceived += Client_TimeReceived;
            client.BeginRequestTime();
            
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this.host);
            settings.ShowDialog();

            if (settings.DialogResult == DialogResult.OK)
                KeeOtp2Config.loadConfig();
        }

        private void buttonTroubleshootingWebsite_Click(object sender, EventArgs e)
        {
            Process.Start(KeeOtp2Statics.RepositoryTroubleshooting);
        }

        private void Client_TimeReceived(object sender, NtpTimeReceivedEventArgs e)
        {
            this.buttonPingNTPServer.Visible = true;

            TimeSpan timeDifference = OtpTime.getTime().Subtract(e.CurrentTime);

            this.Invoke((Action)(() =>
            {
                if (-5000 < timeDifference.TotalMilliseconds && timeDifference.TotalMilliseconds < 5000)
                    MessageBox.Show(String.Format(KeeOtp2Statics.TroubleshootingPingResultOk, Math.Round(Math.Abs(timeDifference.TotalMilliseconds)), Math.Round(Math.Abs((float)timeDifference.TotalMilliseconds / 1000), 1), (timeDifference.TotalMilliseconds < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (-30000 < timeDifference.TotalMilliseconds && timeDifference.TotalMilliseconds < 30000)
                    MessageBox.Show(String.Format(KeeOtp2Statics.TroubleshootingPingResultModerate, Math.Round(Math.Abs(timeDifference.TotalMilliseconds)), Math.Round(Math.Abs((float)timeDifference.TotalMilliseconds / 1000), 1), (timeDifference.TotalMilliseconds < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(String.Format(KeeOtp2Statics.TroubleshootingPingResultBad, Math.Round(Math.Abs(timeDifference.TotalMilliseconds)), Math.Round(Math.Abs((float)timeDifference.TotalMilliseconds / 1000), 1), (timeDifference.TotalMilliseconds < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }));
            this.Enabled = true;
        }
    }
}
