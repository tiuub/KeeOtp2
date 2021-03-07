using KeeOtp2.Properties;
using KeePass.Plugins;
using System;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace KeeOtp2
{
    public partial class About : Form
    {
        private IPluginHost host;

        private GroupBox groupBoxLicense;

        public About(IPluginHost host)
        {
            InitializeComponent();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.info_white,
                "About",
                "KeeOtp2 Plugin.");

            this.Icon = host.MainWindow.Icon;

            this.host = host;
            this.TopMost = host.MainWindow.TopMost;
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;

            Assembly assembly = Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            labelAbout.Text = new StringBuilder(labelAbout.Text).Replace("{VERSION}", fvi.FileVersion).ToString();

            loadDependencies();
        }

        private void loadDependencies()
        {
            clv_Dependencies.Clear();

            clv_Dependencies.Columns.Add("Package", 100);
            clv_Dependencies.Columns.Add("Author", 80);
            clv_Dependencies.Columns.Add("License", 80);

            ListViewItem lvi = new ListViewItem("KeeOtp(1)");
            lvi.SubItems.Add("devinmartin");
            lvi.SubItems.Add("MIT");
            lvi.Tag = Resources.KeeOtpLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("OtpSharp");
            lvi.SubItems.Add("devinmartin");
            lvi.SubItems.Add("MIT");
            lvi.Tag = Resources.OtpSharpLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("Yort.Ntp.Portable");
            lvi.SubItems.Add("Yortw");
            lvi.SubItems.Add("MIT");
            lvi.Tag = Resources.YortNtpPortableLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("ZXing.Net");
            lvi.SubItems.Add("micjahn");
            lvi.SubItems.Add("Apache 2.0");
            lvi.Tag = Resources.ZXingNetLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("Microsoft.Xaml");
            lvi.SubItems.Add("karelz & bmarshall");
            lvi.SubItems.Add("None");
            lvi.Tag = null;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("Material Icons");
            lvi.SubItems.Add("Google");
            lvi.SubItems.Add("Apache License Version 2.0");
            lvi.Tag = Resources.MaterialDesignIconsLICENSE;
            clv_Dependencies.Items.Add(lvi);
        }

        private void llbl_Donate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url");
            llbl_Donate.LinkVisited = true;
        }

        private void llbl_GitHubRepository_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/tiuub/KeeOtp2");
            llbl_GitHubRepository.LinkVisited = true;
        }

        private void clv_Dependencies_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = clv_Dependencies.SelectedItems[0];
            Point mousePosition = clv_Dependencies.PointToClient(Control.MousePosition);
            ListViewHitTestInfo hit = clv_Dependencies.HitTest(mousePosition);
            int columnindex = hit.Item.SubItems.IndexOf(hit.SubItem);

            if (columnindex < 2)
            {
                if (MessageBox.Show("The GitHub Repository will now be opened.\nYou can open the ReadMe and scroll down, until you see Dependencies. There you will find references to the source code, the author and the license of the dependencies.\n\nDo you want to continue?", "Dependencies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    System.Diagnostics.Process.Start("https://github.com/tiuub/KeeOtp2");

            }
            else
            {
                if (groupBoxLicense != null)
                    hideLicense();
                if (lvi.Tag != null)
                {
                    showLicense(lvi.Text, lvi.Tag.ToString());
                }
                else
                    MessageBox.Show(String.Format("Cant load license of {0}.\n\nJust try to open the GitHub Repository and scroll down, until Dependencies. There are all licenses!", lvi.Text), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showLicense(string dependencie, string license)
        {
            groupBoxDependencies.Anchor -= AnchorStyles.Bottom;
            int groupBoxLicenseHeight = 101;
            this.Height = this.Height + groupBoxLicenseHeight + groupBoxDependencies.Margin.All * 2;

            groupBoxLicense = new GroupBox();
            groupBoxLicense.Text = "License - " + dependencie;
            groupBoxLicense.Left = groupBoxDependencies.Left;
            groupBoxLicense.Top = groupBoxDependencies.Bottom + groupBoxDependencies.Margin.All * 2;
            groupBoxLicense.Width = groupBoxDependencies.Width;
            groupBoxLicense.Height = groupBoxLicenseHeight;
            groupBoxLicense.Margin = groupBoxDependencies.Margin;

            RichTextBox richTextBoxLicense = new RichTextBox();
            richTextBoxLicense.ReadOnly = true;
            richTextBoxLicense.Dock = DockStyle.Fill;
            richTextBoxLicense.Text = license;

            groupBoxLicense.Controls.Add(richTextBoxLicense);

            this.Controls.Add(groupBoxLicense);
            groupBoxDependencies.Anchor -= AnchorStyles.Bottom;
        }

        private void hideLicense()
        {
            groupBoxDependencies.Anchor -= AnchorStyles.Bottom;

            int groupBoxLicenseHeight = groupBoxLicense.Height;
            this.Height = this.Height - groupBoxLicenseHeight - groupBoxDependencies.Margin.All * 2;

            this.Controls.Remove(groupBoxLicense);

            groupBoxLicense = null;

            groupBoxDependencies.Anchor -= AnchorStyles.Bottom;
        }
    }
}
