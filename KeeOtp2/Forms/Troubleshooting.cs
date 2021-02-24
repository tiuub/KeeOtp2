using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using KeePass.Plugins;
using OtpSharp;

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
            // this is kind of an odd flow.

            TimeCorrection timeCorrection = null;

            // set up the asyn operation complete with continuation, catch and finally delegates
            // the async operation will use the synchronization context to post the continuation,
            // catch, and finally delegates to run on the UI thread.  Since we are creating this object
            // on the UI thread it will use the UI synchronization context.
            AsyncOperation getTimeCorrectionOperation = new AsyncOperation(() => // on background threadpool thread
            {
                timeCorrection = Ntp.GetTimeCorrectionFromGoogle();
            },
            () => // continuation on UI thread
            {
                var offset = timeCorrection.CorrectionFactor;

                var totalSeconds = Math.Abs(offset.TotalSeconds);
                if (totalSeconds == 0)
                    MessageBox.Show("Your time is perfect according to Google's servers");
                else if (totalSeconds <= 5)
                    MessageBox.Show("Your time is off by five seconds or less from Google's servers.  You should be just fine.");
                else if (totalSeconds <= 30)
                    MessageBox.Show(string.Format("Your time is off by {0} seconds from Google's servers.  You are probably OK but correcting couldn't hurt.", totalSeconds));
                else
                    MessageBox.Show(string.Format("Your time is off by {0} seconds from Google's servers.  Try correcting the difference and try again.", totalSeconds));
            },
            ex => MessageBox.Show(ex.Message, "Error"), // catch on UI thread
            () => // finally on UI thread
            {
                this.buttonPingGoogle.Visible = true;
                this.progressBarGettingTimeCorrection.Visible = false;
            });


            this.buttonPingGoogle.Visible = false;
            this.progressBarGettingTimeCorrection.Visible = true;

            getTimeCorrectionOperation.Run();
        }

        private void buttonTroubleshootingWebsite_Click(object sender, EventArgs e)
        {
            // go to the troubleshooting page
            var url = "https://bitbucket.org/devinmartin/keeotp/wiki/Troubleshooting";
            Process ps = new Process();
            ps.StartInfo = new ProcessStartInfo(url);
            ps.Start();
        }
    }
}
