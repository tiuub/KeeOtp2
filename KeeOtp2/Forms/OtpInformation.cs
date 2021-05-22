using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using KeeOtp2.Properties;
using KeePass.Plugins;
using OtpSharp;
using ZXing;

namespace KeeOtp2
{
    public partial class OtpInformation : Form
    {
        public OtpAuthData Data { get; set; }
        bool fullyLoaded = false;
        bool scanQRMode = true;
        KeePassLib.PwEntry entry;
        IPluginHost host;

        public OtpInformation(OtpAuthData data, KeePassLib.PwEntry entry, IPluginHost host)
        {
            InitializeComponent();
            this.Shown += (sender, e) => FormWasShown();

            pictureBoxBanner.Image = KeePass.UI.BannerFactory.CreateBanner(pictureBoxBanner.Width,
                pictureBoxBanner.Height,
                KeePass.UI.BannerStyle.Default,
                Resources.lock_white,
                KeeOtp2Statics.OtpInformation,
                KeeOtp2Statics.OtpInformationSubline);

            this.Icon = host.MainWindow.Icon;

            this.Data = data;
            this.entry = entry;
            this.host = host;

            this.TopMost = host.MainWindow.TopMost;

            groupBoxKey.Text = KeeOtp2Statics.OtpInformationKeyUri;
            linkLabelLoadUriScanQR.Text = KeeOtp2Statics.OtpInformationScanQr;
            checkBoxCustomSettings.Text = KeeOtp2Statics.OtpInformationCustomSettings + KeeOtp2Statics.InformationChar;
            linkLabelMigrate.Text = KeeOtp2Statics.OtpInformationMigrate + KeeOtp2Statics.InformationChar;
            groupboxTimeStep.Text = KeeOtp2Statics.TimeStep;
            labelStep.Text = KeeOtp2Statics.OtpInformationTimeStepSeconds;
            groupboxInfo.Text = KeeOtp2Statics.OtpInformationKeeOtp1String;
            checkboxOldKeeOtp.Text = KeeOtp2Statics.OtpInformationKeeOtp1SaveMode + KeeOtp2Statics.InformationChar;
            groupboxEncoding.Text = KeeOtp2Statics.Encoding;
            radioButtonBase32.Text = KeeOtp2Statics.Base32;
            radioButtonBase64.Text = KeeOtp2Statics.Base64;
            radioButtonHex.Text = KeeOtp2Statics.Hex;
            radioButtonUtf8.Text = KeeOtp2Statics.Utf8;
            groupboxSize.Text = KeeOtp2Statics.Size;
            radioButtonSix.Text = KeeOtp2Statics.SixDigits;
            radioButtonEight.Text = KeeOtp2Statics.EightDigits;
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

            if (this.Data != null && this.Data.KeeOtp1Mode)
            {
                linkLabelMigrate.Visible = true;
                linkLabelMigrate.Enabled = true;
                toolTip.ToolTipTitle = KeeOtp2Statics.ToolTipMigrateHeadline;
                toolTip.IsBalloon = true;
                toolTip.SetToolTip(linkLabelMigrate, KeeOtp2Statics.ToolTipMigrate);
            }

            toolTip.ToolTipTitle = KeeOtp2Statics.OtpInformation;
            toolTip.IsBalloon = true;
            toolTip.SetToolTip(checkboxOldKeeOtp, KeeOtp2Statics.ToolTipOtpInformationUseOldKeeOtpSaveMode);
        }

        private void OtpInformation_Load(object sender, EventArgs e)
        {
            this.Left = this.host.MainWindow.Left + 20;
            this.Top = this.host.MainWindow.Top + 20;

            timerUpdateTotp.Start();
        }

        private void FormWasShown()
        {
            loadData();
        }

