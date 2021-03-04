using KeePass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeeOtp2
{
    internal static class KeeOtp2Config
    {
        private const String PATH_PLUGINNAME = "KeeOtp2";
        private const String PATH_USE_HOTKEY = PATH_PLUGINNAME + ".UseHotKey";
        private const String PATH_HOTKEY_SEQUENCE = PATH_PLUGINNAME + ".HotKeySequence";
        private const String PATH_HOTKEY_KEYS = PATH_PLUGINNAME + ".HotKeyKeys";

        internal static bool UseHotKey
        {
            get
            {
                return Program.Config.CustomConfig.GetBool(PATH_USE_HOTKEY, true);
            }
            set
            {
                Program.Config.CustomConfig.SetBool(PATH_USE_HOTKEY, value);
            }
        }

        internal static string HotKeySequence
        {
            get
            {
                return Program.Config.CustomConfig.GetString(PATH_HOTKEY_SEQUENCE, KeeOtp2Ext.BuiltInPlaceHolder);
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_HOTKEY_SEQUENCE, value);
            }
        }


        internal static Keys HotKeyKeys
        {
            get
            {
                return (Keys)Enum.Parse(typeof(Keys), Program.Config.CustomConfig.GetString(PATH_HOTKEY_KEYS, (Keys.Control | Keys.Alt | Keys.T).ToString()));
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_HOTKEY_KEYS, value.ToString());
            }
        }
    }
}
