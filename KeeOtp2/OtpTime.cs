using System;
using System.Windows.Forms;
using Yort.Ntp;

namespace KeeOtp2
{
    public enum OtpTimeType
    {
        SystemTime,
        FixedOffset,
        CustomNtpServer
    }

    public static class OtpTime
    {
        private const long TIME_DELTA_VALID_SECONDS = 3600;

        private static long timeDelta = 0;
        private static DateTime timeDeltaValidUntil;

        public static OtpTimeType getTimeType()
        {
            return KeeOtp2Config.TimeType;
        }

        public static long getFixedTimeOffset()
        {
            return KeeOtp2Config.FixedTimeOffset;
        }

        public static string getCustomNtpServer()
        {
            return KeeOtp2Config.CustomNTPServer;
        }

        public static bool getOverrideBuiltInTime()
        {
            return KeeOtp2Config.OverrideBuiltInTime;
        }

        public static DateTime getTime()
        {
            return getTime(getTimeType());
        }

        public static DateTime getTime(OtpTimeType timeType)
        {
            switch (timeType)
            {
                case OtpTimeType.SystemTime:
                    return DateTime.UtcNow;
                case OtpTimeType.FixedOffset:
                    return getTimeFixedOffset(getFixedTimeOffset());
                case OtpTimeType.CustomNtpServer:
                    return getTimeCustomNTPServer();
                default:
                    return DateTime.UtcNow;
            }
        }

        public static DateTime getTimeFixedOffset(long timeDelta)
        {
            return DateTime.UtcNow.AddSeconds(timeDelta);
        }

        public static DateTime getTimeCustomNTPServer()
        {
            return getTimeCustomNTPServer(false);
        }

        public static DateTime getTimeCustomNTPServer(bool ignoreInvalidTimeDelta)
        {
            if (!ignoreInvalidTimeDelta && (timeDeltaValidUntil == null || timeDeltaValidUntil < DateTime.UtcNow))
                pollCustomNtpServer(getCustomNtpServer());
            return DateTime.UtcNow.AddSeconds(timeDelta);
        }



        public static void pollCustomNtpServer()
        {
            pollCustomNtpServer(getCustomNtpServer());
        }

        public static void pollCustomNtpServer(string serverAddress)
        {
            NtpClient ntpClient = new NtpClient(serverAddress);
            ntpClient.TimeReceived += NtpClient_TimeReceived;
            ntpClient.ErrorOccurred += NtpClient_ErrorOccurred;
            ntpClient.BeginRequestTime();
        }

        private static void NtpClient_ErrorOccurred(object sender, NtpNetworkErrorEventArgs e)
        {
            MessageBox.Show("Polling the NTP Server failed. Please confirm your entered address!\n\nError message:\n" + e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void NtpClient_TimeReceived(object sender, NtpTimeReceivedEventArgs e)
        {
            TimeSpan timeDifference = e.CurrentTime.Subtract(DateTime.UtcNow);
            timeDelta = (int)Math.Round(timeDifference.TotalSeconds);
            timeDeltaValidUntil = DateTime.UtcNow.AddSeconds(TIME_DELTA_VALID_SECONDS);
        }
    }
}
