namespace KeeOtp2
{
    partial class About
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.groupBoxAbout = new System.Windows.Forms.GroupBox();
            this.labelAbout = new System.Windows.Forms.Label();
            this.labelDisclaimer = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.llbl_Donate = new System.Windows.Forms.LinkLabel();
            this.llbl_GitHubRepository = new System.Windows.Forms.LinkLabel();
            this.groupBoxDependencies = new System.Windows.Forms.GroupBox();
            this.clv_Dependencies = new KeePass.UI.CustomListViewEx();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.groupBoxAbout.SuspendLayout();
            this.groupBoxDependencies.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(327, 50);
            this.pictureBoxBanner.TabIndex = 1;
            this.pictureBoxBanner.TabStop = false;
            // 
            // groupBoxAbout
            // 
            this.groupBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAbout.Controls.Add(this.labelAbout);
            this.groupBoxAbout.Controls.Add(this.labelDisclaimer);
            this.groupBoxAbout.Location = new System.Drawing.Point(12, 56);
            this.groupBoxAbout.Name = "groupBoxAbout";
            this.groupBoxAbout.Size = new System.Drawing.Size(303, 101);
            this.groupBoxAbout.TabIndex = 3;
            this.groupBoxAbout.TabStop = false;
            this.groupBoxAbout.Text = "About";
            // 
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.Location = new System.Drawing.Point(63, 16);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(176, 78);
            this.labelAbout.TabIndex = 0;
            this.labelAbout.Text = "KeeOtp2 by tiuub.\r\nVersion: {VERSION}\r\nLicense: MIT\r\n\r\nKeeOtp2 is based on KeeOtp" +
    "(1).\r\nOriginally developed by devinmartin.";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDisclaimer
            // 
            this.labelDisclaimer.AutoSize = true;
            this.labelDisclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisclaimer.Location = new System.Drawing.Point(17, 171);
            this.labelDisclaimer.Name = "labelDisclaimer";
            this.labelDisclaimer.Size = new System.Drawing.Size(264, 26);
            this.labelDisclaimer.TabIndex = 1;
            this.labelDisclaimer.Text = "(*Links to the source codes and the licenses of the\r\ndependencies can be found on" +
    " the GitHub Repository.)";
            this.labelDisclaimer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(240, 313);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // llbl_Donate
            // 
            this.llbl_Donate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llbl_Donate.AutoSize = true;
            this.llbl_Donate.Location = new System.Drawing.Point(192, 318);
            this.llbl_Donate.Name = "llbl_Donate";
            this.llbl_Donate.Size = new System.Drawing.Size(42, 13);
            this.llbl_Donate.TabIndex = 7;
            this.llbl_Donate.TabStop = true;
            this.llbl_Donate.Text = "Donate";
            this.llbl_Donate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl_Donate_LinkClicked);
            // 
            // llbl_GitHubRepository
            // 
            this.llbl_GitHubRepository.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llbl_GitHubRepository.AutoSize = true;
            this.llbl_GitHubRepository.Location = new System.Drawing.Point(12, 318);
            this.llbl_GitHubRepository.Name = "llbl_GitHubRepository";
            this.llbl_GitHubRepository.Size = new System.Drawing.Size(93, 13);
            this.llbl_GitHubRepository.TabIndex = 8;
            this.llbl_GitHubRepository.TabStop = true;
            this.llbl_GitHubRepository.Text = "GitHub Repository";
            this.llbl_GitHubRepository.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl_GitHubRepository_LinkClicked);
            // 
            // groupBoxDependencies
            // 
            this.groupBoxDependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDependencies.Controls.Add(this.clv_Dependencies);
            this.groupBoxDependencies.Location = new System.Drawing.Point(12, 163);
            this.groupBoxDependencies.Name = "groupBoxDependencies";
            this.groupBoxDependencies.Size = new System.Drawing.Size(303, 144);
            this.groupBoxDependencies.TabIndex = 9;
            this.groupBoxDependencies.TabStop = false;
            this.groupBoxDependencies.Text = "Dependencies";
            // 
            // clv_Dependencies
            // 
            this.clv_Dependencies.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.clv_Dependencies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clv_Dependencies.FullRowSelect = true;
            this.clv_Dependencies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.clv_Dependencies.HideSelection = false;
            this.clv_Dependencies.HotTracking = true;
            this.clv_Dependencies.HoverSelection = true;
            this.clv_Dependencies.Location = new System.Drawing.Point(6, 19);
            this.clv_Dependencies.MultiSelect = false;
            this.clv_Dependencies.Name = "clv_Dependencies";
            this.clv_Dependencies.Size = new System.Drawing.Size(291, 114);
            this.clv_Dependencies.TabIndex = 0;
            this.clv_Dependencies.UseCompatibleStateImageBehavior = false;
            this.clv_Dependencies.View = System.Windows.Forms.View.Details;
            this.clv_Dependencies.Click += new System.EventHandler(this.clv_Dependencies_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 348);
            this.Controls.Add(this.groupBoxDependencies);
            this.Controls.Add(this.llbl_GitHubRepository);
            this.Controls.Add(this.llbl_Donate);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxAbout);
            this.Controls.Add(this.pictureBoxBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.groupBoxAbout.ResumeLayout(false);
            this.groupBoxAbout.PerformLayout();
            this.groupBoxDependencies.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBanner;
        private System.Windows.Forms.GroupBox groupBoxAbout;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.LinkLabel llbl_Donate;
        private System.Windows.Forms.Label labelDisclaimer;
        private System.Windows.Forms.LinkLabel llbl_GitHubRepository;
        private System.Windows.Forms.GroupBox groupBoxDependencies;
        private KeePass.UI.CustomListViewEx clv_Dependencies;
    }
}