        private OtpAuthData readData()
        {
            if (OtpAuthUtils.checkUriString(textBoxKey.Text))
            {
                return OtpAuthUtils.uriToOtpAuthData(new Uri(textBoxKey.Text));
            }
            else
            {
                OtpAuthData data = new OtpAuthData();
                string secret = textBoxKey.Text.Replace(" ", string.Empty).Replace("-", string.Empty);
                if (string.IsNullOrEmpty(this.textBoxKey.Text))
                    throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationMissingSecret);

                if (this.radioButtonBase32.Checked)
                    data.Encoding = OtpSecretEncoding.Base32;
                else if (this.radioButtonBase64.Checked)
                    data.Encoding = OtpSecretEncoding.Base64;
                else if (this.radioButtonHex.Checked)
                    data.Encoding = OtpSecretEncoding.Hex;
                else if (this.radioButtonUtf8.Checked)
                    data.Encoding = OtpSecretEncoding.UTF8;

                // Secret validation, will throw an error if invalid
                secret = OtpAuthUtils.correctPlainSecret(secret, this.Data.Encoding);
                OtpAuthUtils.validatePlainSecret(secret, this.Data.Encoding);

                int step = 30;
                if (int.TryParse(this.textBoxStep.Text, out step))
                {
                    if (step <= 0)
                        throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationInvalidInteger);
                }
                else
                    throw new InvalidOtpConfiguration(KeeOtp2Statics.InvalidOtpConfigurationInvalidInteger);
                data.Period = step;

                if (this.radioButtonSix.Checked)
                    data.Digits = 6;
                else if (this.radioButtonEight.Checked)
                    data.Digits = 8;

                if (this.radioButtonSha1.Checked)
                    data.Algorithm = OtpHashMode.Sha1;
                else if (this.radioButtonSha256.Checked)
                    data.Algorithm = OtpHashMode.Sha256;
                else if (this.radioButtonSha512.Checked)
                    data.Algorithm = OtpHashMode.Sha512;

                data.SetPlainSecret(secret);

