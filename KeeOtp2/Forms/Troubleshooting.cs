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
        }

        private void Troubleshooting_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;
        }

        private void buttonPingGoogle_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.buttonPingGoogle.Visible = false;
            this.progressBarGettingTimeCorrection.Visible = true;

            var client = new NtpClient();
            client.TimeReceived += Client_TimeReceived;
            client.BeginRequestTime();
            
        }

        private void buttonTroubleshootingWebsite_Click(object sender, EventArgs e)
        {
            // go to the troubleshooting page
            var url = "https://github.com/tiuub/KeeOtp2";
            Process ps = new Process();
            ps.StartInfo = new ProcessStartInfo(url);
            ps.Start();
        }

        private void Client_TimeReceived(object sender, NtpTimeReceivedEventArgs e)
        {
            this.buttonPingGoogle.Visible = true;
            this.progressBarGettingTimeCorrection.Visible = false;
            //TimeSpan difference = DateTime.Now.Subtract();
            int difference = (DateTime.Now.Millisecond - e.CurrentTime.Millisecond);

            if (-5000 < difference && difference < 5000)
                MessageBox.Show(String.Format("You are {0} milliseconds ({1} seconds) {2}. All fine!", Math.Abs(difference), Math.Round(Math.Abs((float)difference / 1000), 1), (difference < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (-30000 < difference && difference < 30000)
                MessageBox.Show(String.Format("You are {0} milliseconds ({1} seconds) {2}. It may work, but you should check your time settings!", Math.Abs(difference), Math.Round(Math.Abs((float)difference / 1000), 1), (difference < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(String.Format("You are {0} milliseconds ({1} seconds) {2}. Generating TOTPs wont work. You should definitely check your time settings!", Math.Abs(difference), Math.Round(Math.Abs((float)difference / 1000), 1), (difference < 0 ? "behind" : "before")), "NTP Request", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Enabled = true;
        }
    }
}
