using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Linq;
using KeeOtp2.Properties;
using KeePass.Plugins;
using OtpNet;
using ZXing;
using KeePassLib;

namespace KeeOtp2
{
    public partial class OtpInformation : Form
    {
        private IPluginHost host;
        private PwEntry entry;
        private OtpAuthData data;

        bool scanQRMode = true;

        private Dictionary<int, int> comboBoxLengthIndexValue;
        private Dictionary<int, OtpType> comboBoxTypeIndexValue;

        public OtpInformation(IPluginHost host, PwEntry entry, OtpAuthData data)
        {
            InitializeComponent();

            this.host = host;
            this.entry = entry;
            this.data = data;
        }

        private void OtpInformation_Load(object sender, EventArgs e)
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
                Resources.lock_white,
                KeeOtp2Statics.OtpInformation,
                KeeOtp2Statics.OtpInformationSubline);

            groupBoxKey.Text = KeeOtp2Statics.OtpInformationKeyUri;
            linkLabelLoadUriScanQR.Text = KeeOtp2Statics.OtpInformationScanQr;
            checkBoxCustomSettings.Text = KeeOtp2Statics.OtpInformationCustomSettings + KeeOtp2Statics.InformationChar;
            linkLabelMigrate.Text = KeeOtp2Statics.OtpInformationMigrate + KeeOtp2Statics.InformationChar;
            groupBoxPeriodCounter.Text = KeeOtp2Statics.Period;
            labelPeriodCounter.Text = KeeOtp2Statics.OtpInformationPeriodSeconds;
            groupboxInfo.Text = KeeOtp2Statics.OtpInformationKeeOtp1String;
            checkboxOldKeeOtp.Text = KeeOtp2Statics.OtpInformationKeeOtp1SaveMode + KeeOtp2Statics.InformationChar;
            groupboxGeneral.Text = KeeOtp2Statics.General;
            labelLength.Text = KeeOtp2Statics.Length;
            labelType.Text = KeeOtp2Statics.Type;
            groupboxEncoding.Text = KeeOtp2Statics.Encoding;
            radioButtonBase32.Text = KeeOtp2Statics.Base32;
            radioButtonBase64.Text = KeeOtp2Statics.Base64;
            radioButtonHex.Text = KeeOtp2Statics.Hex;
            radioButtonUtf8.Text = KeeOtp2Statics.Utf8;
            groupboxHashAlgorithm.Text = KeeOtp2Statics.HashAlgorithm;
            radioButtonSha1.Text = KeeOtp2Statics.Sha1;
            radioButtonSha256.Text = KeeOtp2Statics.Sha256;
            radioButtonSha512.Text = KeeOtp2Statics.Sha512;
            labelStatus.Text = KeeOtp2Statics.HoverInformation;
            buttonOk.Text = KeeOtp2Statics.OK;
            buttonCancel.Text = KeeOtp2Statics.Cancel;

            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = KeeOtp2Statics.OtpInformation;
            toolTip.IsBalloon = true;
            toolTip.SetToolTip(checkBoxCustomSettings, KeeOtp2Statics.ToolTipOtpInformationUseCustomSettings);

            toolTip.ToolTipTitle = KeeOtp2Statics.OtpInformation;
            toolTip.IsBalloon = true;
            toolTip.SetToolTip(checkboxOldKeeOtp, KeeOtp2Statics.ToolTipOtpInformationUseOldKeeOtpSaveMode);