                return data;
            }
        }

        private void OtpInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
                return;
            try
            {
                List<string> loadedFields = null;
                if (this.Data != null)
                    loadedFields = this.Data.loadedFields;

                this.Data = readData();
                
                this.Data.loadedFields = loadedFields;

                this.entry.CreateBackup(this.host.Database);

                if (this.Data.loadedFields != null && !string.IsNullOrEmpty(OtpAuthUtils.getTotpString(this.Data)))
                    OtpAuthUtils.purgeLoadedFields(this.Data, this.entry);

                if (checkboxOldKeeOtp.Checked)
                {
                    OtpAuthUtils.migrateToKeeOtp1String(this.Data, this.entry);
                }
                else
                {
                    OtpAuthUtils.migrateToBuiltInOtp(this.Data, this.entry);
                }
                

                this.entry.Touch(true);
                this.host.MainWindow.ActiveDatabase.Modified = true;
                this.host.MainWindow.UpdateUI(false, null, false, null, false, null, true);
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

        private void checkBoxCustomSettings_CheckedChanged(object sender, EventArgs e)
        {
            SetCustomSettingsState(this.fullyLoaded);
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
                linkLabelLoadUriScanQR.Visible = false;
                linkLabelLoadUriScanQR.Enabled = false;
                try
                {
                    this.Data = OtpAuthUtils.uriToOtpAuthData(new Uri(textBoxKey.Text));
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

        private void linkLabelMigrate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show(KeeOtp2Statics.MessageBoxMigrationReplacePlaceholder, KeeOtp2Statics.Migration, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OtpAuthUtils.replacePlaceholder(this.entry, KeeOtp2Ext.KeeOtp1PlaceHolder, KeeOtp2Ext.BuiltInPlaceHolder);
            }

            checkboxOldKeeOtp.Checked = false;
            this.DialogResult = DialogResult.OK;
        }

        private void loadData()
        {
            if (this.Data != null)
            {
                this.textBoxKey.Text = this.Data.GetPlainSecret();

                if (this.Data.Period != 30 || this.Data.KeeOtp1Mode ||
                    this.Data.Encoding != OtpSecretEncoding.Base32 ||
                    this.Data.Digits != 6 || this.Data.Algorithm != OtpHashMode.Sha1)
                {
                    this.checkBoxCustomSettings.Checked = true;
                }

                this.textBoxStep.Text = this.Data.Period.ToString();

                this.checkboxOldKeeOtp.Checked = this.Data.KeeOtp1Mode;

                if (this.Data.Encoding == OtpSecretEncoding.Base64)
                {
                    this.radioButtonBase32.Checked = false;
                    this.radioButtonBase64.Checked = true;
                    this.radioButtonHex.Checked = false;
                    this.radioButtonUtf8.Checked = false;
                }
                else if (this.Data.Encoding == OtpSecretEncoding.Hex)
                {
                    this.radioButtonBase32.Checked = false;
                    this.radioButtonBase64.Checked = false;
                    this.radioButtonHex.Checked = true;
                    this.radioButtonUtf8.Checked = false;
                }
                else if (this.Data.Encoding == OtpSecretEncoding.UTF8)
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

                if (this.Data.Digits == 8)
                {
                    this.radioButtonSix.Checked = false;
                    this.radioButtonEight.Checked = true;
                }
                else // default size
                {
                    this.radioButtonSix.Checked = true;
                    this.radioButtonEight.Checked = false;
                }

                if (this.Data.Algorithm == OtpHashMode.Sha256)
                {
                    this.radioButtonSha1.Checked = false;
                    this.radioButtonSha256.Checked = true;
                    this.radioButtonSha512.Checked = false;
                }
                else if (this.Data.Algorithm == OtpHashMode.Sha512)
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
            }
            else
            {
                this.textBoxStep.Text = "30";
                this.radioButtonSha1.Checked = true;
                this.radioButtonSha256.Checked = false;
                this.radioButtonSha512.Checked = false;

                this.radioButtonSix.Checked = true;
                this.radioButtonEight.Checked = false;

                this.radioButtonBase32.Checked = true;
                this.radioButtonBase64.Checked = false;
                this.radioButtonHex.Checked = false;
                this.radioButtonUtf8.Checked = false;
            }

            SetCustomSettingsState(false);
            this.fullyLoaded = true;
        }


        private void SetCustomSettingsState(bool showWarning)
        {
            var useCustom = this.checkBoxCustomSettings.Checked;

            this.radioButtonBase32.Enabled =
                this.radioButtonBase64.Enabled =
                this.radioButtonHex.Enabled =
                this.radioButtonUtf8.Enabled =
                this.radioButtonSix.Enabled =
                this.radioButtonEight.Enabled =
                this.textBoxStep.Enabled =
                this.radioButtonSha1.Enabled =
                this.radioButtonSha256.Enabled =
                this.radioButtonSha512.Enabled =
                this.checkboxOldKeeOtp.Enabled = useCustom;
        }


        private void scanQRCode()
        {
            Uri uri = null;
            IBarcodeReader reader = new BarcodeReader();
            Bitmap bmpScreenshot;
            Graphics gfxScreenshot;

            this.Hide();
            foreach (Screen sc in Screen.AllScreens)
            {
                bmpScreenshot = new Bitmap(sc.Bounds.Width, sc.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                gfxScreenshot = Graphics.FromImage(bmpScreenshot);
                gfxScreenshot.CopyFromScreen(sc.Bounds.X, sc.Bounds.Y, 0, 0, sc.Bounds.Size, CopyPixelOperation.SourceCopy);
                var result = reader.Decode(bmpScreenshot);
                if (result != null)
                    if (result.ToString().StartsWith("otpauth"))
                        uri = new Uri(result.ToString());
            }

            this.Show();

            if (uri != null)
            {
                MessageBox.Show(KeeOtp2Statics.MessageBoxQrCodeFound, KeeOtp2Statics.OtpInformationScanQr, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Data = OtpAuthUtils.uriToOtpAuthData(uri);
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

        private void timerUpdateTotp_Tick(object sender, EventArgs e)
        {
            if (textBoxKey.Text.Length > 0)
            {
                try
                {
                    OtpAuthData data = readData();
                    Totp totp = OtpAuthUtils.getTotp(data);
                    groupBoxKey.Text = String.Format(KeeOtp2Statics.OtpInformationKeyUriPreview, totp.ComputeTotp(OtpTime.getTime()), totp.RemainingSeconds());
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
    }
}
