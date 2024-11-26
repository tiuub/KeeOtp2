using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeeOtp2
{
    class KeeOtp2Statics
    {
        public const string PluginName = "KeeOtp2";
        public const string PluginRepository = "https://github.com/tiuub/KeeOtp2";
        public const string PluginUpdateUrl = "https://raw.githubusercontent.com/tiuub/KeeOtp2/master/VERSION";

        public static readonly string OK = "OK";
        public static readonly string Cancel = "Cancel";
        public static readonly string Close = "Close";
        public static readonly string Copy = "Copy";
        public static readonly string Edit = "Edit";
        public static readonly string Reload = "Reload";
        public static readonly string InformationChar = "*";
        public static readonly string SelectorChar = ":";
        public static readonly string HoverInformation = "(*Hover for more information)";
        public static readonly string Dependencies = "Dependencies";
        public static readonly string Dependencie = "Dependencie";
        public static readonly string Author = "Author";
        public static readonly string License = "License";
        public static readonly string GitHubRepository = "GitHub Repository";
        public static readonly string RepositoryLicenseLink = PluginRepository + "#license";
        public static readonly string RepositoryTroubleshooting = PluginRepository + "#troubleshooting";
        public static readonly string Doante = "Donate";
        public static readonly string DonateLink = "https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url";
        public static readonly string Error = "Error";
        public static readonly string Failure = "Failure";
        public static readonly string Period = "Time Step";
        public static readonly string Encoding = "Encoding";
        public static readonly string HashAlgorithm = "Hash Algorithm";
        public static readonly string Base32 = "Base 32";
        public static readonly string Base64 = "Base 64";
        public static readonly string Hex = "Hex";
        public static readonly string Utf8 = "Utf-8";
        public static readonly string General = "General";
        public static readonly string Sha1 = "Sha-1";
        public static readonly string Sha256 = "Sha-256";
        public static readonly string Sha512 = "Sha-512";
        public static readonly string Migration = "Migration";
        public static readonly string HotKey = "HotKey";
        public static readonly string Other = "Other";
        public static readonly string CompatibleWithKeeTrayTotp = "Compatible With KeeTrayTOTP";
        public static readonly string GlobalTime = "Global Time";
        public static readonly string TOTP = "TOTP";
        public static readonly string Information = "Information";
        public static readonly string Actions = "Actions";
        public static readonly string Custom = "Custom";
        public static readonly string Counter = "Counter";
        public static readonly string Type = "Type";
        public static readonly string Length = "Length";
        public static readonly string CommonAbbreviation = "CMN";
        public static readonly string Warning = "Warning";

        public static readonly string About = "About";
        public static readonly string AboutSubline = "KeeOtp2 Plugin.";
        public static readonly string AboutDisclaimer = "KeeOtp2 by tiuub.\nVersion: {0}\nLicense: MIT\n\nKeeOtp2 is based on KeeOtp(1).\nOriginally developed by devinmartin.";
        public static readonly string AboutMessageBoxOpenRepository = "The GitHub Repository will now be opened.\nYou can open the ReadMe and scroll down, until you see Dependencies. There you will find references to the source code, the author and the license of the dependencies.\n\nDo you want to continue?";
        public static readonly string AboutMessageBoxCantLoadLicense = "Cant load license of {0}.\n\nJust try to open the GitHub Repository and scroll down, until Dependencies.There are all licenses!";

        public static readonly string OtpInformation = "Configuration";
        public static readonly string OtpInformationSubline = "Set up the key for generating one time passwords";
        public static readonly string OtpInformationKeyUri = "Key or Uri (otpauth://):";
        public static readonly string OtpInformationKeyUriTotpPreview = OtpInformationKeyUri + " (Preview: {0} - {1})";
        public static readonly string OtpInformationKeyUriHotpPreview = OtpInformationKeyUri + " (Preview: {0})";
        public static readonly string OtpInformationKeyUriInvalid = OtpInformationKeyUri + " (Invalid)";
        public static readonly string OtpInformationLoadUri = "Load Uri";
        public static readonly string OtpInformationScanQr = "Scan QR Code";
        public static readonly string OtpInformationCustomSettings = "Use Custom Settings";
        public static readonly string OtpInformationMigrate = "Migrate to built-in";
        public static readonly string OtpInformationPeriodSeconds = Period + " (Seconds)";
        public static readonly string OtpInformationKeeOtp1String = "KeeOtp1 String (Deprecated)";
        public static readonly string OtpInformationKeeOtp1SaveMode = "Use old KeeOtp save mode";
        public static readonly string OtpInformationCommonAbbreviationExplanation = "(*cmn = common)";

        public static readonly string Settings = "Settings";
        public static readonly string SettingsSubline = "Configure the KeeOtp2 plugin.";
        public static readonly string SettingsMigratieEntryTo = "Migrate every entry to";
        public static readonly string SettingsTOTPGlobalAutoType = "TOTP global auto-type";
        public static readonly string SettingsUseGlobalHotkey = "Use global Hotkey";
        public static readonly string SettingsBeforeScanningTheQRCode = "Before scanning the QR code";
        public static readonly string SettingsDisplayConfirmationPrompt = "Display confirmation prompt";
        public static readonly string SettingsWhenSaveTotpSettings = "When Save TOTP Settings";
        public static readonly string SettingsKeyOfTotpSeed = "Key of TOTP Seed";
        public static readonly string SettingsKeyOfTotpSettings = "Key of TOTP Settings";
        public static readonly string SettingsSetSettingsForKeeTrayTotp = "Set Settings For KeeTrayTOTP";
        public static readonly string SettingsHotKeySequence = "HotKey Sequence";
        public static readonly string SettingsGlobalHotKey = "Global HotKey";
        public static readonly string SettingsContextMenu = "Right-Click Context Menu (changes apply after restart)";
        public static readonly string SettingsCopyTotpShortcut = "Copy TOTP shourtcut";
        public static readonly string SettingsShowCopyTotp = "Show \"Copy TOTP\"";
        public static readonly string SettingsTotpToClipboard = "TOTP to clipboard";
        public static readonly string SettingsUseLocalHotkey = "Use local Hotkey";
        public static readonly string SettingsShowQrCode = "Show \"Show QR Code\"";
        public static readonly string SettingsQrCodeContextMenu = "QR Code context menu";
        public static readonly string SettingsLocalHotKey = "Local Hotkey";
        public static readonly string SettingsUseSystemTime = "Use time of system";
        public static readonly string SettingsPreviewUtc = "Preview (Utc): {0}";
        public static readonly string SettingsFixedTimeOffset = "Fixed time offset (sec.)";
        public static readonly string SettingsCustomNtpServer = "Custom NTP server";
        public static readonly string SettingsOverrideBuiltIn = "Override built-in time";
        public static readonly string SettingsLoadedNEntries = "Loaded {0} entrie(s)!";
        public static readonly string SettingsDoneNofNEntries = "Done {0} of {1} entries!";

        public static readonly string ShowOtp = "Timed Passwords";
        public static readonly string ShowOtpSubline = "Enter this code in the verification system.";
        public static readonly string ShowOtpIncorrect = "Incorrect?";
        public static readonly string ShowOtpNextCode = "Next code";
        public static readonly string ShowOtpShowQr = "Show Qr";
        // The original text is too long
        public static readonly string ShowOtpNextRemaining = TOTP + " - Countdown: {0} - Next code: {1}";
        public static readonly string ShowOtpNextCounter = TOTP + " - Counter: {0} - Next code: {1}";

        public static readonly string ShowQr = "QR Code";
        public static readonly string ShowQrSubline = "Set up your TOTP on other devices.";
        public static readonly string ShowQrDisclamer = "QrCode - Scan with your favourite Authenticator App";
        public static readonly string ShowQrCopyUri = "Copy Uri";
        public static readonly string ShowQrTimeout = ShowQr + " - Timeout in {0} seconds.";
        public static readonly string ShowQrExpired = "Due to security reasons,\r\nthe QR Code was removed\r\nafter {0} seconds.\r\n\r\nPress Reload,\r\nto show it again!";

        public static readonly string Troubleshooting = "Troubleshooting";
        public static readonly string TroubleshootingSubline = "Try these actions, if your code is wrong.";
        public static readonly string TroubleshootingInformation = "There are many things that can cause an incorrect code. We'll go through the most common things here.";
        public static readonly string TroubleshootingPingNtpServer = "Ping NTP server";
        public static readonly string TroubleshootingChangeSettings = "Change settings";
        public static readonly string TroubleshootingCheckWebsite = "Check the troubleshooting website";
        public static readonly string TroubleshootingPingResult = "You are {0} milliseconds ({1} seconds) {2}.";
        public static readonly string TroubleshootingPingResultOk = TroubleshootingPingResult + " All fine!";
        public static readonly string TroubleshootingPingResultModerate = TroubleshootingPingResult + " It may work, but you should check your time settings!";
        public static readonly string TroubleshootingPingResultBad = TroubleshootingPingResult + " Generating TOTPs wont work. You should definitely check your time settings or visit the troubleshooting website!";

        public static readonly string MessageBoxScanQrCode = "Ensure that the QRCode is somewhere visible on the screen.\nThe plugin will look for any QRCode on the screen.\n\nPress 'OK' to start the scan!";
        public static readonly string MessageBoxQrCodeFound = "Great! The QRCode was found and the credentials will now be configured for you!";
        public static readonly string MessageBoxQrCodeNotFound = "No QRCodes found!\n\nPlease ensure that the QRCode is somewhere visible on the screen.\n\nPress 'Retry' to restart the scan!";
        public static readonly string MessageBoxMigrationConfirmation = "Do you really want to migrate?\n\nMigration: {0}";
        public static readonly string MessageBoxMigrationReplacePlaceholder = "Do you want to replace the Auto-Type key {0} with the key {1}?";
        public static readonly string MessageBoxMigrationRemoveStringAfterMigration = "Do you want to remove the string fields after successful migration?";
        public static readonly string MessageBoxMigrationForcedKeeOtp1Mode = "Some of your entries are forced to KeeOtp(1). This means, if you would migrate them, they would definitely stop working. This manly happens to entries, configured for Steam.";
        public static readonly string MessageBoxCantParseEntry = "Cant parse the following entry:\nGroup: {0}\nTitle: {1}\nUsername: {2}\n\bPlease check the format of this entry.";
        public static readonly string MessageBoxShowQrWrongEncoding = "QRCodes can only be used with Base32 secret encoding.\n\nYour encoding: {0}";
        public static readonly string MessageBoxSelectedMultipleEntries = "Please select only one entry";
        public static readonly string MessageBoxOtpNotConfigured = "Must configure TOTP on this entry. Do you want to do this now?";
        public static readonly string MessageBoxOtpNotProprietary = "Your configuration for your OTP is not proprietary. This might rise up some issues, while sharing this configuration with the uri or the qr code.\n\nPlease remember this, if you run into issues.\n\nPress OK to continue.";
        public static readonly string MessageBoxSettingsRestartNotification = "You have changed some settings which require a restart of KeePass.\n\nPlease restart KeePass, if you want to use these features.";
        

        public static readonly string ToolStripMenuConfigure = "Configure TOTP";
        public static readonly string ToolStripMenuShowOtp = "Show TOTP";
        public static readonly string ToolStripMenuShowQrCode = "Show QR Code";
        public static readonly string ToolStripMenuCopyOtp = "Copy TOTP";

        public static readonly string ToolTipMigrateHeadline = "Why I am seeing this?";
        public static readonly string ToolTipMigrate = "Since KeePass 2.47, TOTPs can generated by a built-in function.\nYou can use this button to easily migrate to the built-in function.\n\n(It is also recommended!)";
        public static readonly string ToolTipHotKeySequence = "Click inside the box to change the sequence!";
        public static readonly string ToolTipHotKeyCombination = "Click inside the box to change tho combination!";
        public static readonly string ToolTipHotKeyNotAvailable = "HotKeys are currently not supported on your system! They are only supported on Windows right now!";
        public static readonly string ToolTipContextMenuItem = "This will give you a shortcut to copy the current TOTP to your clipboard. You can access it, by right-clicking on a entry.";
        public static readonly string ToolTipOverrideBuiltInTime = "If set true, this plugin will generate the TOTP even if\n{TIMEOTP} placeholder is set. This will be only done\nwhen using a time offset or a custom NTP server!";
        public static readonly string ToolTipOtpInformationUseCustomSettings = "You should only modify these settings, if you know what you are doing.\nIn the most cases, these settings are not needed to change.";
        public static readonly string ToolTipOtpInformationUseOldKeeOtpSaveMode = "This setting is here to guarantee the compatability to older versions of KeeOtp.\nIt indicates whether you are using the old KeeOtp save mode or the new one.";
        public static readonly string ToolTipShowQrCode = "Through this QR Code you can configure this OTP on other devices. You can scan this QR Code with Google Authenticator for example.";
        public static readonly string ToolTipShowQrCodeCopyUri = "This will set an uri string to your clipboard.\n\nCurrent uri string:\n{0}";
        public static readonly string ToolTipShowQrCodeReload = "Due to security reasons, the QR code will become invisble after a while.\nThis is, because an QR code can be easily scanned by everyone who walks by your computer.\nThis process can be done within a few seconds, without your knowledge.";
        public static readonly string ToolTipTroubleshootingPingNtpServer = "Sometimes your computer time is running behind. To validate your local time, you can ping a NTP server.";
        public static readonly string ToolTipTroubleshootingChangeSettings = "In the settings, you can configure a custom time offset or a custom NTP server.";
        public static readonly string ToolTipTroubleshootingWebsite = "This will open the troubleshooting website of this plugin.";

        public static readonly string InvalidOtpConfiguration = "Invalid Otp Configuration!";
        public static readonly string InvalidOtpConfigurationMissingSecret = "Your configuraiton does not contain a secret. A secret is required!";
        public static readonly string InvalidOtpConfigurationInvalidInteger = "The time step must be a non-zero positive integer. The standard value is 30.";
        public static readonly string InvalidBase32Format = "Invalid Base32 Format!";
        public static readonly string InvalidBase64Format = "Invalid Base64 Format!";
        public static readonly string InvalidHexFormat = "Invalid Hex Format!";
        public static readonly string InvalidUriFormat = "Invalid Uri Format!";
        public static readonly string MessageBoxException = "There happened an error.Please check your entered key and your settings!\n\nError message:\n{0}";

        public static readonly string MessageBoxHotkeyRegistrationFailed = "Registration of the global hotkey failed. The given hotkey may be already used by another program. Try changing the hotkey or deactivate the option, if you dont need it.\n\nError message:\n{0}";
    }
}
