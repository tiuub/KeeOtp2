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

            this.host = host;
        }

        private void About_Load(object sender, EventArgs e)
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
                Resources.info_white,
                KeeOtp2Statics.About,
                KeeOtp2Statics.AboutSubline);

            groupBoxDependencies.Text = KeeOtp2Statics.Dependencies;
            linkLabelGitHubRepository.Text = KeeOtp2Statics.GitHubRepository;
            linkLabelDonate.Text = KeeOtp2Statics.Doante;
            buttonOK.Text = KeeOtp2Statics.OK;
            groupBoxAbout.Text = KeeOtp2Statics.About;

            Assembly assembly = Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            labelAbout.Text = String.Format(KeeOtp2Statics.AboutDisclaimer, fvi.FileVersion);

            loadDependencies();
        }

        private void loadDependencies()
        {
            clv_Dependencies.Clear();

            clv_Dependencies.Columns.Add(KeeOtp2Statics.Dependencie, 100);
            clv_Dependencies.Columns.Add(KeeOtp2Statics.Author, 80);
            clv_Dependencies.Columns.Add(KeeOtp2Statics.License, 80);

            ListViewItem lvi = new ListViewItem("KeeOtp(1)");
            lvi.SubItems.Add("Devin Martin");
            lvi.SubItems.Add("MIT");
            lvi.Tag = Resources.KeeOtpLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("Otp.NET");
            lvi.SubItems.Add("Kyle Spearrin");
            lvi.SubItems.Add("MIT");
            lvi.Tag = Resources.OtpNetLICENSE;
            clv_Dependencies.Items.Add(lvi);
            
            lvi = new ListViewItem("Yort.Ntp.Portable");
            lvi.SubItems.Add("Troy Willmot");
            lvi.SubItems.Add("MIT");
            lvi.Tag = Resources.YortNtpPortableLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("ZXing.Net");
            lvi.SubItems.Add("Michael Jahn");
            lvi.SubItems.Add("Apache 2.0");
            lvi.Tag = Resources.ZXingNetLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("NHotkey");
            lvi.SubItems.Add("Thomas Levesque");
            lvi.SubItems.Add("Apache 2.0");
            lvi.Tag = Resources.NHotkeyLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("NHotkey.WindowsForms");
            lvi.SubItems.Add("Thomas Levesque");
            lvi.SubItems.Add("Apache 2.0");
            lvi.Tag = Resources.NHotkeyWindowsFormsLICENSE;
            clv_Dependencies.Items.Add(lvi);

            lvi = new ListViewItem("Material Icons");
            lvi.SubItems.Add("Google");
            lvi.SubItems.Add("Apache License Version 2.0");
            lvi.Tag = Resources.MaterialDesignIconsLICENSE;
            clv_Dependencies.Items.Add(lvi);
        }

        private void linkLabelGitHubRepository_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(KeeOtp2Statics.RepositoryLicenseLink);
            linkLabelGitHubRepository.LinkVisited = true;
        }

        private void linkLabelDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(KeeOtp2Statics.DonateLink);
            linkLabelDonate.LinkVisited = true;
        }

        private void clv_Dependencies_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = clv_Dependencies.SelectedItems[0];
            Point mousePosition = clv_Dependencies.PointToClient(Control.MousePosition);
            ListViewHitTestInfo hit = clv_Dependencies.HitTest(mousePosition);
            int columnindex = hit.Item.SubItems.IndexOf(hit.SubItem);

            if (columnindex < 2)
            {
                if (MessageBox.Show(KeeOtp2Statics.AboutMessageBoxOpenRepository, KeeOtp2Statics.Dependencies, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(KeeOtp2Statics.RepositoryLicenseLink);

            }
            else
            {
                if (groupBoxLicense != null)
                    hideLicense();
                if (lvi.Tag != null)
                {
                    showLicense(lvi.Text, lvi.SubItems[1].Text, lvi.Tag.ToString());
                }
                else
                    MessageBox.Show(String.Format(KeeOtp2Statics.AboutMessageBoxCantLoadLicense, lvi.Text), KeeOtp2Statics.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showLicense(string dependencie, string author, string license)
        {
            groupBoxDependencies.Anchor -= AnchorStyles.Bottom;
            int groupBoxLicenseHeight = 101;
            this.Height = this.Height + groupBoxLicenseHeight + groupBoxDependencies.Margin.All * 2;

            groupBoxLicense = new GroupBox();
            groupBoxLicense.Text = String.Format("{0} - {1} by {2}", KeeOtp2Statics.License, dependencie, author);
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
