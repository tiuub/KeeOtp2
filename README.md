# KeeOtp2
[![Latest Release](https://img.shields.io/github/v/release/tiuub/KeeOtp2)](https://github.com/tiuub/KeeOtp2/releases/latest)
[![GitHub All Releases](https://img.shields.io/github/downloads/tiuub/KeeOtp2/total)](https://github.com/tiuub/KeeOtp2/releases/latest)
[![Issues](https://img.shields.io/github/issues/tiuub/KeeOtp2)](https://github.com/tiuub/KeeOtp2/issues)
[![GitHub](https://img.shields.io/github/license/tiuub/KeeOtp2)](https://github.com/tiuub/KeeOtp2/blob/master/LICENSE)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url)


KeeOtp2 is a plugin for [KeePass](http://keepass.info). It provides a form to display one time passwords. The TOTP secret keys are stored in a normalized format, so this plugin is fully compatible with the built-in OTP function. It also can be used as a GUI for the built-in OTP function. *(The plugin can also migrate saved [KeeOtp(1)](https://github.com/tiuub/KeeOtpMirror) secrets, to the new built-in function.)*

> This plugin is based on [KeeOtp(1)](https://github.com/tiuub/KeeOtpMirror), which was originally developed by [devinmartin](https://bitbucket.org/devinmartin). Since [devinmartin](https://bitbucket.org/devinmartin)s website is no longer available, I have reuploaded the original source code [here](https://github.com/tiuub/KeeOtpMirror) (or at [archive.org](http://web.archive.org/web/20200621144226/https://bitbucket.org/devinmartin/keeotp/wiki/Home)).

## Installation

 - Download the latest release [here](https://github.com/tiuub/KeeOtp2/releases/latest)
 - Copy the KeeOtp2.plgx in the KeePass plugins directory and restart the application.

### Alternative Installation

- Using [chocolatey](https://chocolatey.org/) in Powershell with `choco install keepass-plugin-keeotp2`



## Usage

### Configure TOTP

Rightclick on a entry, click on **KeeOtp2** and press **Configure TOTP**.

This will open the configuration window.

![Configuration Window](Screenshots/screenshot-1.jpg)

Enter the key you have received and press **OK**.

Now you can see the TOTP by rightclicking an entry, hovering on **KeeOtp2** and clicking **Show TOTP**.



## Migrate from KeeOtp1 to KeeOtp2/Built-In OTP

Click on **Tools**, navigate to **KeeOtp2** and press **Settings**.

There you can migrate all of your entries to KeeOtp2/Built-In OTP by selecting your target format and clicking on **OK**.

![Settings](Screenshots/screenshot-2.jpg)


### Another way to migrate

Just right click any entry in your database and click on **Timed One Time Password**.

Rightclick on an entry, hover **KeeOtp2** and click **Configure TOTP**. When the configuration window is opened, click on **Migrate to Built-In** (blue label) in the upper half.



## Auto-Type


### HotKey
Default HotKey: **CTRL + ALT + T**

You can set a global hotkey to auto-type your TOTP.
Therefore you have to click on **Tools**, navigate to **KeeOtp2** and press **Settings**.
There you can activate or disable the global hotkey and set your own key combination.


### Placeholder

This plugin supports the built-in [Auto-Type](https://keepass.info/help/base/autotype.html) function.

Placeholder | Usage
--- | ---
**{TOTP}** | Was used by KeeOtp(1) *(Deprecated)*
**{TIMEOTP}** | Can be used with KeeOtp2 and built-in TOTP *(Recommended)*

Still you can use **{TOTP}**, but its rather recommended to use the built-in placeholder **{TIMEOTP}**.



## Global Time

You can configure your specific time for generating TOTPs. Therefore you have three options in the *Settings* form.

![Settings](Screenshots/screenshot-2.jpg)

Setting | Description
--- | ---
**Use time of system** | This will basically use the time of your system
**Fixed time offset (sec.)** | This will set a fixed time offset to your systemtime. (For example if you know, your system is running 5 seconds behind or 50 seconds forward.)
**Custom NTP server** | This will poll the given NTP server to get the current and correct time.



## Sharing OTP Configuration

You can either share your OTP configuration with an uri string (otpauth://...) or with an QR code.

Therefore you have to rightclick the target entry, hover on **KeeOtp2**, click on **Show TOTP** and click on **Show QR**.

Then you can scan the shown QR code or click on **Copy URI** to copy the uri string to your clipboard.



## Troubleshooting

Sometimes the generation of TOTPs will fail. The most common failure is a wrong system time.

The best way to prove this, is by manually checking your system time or let the plugin prove it for you.
Therefore you have to rightclick an configured entry, hover on **KeeOtp2**, click on **Show TOTP** and if the window opens up, click on **Incorrect?**. 
Then you can press on **Ping NTP server**.

If the ping result is over 5 seconds, you should change your time settings. You can do this on system side or inside the plugin.
Therefore you can check out the section [Global Time](#global-time) to set a fixed time offset or a custom NTP server.



## Download

You can download the .plgx file [here](https://github.com/tiuub/KeeOtp2/releases/latest).



## Compiling

If you want to compile the project by yourself, you have to clone the repository and run the *makeplgx.bat* file.
This will create a new folder in your cloned repository, called *Releases*. There you can find your self-compiled .plgx file.



## License

[![GitHub](https://img.shields.io/github/license/tiuub/KeeOtp2)](https://github.com/tiuub/KeeOtp2/blob/master/LICENSE)



## Dependencies

Dependencie | Source | NuGet | Author | License
--- | --- | --- | --- | ---
**KeeOtp(1)** | [source](https://github.com/tiuub/KeeOtpMirror)/[archive](http://web.archive.org/web/20200621144226/https://bitbucket.org/devinmartin/keeotp/wiki/Home) | - | [Devin Martin](https://bitbucket.org/devinmartin) | [MIT](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/KeeOtp/LICENSE)
**Otp.NET** | [source](https://github.com/kspearrin/Otp.NET) | [NuGet](https://www.nuget.org/packages/Otp.NET) | [Kyle Spearrin](https://github.com/kspearrin) | [MIT](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/Otp.NET/LICENSE)
**Yort.Ntp.Portable** | [source](https://github.com/Yortw/Yort.Ntp) | [NuGet](https://www.nuget.org/packages/Yort.Ntp.Portable/) | [Troy Willmot](https://github.com/Yortw) | [MIT](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/Yort.Ntp.Portable/LICENSE)
**ZXing.Net** | [source](https://github.com/micjahn/ZXing.Net/) | [NuGet](https://www.nuget.org/packages/ZXing.Net/) | [Michael Jahn](https://github.com/micjahn/) | [Apache 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/ZXing.Net/LICENSE)
**NHotkey** | [source](https://github.com/thomaslevesque/NHotkey) | [NuGet](https://www.nuget.org/packages/NHotkey/) | [Thomas Levesque](https://github.com/thomaslevesque) | [Apache 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/NHotkey/LICENSE)
**NHotkey.WindowsForms** | [source](https://github.com/thomaslevesque/NHotkey) | [NuGet](https://www.nuget.org/packages/NHotkey.WindowsForms/) | [Thomas Levesque](https://github.com/thomaslevesque) | [Apache 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/NHotkey.WindowsForms/LICENSE)


### Dependencies (KeeOtp2.Tests)

Dependencie | Source | NuGet | Author | License
--- | --- | --- | --- | ---
**xunit** | [source](https://github.com/xunit/xunit) | [NuGet](https://www.nuget.org/packages/xunit/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/xunit/blob/main/LICENSE)
**xunit.analyzers** | [source](https://github.com/xunit/xunit.analyzers) | [NuGet](https://www.nuget.org/packages/xunit.analyzers/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/xunit.analyzers/blob/main/LICENSE)
**xunit.assert** | [source](https://github.com/xunit/xunit) | [NuGet](https://www.nuget.org/packages/xunit.assert/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/xunit/blob/main/LICENSE)
**xunit.core** | [source](https://github.com/xunit/xunit) | [NuGet](https://www.nuget.org/packages/xunit.core/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/xunit/blob/main/LICENSE)
**xunit.runner.visualstudio** | [source](https://github.com/xunit/visualstudio.xunit) | [NuGet](https://www.nuget.org/packages/xunit.runner.visualstudio/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/visualstudio.xunit/blob/main/License.txt)
**xunit.extensibility.core** | [source](https://github.com/xunit/xunit) | [NuGet](https://www.nuget.org/packages/xunit.extensibility.core/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/xunit/blob/main/LICENSE)
**xunit.extensibility.execution** | [source](https://github.com/xunit/xunit) | [NuGet](https://www.nuget.org/packages/xunit.extensibility.execution/) | [xunit](https://github.com/xunit) | [Apache 2.0](https://github.com/xunit/xunit/blob/main/LICENSE)


### Icons

Icon | Source | Brand | Author | License
--- | --- | --- | --- | ---
**info** | [Google Fonts](https://material.io/resources/icons/?icon=info&style=baseline) | **Material design icons** | [Google](https://about.google) | [Apache License Version 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/MaterialDesignIcons/LICENSE)
**lock** | [Google Fonts](https://material.io/resources/icons/?icon=lock&style=baseline) | **Material design icons** | [Google](https://about.google) | [Apache License Version 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/MaterialDesignIcons/LICENSE)
**qr_code** | [Google Fonts](https://material.io/resources/icons/?icon=qr_code&style=baseline) | **Material design icons** | [Google](https://about.google) | [Apache License Version 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/MaterialDesignIcons/LICENSE)
**schedule** | [Google Fonts](https://material.io/resources/icons/?icon=schedule&style=baseline) | **Material design icons** | [Google](https://about.google) | [Apache License Version 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/MaterialDesignIcons/LICENSE)
**settings** | [Google Fonts](https://material.io/resources/icons/?icon=settings&style=baseline) | **Material design icons** | [Google](https://about.google) | [Apache License Version 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/MaterialDesignIcons/LICENSE)
**help** | [Google Fonts](https://material.io/resources/icons/?icon=help&style=baseline) | **Material design icons** | [Google](https://about.google) | [Apache License Version 2.0](https://github.com/tiuub/KeeOtp2/blob/master/Dependencies/MaterialDesignIcons/LICENSE)
