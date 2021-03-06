using System;
using System.Diagnostics;
using System.Windows.Forms;
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
            this.Icon = host.MainWindow.Icon;

            this.host = host;
            this.TopMost = host.MainWindow.TopMost;
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
            this.progressBarGettingTimeCorrection.Visible = true;

            var client = new NtpClient();
            client.TimeReceived += Client_TimeReceived;
            client.BeginRequestTime();
            
        }

        private void buttonTroubleshootingWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/tiuub/KeeOtp2");
        }

        private void Client_TimeReceived(object sender, NtpTimeReceivedEventArgs e)
        {
            this.buttonPingNTPServer.Visible = true;
            this.progressBarGettingTimeCorrection.Visible = false;
            TimeSpan timeDifference = OtpTime.getTime().Subtract(e.CurrentTime);

            this.Invoke((Action)(() =>
            {
                if (-5000 < timeDifference.TotalMilliseconds && timeDifference.TotalMilliseconds < 5000)
                    MessageBox.Show(String.Format("You are {0} milliseconds ({1} seconds) {2}. All fine!", Math.Round(Math.Abs(timeDifference.TotalMilliseconds)), Math.Round(Math.Abs((float)timeDifference.TotalMilliseconds / 1000), 1), (timeDifference.TotalMilliseconds < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (-30000 < timeDifference.TotalMilliseconds && timeDifference.TotalMilliseconds < 30000)
                    MessageBox.Show(String.Format("You are {0} milliseconds ({1} seconds) {2}. It may work, but you should check your time settings!", Math.Round(Math.Abs(timeDifference.TotalMilliseconds)), Math.Round(Math.Abs((float)timeDifference.TotalMilliseconds / 1000), 1), (timeDifference.TotalMilliseconds < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(String.Format("You are {0} milliseconds ({1} seconds) {2}. Generating TOTPs wont work. You should definitely check your time settings!", Math.Round(Math.Abs(timeDifference.TotalMilliseconds)), Math.Round(Math.Abs((float)timeDifference.TotalMilliseconds / 1000), 1), (timeDifference.TotalMilliseconds < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }));
            this.Enabled = true;
        }
    }
}
