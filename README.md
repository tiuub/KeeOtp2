# KeeOtp2
[![Latest Release](https://img.shields.io/github/v/release/tiuub/KeeOtp2)](https://github.com/tiuub/KeeOtp2/releases/latest)
[![GitHub All Releases](https://img.shields.io/github/downloads/tiuub/KeeOtp2/total)](https://github.com/tiuub/KeeOtp2/releases/latest)
[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url)

KeeOtp2 is a plugin for [KeePass](http://keepass.info). It provides a form to display one time passwords. The TOTP secret keys are stored in a normalized format, so this plugin is fully compatible with the built-in OTP function. It also can be used as a GUI for the built-in OTP function. *(The plugin can also migrate saved [KeeOtp(1)](https://github.com/tiuub/KeeOtpMirror) secrets, to the new built-in function.)*

> This plugin is based on [KeeOtp(1)](https://github.com/tiuub/KeeOtpMirror), which was originally developed by [devinmartin](https://bitbucket.org/devinmartin). Since [devinmartin](https://bitbucket.org/devinmartin)s website is no longer available, I have reuploaded the original source code [here](https://github.com/tiuub/KeeOtpMirror).

## Installation

 - Download the latest release [here](https://github.com/tiuub/KeeOtp2/releases/latest)
 - Copy the KeeOtp2.plgx in the KeePass plugins directory and restart the application.



## Usage

### Configure TOTP

Rightclick on a entry and click on **Timed One Time Password**.

This will open the configuration window.

![Configuration Window](Screenshots/screenshot-1.jpg)

Enter the key you have received and press **OK**.

The TOTP should now be visible.


### Migrate from KeeOtp1 to KeeOtp2/Built-In OTP

Click on **Tools**, navigate to **KeeOtp2** and press **Settings**.

You can migrate all of your entries to KeeOtp2/Built-In OTP by clicking on **Migrate**.

![Settings](Screenshots/screenshot-2.jpg)

### Or

Just right click any entry in your database and click on **Timed One Time Password**.

Click on **Edit**. When the configuration window is opened, click on **Migrate to Built-In**.

![Configuration Window](Screenshots/screenshot-3.jpg)



## Download

You can download the .plgx file [here](https://github.com/tiuub/KeeOtp2/releases/latest).



## Auto-Type

This plugin supports the built-in [Auto-Type](https://keepass.info/help/base/autotype.html) function.

Placeholder | Usage
--- | ---
**{TOTP}** | Have to be used, with KeeOtp(1) save mode *(Deprecated)*
**{TIMEOTP}** | Can be used with KeeOtp2 and built-in TOTP *(Recommended)*

Still you can use **{TOTP}**, but its rather recommended to use the built-in placeholder **{TIMEOTP}**.


## License

[![GitHub](https://img.shields.io/github/license/tiuub/KeeOtp2)](https://github.com/tiuub/KeeOtp2/blob/master/LICENSE)

Original [KeeOtp(1)](https://github.com/tiuub/KeeOtpMirror) License: MIT

Original [KeeOtp(1)](https://github.com/tiuub/KeeOtpMirror) Developer: [devinmartin](https://bitbucket.org/devinmartin)

### Icons

The icons used in this plugin are from the Oxygen icon set and are used under the Creative Commons Attribution-NonCommercial-NoDerivs 2.5 Generic (CC BY-NC-ND 2.5) license.

The author's website is located here: http://www.oxygen-icons.org
