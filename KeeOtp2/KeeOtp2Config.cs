using KeePass;
using System;
using System.Windows.Forms;

namespace KeeOtp2
{
    internal static class KeeOtp2Config
    {
        private const String PATH_PLUGINNAME = "KeeOtp2";

        private const String PATH_USE_HOTKEY = PATH_PLUGINNAME + ".UseHotKey";
        private const String PATH_HOTKEY_SEQUENCE = PATH_PLUGINNAME + ".HotKeySequence";
        private const String PATH_HOTKEY_KEYS = PATH_PLUGINNAME + ".HotKeyKeys";

        private const String PATH_TIME_TYPE = PATH_PLUGINNAME + ".TimeType";
        private const String PATH_FIXED_TIME_OFFSET = PATH_PLUGINNAME + ".FixedTimeOffset";
        private const String PATH_CUSTOM_NTP_SERVER = PATH_PLUGINNAME + ".CustomNTPServer";
        private const String PATH_OVERRIDE_BUILT_IN = PATH_PLUGINNAME + ".OverrideBuiltIn";

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

        internal static OtpTimeType TimeType
        {
            get
            {
                return (OtpTimeType)Enum.Parse(typeof(OtpTimeType), Program.Config.CustomConfig.GetString(PATH_TIME_TYPE, OtpTimeType.SystemTime.ToString()));
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_TIME_TYPE, value.ToString());
            }
        }

        internal static long FixedTimeOffset
        {
            get
            {
                return Program.Config.CustomConfig.GetLong(PATH_FIXED_TIME_OFFSET, 0);
            }
            set
            {
                Program.Config.CustomConfig.SetLong(PATH_FIXED_TIME_OFFSET, value);
            }
        }

        internal static string CustomNTPServer
        {
            get
            {
                return Program.Config.CustomConfig.GetString(PATH_CUSTOM_NTP_SERVER, "time-a.nist.gov");
            }
            set
            {
                Program.Config.CustomConfig.SetString(PATH_CUSTOM_NTP_SERVER, value.ToString());
            }
        }

        internal static bool OverrideBuiltInTime
        {
            get
            {
                return Program.Config.CustomConfig.GetBool(PATH_OVERRIDE_BUILT_IN, true);
            }
            set
            {
                Program.Config.CustomConfig.SetBool(PATH_OVERRIDE_BUILT_IN, value);
            }
        }
    }
}