            comboBoxLengthIndexValue = new Dictionary<int, int>();
            for (int i = 5; i <= 10; i++)
            {
                if (i == 6 || i == 8)
                    comboBoxLengthIndexValue[comboBoxLength.Items.Add(String.Format("{0} ({1})", i, KeeOtp2Statics.CommonAbbreviation.ToLower() + KeeOtp2Statics.InformationChar))] = i;
                else
                    comboBoxLengthIndexValue[comboBoxLength.Items.Add(i.ToString())] = i;
            }
            comboBoxTypeIndexValue = new Dictionary<int, OtpType>();
            foreach (OtpType type in Enum.GetValues(typeof(OtpType)))
            {
                comboBoxTypeIndexValue[comboBoxType.Items.Add(type.ToString())] = type;
            }
        }

        private void OtpInformation_Shown(object sender, EventArgs e)
        {
            loadData();
            timerUpdateTotp.Start();
        }

        private void OtpInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
                return;
            try
            {
                this.data = readData();

                this.entry.CreateBackup(this.host.Database);

                if (this.data.loadedFields != null && !string.IsNullOrEmpty(OtpAuthUtils.getOtpString(this.data)))
                    OtpAuthUtils.purgeLoadedFields(this.data, this.entry);

                if (this.data.KeeOtp1Mode)
                {
                    OtpAuthUtils.migrateToKeeOtp1String(this.data, this.entry);
                }
                else
                {
                    OtpAuthUtils.migrateToBuiltInOtp(this.data, this.entry);
                }
                

                this.entry.Touch(true, false);
                this.host.MainWindow.ActiveDatabase.Modified = true;
                this.host.MainWindow.UpdateUI(false, null, false, null, false, null, true);
                this.host.MainWindow.RefreshEntriesList();
            }
            catch (InvalidBase32FormatException ex)
            {
                MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidBase32Format, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }
            catch (InvalidBase64FormatException ex)
            {
                MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidBase64Format, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }
            catch (InvalidHexFormatException ex)
            {
                MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidHexFormat, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }
            catch (InvalidUriFormat ex)
            {
                MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidUriFormat, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            catch (InvalidOtpConfiguration ex)
            {
                MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidOtpConfiguration, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxException, ex.Message), KeeOtp2Statics.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
        }

        private void linkLabelLoadUriScanQR_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (scanQRMode)
            {
                if (MessageBox.Show(KeeOtp2Statics.MessageBoxScanQrCode, KeeOtp2Statics.OtpInformationScanQr, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    scanQRCode();
                }

            }
            else
            {
                try
                {
                    this.data = OtpAuthUtils.uriToOtpAuthData(new Uri(textBoxKey.Text));
                    loadData();
                }
                catch (InvalidBase32FormatException ex)
                {
                    MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidBase32Format, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (InvalidUriFormat ex)
                {
                    MessageBox.Show(ex.Message, KeeOtp2Statics.InvalidUriFormat, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxException, ex.Message), KeeOtp2Statics.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.DialogResult = DialogResult.OK;
        }

        private void textBoxKey_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKey.Text.StartsWith("otpauth://"))
            {
                linkLabelLoadUriScanQR.Text = KeeOtp2Statics.OtpInformationLoadUri;
                scanQRMode = false;
            }
            else
            {
                linkLabelLoadUriScanQR.Text = KeeOtp2Statics.OtpInformationScanQr;
                scanQRMode = true;
            }
        }

        private void checkBoxCustomSettings_CheckedChanged(object sender, EventArgs e)
        {
            SetCustomSettingsState();
        }

        private void linkLabelMigrate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show(String.Format(KeeOtp2Statics.MessageBoxMigrationReplacePlaceholder, KeeOtp2Ext.KeeOtp1PlaceHolder, string.Format("{0}/{1}", KeeOtp2Ext.BuiltInTotpPlaceHolder, KeeOtp2Ext.BuiltInHotpPlaceHolder)), KeeOtp2Statics.Migration, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (data.Type == OtpType.Totp)
                    OtpAuthUtils.replacePlaceholder(this.entry, KeeOtp2Ext.KeeOtp1PlaceHolder, KeeOtp2Ext.BuiltInTotpPlaceHolder);
                else if (data.Type == OtpType.Hotp)
                    OtpAuthUtils.replacePlaceholder(this.entry, KeeOtp2Ext.KeeOtp1PlaceHolder, KeeOtp2Ext.BuiltInHotpPlaceHolder);
            }

            checkboxOldKeeOtp.Checked = false;
            this.DialogResult = DialogResult.OK;
        }

        private void comboBoxLength_DropDown(object sender, EventArgs e)
        {
            labelStatus.Text = KeeOtp2Statics.OtpInformationCommonAbbreviationExplanation;
        }

        private void comboBoxLength_DropDownClosed(object sender, EventArgs e)
        {
            labelStatus.Text = KeeOtp2Statics.HoverInformation;
        }

        private void comboBoxType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool timerEnabled = timerUpdateTotp.Enabled;
            timerUpdateTotp.Stop();
            timerUpdateTotp.Dispose();
            OtpType type = comboBoxTypeIndexValue[comboBoxType.SelectedIndex];

            try
            {
                this.data = readData(true);
            }
            catch
            {
                this.data = new OtpAuthData();
            }

            if (type == OtpType.Totp)
            {
                this.data.Proprietary = true;
                this.data.Type = OtpType.Totp;
            }
            else if (type == OtpType.Hotp)
            {
                this.data.Proprietary = true;
                this.data.Digits = 6;
                this.data.Type = OtpType.Hotp;
            }
            else if (type == OtpType.Steam)
            {
                this.data.Proprietary = false;
                this.data.Digits = 5;
                this.data.KeeOtp1Mode = true;
                this.data.Type = OtpType.Steam;
            }

            loadData();
            timerUpdateTotp.Enabled = timerEnabled;
        }

        private void timerUpdateTotp_Tick(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length > 0)
            {
                try
                {
                    OtpAuthData data = readData();
                    OtpBase otp = OtpAuthUtils.getOtp(data);
                    if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                    {
                        groupBoxKey.Text = String.Format(KeeOtp2Statics.OtpInformationKeyUriTotpPreview, otp.getTotpString(OtpTime.getTime()), otp.getRemainingSeconds(OtpTime.getTime()));
                    }
                    else if (data.Type == OtpType.Hotp)
                    {

                        groupBoxKey.Text = String.Format(KeeOtp2Statics.OtpInformationKeyUriHotpPreview, otp.getHotpString(data.Counter));
                    }
                }
                catch
                {
                    groupBoxKey.Text = KeeOtp2Statics.OtpInformationKeyUriInvalid;
                }
            }
            else
            {
                groupBoxKey.Text = KeeOtp2Statics.OtpInformationKeyUri;
            }
        }

        private OtpAuthData readData(bool skipType = false)
        {
            if (OtpAuthUtils.checkUriString(textBoxKey.Text))
            {
                OtpAuthData data = OtpAuthUtils.uriToOtpAuthData(new Uri(textBoxKey.Text));
                if (this.data != null)
                    data.loadedFields = this.data.loadedFields;
                return data;
            }
            else
            {
                OtpAuthData data = new OtpAuthData();

                if (this.data != null)
                    data = (OtpAuthData)this.data.Clone();

                string secret = textBoxKey.Text.Replace(" ", string.Empty).Replace("-", string.Empty);
                if (string.IsNullOrEmpty(this.textBoxKey.Text))
                    throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationMissingSecret);

                if (checkBoxCustomSettings.Checked)
                {
                    if (this.radioButtonBase32.Checked)
                        data.Encoding = OtpSecretEncoding.Base32;
                    else if (this.radioButtonBase64.Checked)
                        data.Encoding = OtpSecretEncoding.Base64;
                    else if (this.radioButtonHex.Checked)
                        data.Encoding = OtpSecretEncoding.Hex;
                    else if (this.radioButtonUtf8.Checked)
                        data.Encoding = OtpSecretEncoding.UTF8;
                }

                // Secret validation, will throw an error if invalid
                secret = OtpAuthUtils.correctPlainSecret(secret, data.Encoding);
                OtpAuthUtils.validatePlainSecret(secret, data.Encoding);

                if (checkBoxCustomSettings.Checked)
                {
                    if (!skipType)
                        data.Type = comboBoxTypeIndexValue[comboBoxType.SelectedIndex];

                    if (data.Type == OtpType.Totp || data.Type == OtpType.Steam)
                    {
                        int period = 30;
                        if (int.TryParse(this.textBoxPeriodCounter.Text, out period))
                        {
                            if (period <= 0)
                                throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationInvalidInteger);
                        }
                        else
                            throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationInvalidInteger);
                        data.Period = period;
                    }
                    else if (data.Type == OtpType.Hotp)
                    {
                        int counter = 0;
                        if (int.TryParse(this.textBoxPeriodCounter.Text, out counter))
                        {
                            if (counter < 0)
                                throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationInvalidInteger);
                        }
                        else
                            throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationInvalidInteger);
                        data.Counter = counter;
                    }

                    data.Digits = comboBoxLengthIndexValue[comboBoxLength.SelectedIndex];

                    if (this.radioButtonSha1.Checked)
                        data.Algorithm = OtpHashMode.Sha1;
                    else if (this.radioButtonSha256.Checked)
                        data.Algorithm = OtpHashMode.Sha256;
                    else if (this.radioButtonSha512.Checked)
                        data.Algorithm = OtpHashMode.Sha512;

                    data.KeeOtp1Mode = checkboxOldKeeOtp.Checked;
                }

                data.SetPlainSecret(secret);

                return data;
            }
        }

        private void loadData()
        {
            bool timerEnabled = timerUpdateTotp.Enabled;
            bool comboBoxLengthEnabled = comboBoxLength.Enabled;
            bool comboBoxTypeEnabled = comboBoxType.Enabled;
            timerUpdateTotp.Enabled = false;
            comboBoxLength.Enabled = false;
            comboBoxType.Enabled = false;

            if (this.data == null)
                this.data = new OtpAuthData();

            this.textBoxKey.Text = this.data.GetPlainSecret();

            if (this.data.Period != 30 ||
                this.data.KeeOtp1Mode ||
                this.data.Encoding != OtpSecretEncoding.Base32 ||
                this.data.Digits != 6 ||
                this.data.Type != OtpType.Totp ||
                this.data.Algorithm != OtpHashMode.Sha1)
            {
                this.checkBoxCustomSettings.Checked = true;
            }

            if (this.data.Type == OtpType.Totp || this.data.Type == OtpType.Steam)
            {
                this.textBoxPeriodCounter.Text = this.data.Period.ToString();
                groupBoxPeriodCounter.Text = KeeOtp2Statics.Period;
                labelPeriodCounter.Text = KeeOtp2Statics.OtpInformationPeriodSeconds;
            }
            else if (this.data.Type == OtpType.Hotp)
            {
                this.textBoxPeriodCounter.Text = this.data.Counter.ToString();
                groupBoxPeriodCounter.Text = KeeOtp2Statics.Counter;
                labelPeriodCounter.Text = KeeOtp2Statics.Counter;
            }

            this.checkboxOldKeeOtp.Checked = this.data.KeeOtp1Mode;

            this.comboBoxLength.SelectedIndex = comboBoxLengthIndexValue.FirstOrDefault(x => x.Value == this.data.Digits).Key;
            this.comboBoxType.SelectedIndex = comboBoxTypeIndexValue.FirstOrDefault(x => x.Value == this.data.Type).Key;

            if (this.data.Encoding == OtpSecretEncoding.Base64)
            {
                this.radioButtonBase32.Checked = false;
                this.radioButtonBase64.Checked = true;
                this.radioButtonHex.Checked = false;
                this.radioButtonUtf8.Checked = false;
            }
            else if (this.data.Encoding == OtpSecretEncoding.Hex)
            {
                this.radioButtonBase32.Checked = false;
                this.radioButtonBase64.Checked = false;
                this.radioButtonHex.Checked = true;
                this.radioButtonUtf8.Checked = false;
            }
            else if (this.data.Encoding == OtpSecretEncoding.UTF8)
            {
                this.radioButtonBase32.Checked = false;
                this.radioButtonBase64.Checked = false;
                this.radioButtonHex.Checked = false;
                this.radioButtonUtf8.Checked = true;
            }
            else // default encoding
            {
                this.radioButtonBase32.Checked = true;
                this.radioButtonBase64.Checked = false;
                this.radioButtonHex.Checked = false;
                this.radioButtonUtf8.Checked = false;

            }

            if (this.data.Algorithm == OtpHashMode.Sha256)
            {
                this.radioButtonSha1.Checked = false;
                this.radioButtonSha256.Checked = true;
                this.radioButtonSha512.Checked = false;
            }
            else if (this.data.Algorithm == OtpHashMode.Sha512)
            {
                this.radioButtonSha1.Checked = false;
                this.radioButtonSha256.Checked = false;
                this.radioButtonSha512.Checked = true;
            }
            else // default hashmode
            {
                this.radioButtonSha1.Checked = true;
                this.radioButtonSha256.Checked = false;
                this.radioButtonSha512.Checked = false;
            }

            if (this.data != null && this.data.KeeOtp1Mode && !this.data.isForcedKeeOtp1Mode())
            {
                linkLabelMigrate.Visible = true;
                linkLabelMigrate.Enabled = true;
                ToolTip toolTip = new ToolTip();
                toolTip.ToolTipTitle = KeeOtp2Statics.ToolTipMigrateHeadline;
                toolTip.IsBalloon = true;
                toolTip.SetToolTip(linkLabelMigrate, KeeOtp2Statics.ToolTipMigrate);
            }
            else
            {
                linkLabelMigrate.Visible = false;
                linkLabelMigrate.Enabled = false;
            }

            timerUpdateTotp.Enabled = timerEnabled;
            comboBoxLength.Enabled = comboBoxLengthEnabled;
            comboBoxType.Enabled = comboBoxTypeEnabled;

            SetCustomSettingsState();
        }


        private void SetCustomSettingsState()
        {
            var useCustom = this.checkBoxCustomSettings.Checked;

            this.radioButtonBase32.Enabled =
                this.radioButtonBase64.Enabled =
                this.radioButtonHex.Enabled =
                this.radioButtonUtf8.Enabled =
                this.comboBoxLength.Enabled =
                this.comboBoxType.Enabled =
                this.textBoxPeriodCounter.Enabled =
                this.radioButtonSha1.Enabled =
                this.radioButtonSha256.Enabled =
                this.radioButtonSha512.Enabled = useCustom;

            if (this.data != null && this.data.isForcedKeeOtp1Mode())
                this.checkboxOldKeeOtp.Enabled = false;
            else
                this.checkboxOldKeeOtp.Enabled = useCustom;
        }


        private void scanQRCode()
        {
            Uri uri = null;
            // Bitmap bmpScreenshot;
            // Graphics gfxScreenshot;

            Form p = this;
            while (p != null)
            {
                p.Hide();
                p.Opacity = 0;
                p = p.Owner;
            }
            
            // Wait until window is closed entirely
            System.Threading.Thread.Sleep(500);

            // int i = 0;
            // foreach (Screen sc in Screen.AllScreens)
            // {
            //     bmpScreenshot = new Bitmap(sc.Bounds.Width, sc.Bounds.Height, PixelFormat.Format32bppArgb);
            //     gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            //     gfxScreenshot.CopyFromScreen(sc.Bounds.X, sc.Bounds.Y, 0, 0, sc.Bounds.Size, CopyPixelOperation.SourceCopy);
            //     try
            //     {
            //         uri = OtpAuthUtils.bitmapToUri(bmpScreenshot);
            //         if (uri != null)
            //         {
            //             break;
            //         }
            //     }
            //     catch (CouldNotFindValidUri) { }
            //     i++;
            // }
            int i = 0;
            foreach (Screen sc in Screen.AllScreens)
            {
                float scaleX = ScreenUtils.getDpiScale(sc);
                float scaleY = scaleX;

                int width = sc.Bounds.Width;
                int height = sc.Bounds.Height;
                int screenshotWidth = (int)(width / scaleX);
                int screenshotHeight = (int)(height / scaleY);
                // using (Bitmap bmpScreenshot = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                using (var screenshot = new Bitmap(screenshotWidth, screenshotHeight, PixelFormat.Format32bppArgb))
                {
                    // string filePath;
                    using (var graphics = Graphics.FromImage(screenshot))
                    {
                        // float scaleX = 1;
                        // float scaleY = 1;
                        int sourceX = (int)(sc.Bounds.X / scaleX);
                        int sourceY = (int)(sc.Bounds.Y / scaleY);
                        // int sourceX = sc.Bounds.X;
                        // int sourceY = sc.Bounds.Y;
                        // int width = (int)(sc.Bounds.Width * scaleX);
                        // int height = (int)(sc.Bounds.Height * scaleY);
                        var size = new Size(width, height);
                        graphics.CopyFromScreen(sourceX, sourceY, 0, 0, size, CopyPixelOperation.SourceCopy);
                        // filePath = "screenshot_" + i + "_" + scaleX + "_" + sc.Bounds.Width + "x" + sc.Bounds.Height
                        //         + "_" + sourceX + "," + sourceY
                        //         + "-" + width + "," + height + ".png";
                    }
                    // filePath = "screenshot_" + i + "_" + sc.Bounds.Size.Width + "x" + sc.Bounds.Size.Height + ".png";
                    // screenshot.Save(filePath, ImageFormat.Png);
                    try
                    {
                        uri = OtpAuthUtils.bitmapToUri(screenshot);
                        if (uri != null)
                        {
                            break;
                        }
                    }
                    catch (CouldNotFindValidUri) { }
                }
                i++;
            }

            p = this;
            while (p != null)
            {
                p.Show();
                p.Opacity = 1;
                p = p.Owner;
            }

            if (uri != null)
            {
                MessageBox.Show(KeeOtp2Statics.MessageBoxQrCodeFound, KeeOtp2Statics.OtpInformationScanQr, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.data = OtpAuthUtils.uriToOtpAuthData(uri);
                loadData();
            }
            else
            {
                if (MessageBox.Show(KeeOtp2Statics.MessageBoxQrCodeNotFound, KeeOtp2Statics.OtpInformationScanQr, MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry)
                {
                    scanQRCode();
                }
            }
        }
    }
}
