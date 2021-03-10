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
        private const int CUSTOM_NTP_SERVER_MAX_RETRIES = 3;
        private const int CUSTOM_NTP_SERVER_RETRY_DELAY = 40000;

        private static long timeDelta = 0;
        private static DateTime timeDeltaValidUntil;

        private static Timer retryTimer;
        private static int retryCounter = 0;

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
            if (retryTimer == null || !retryTimer.Enabled)
            {
                NtpClient ntpClient = new NtpClient(serverAddress);
                ntpClient.TimeReceived += NtpClient_TimeReceived;
                ntpClient.ErrorOccurred += NtpClient_ErrorOccurred;
                ntpClient.BeginRequestTime();
            }
        }

        private static void retryPollCustomNtpServer(int delay)
        {
            if (retryCounter <= CUSTOM_NTP_SERVER_MAX_RETRIES)
            {
                retryTimer = new Timer();
                retryTimer.Interval = delay;
                retryTimer.Tick += (o, e) =>
                {
                    retryTimer.Dispose();
                    retryCounter++;
                    pollCustomNtpServer();
                    
                };
                retryTimer.Start();
            }
            else
            {
                retryCounter = 0;
                timeDeltaValidUntil = DateTime.UtcNow.AddMinutes(20);
                MessageBox.Show("Cant reach the server. Please check your internet!\n\nYour global time was set to the last known from the ntp server or the time of your system!\n\nIf your connection is back up, go to Tools -> KeeOtp2 -> Settings -> Global Time -> Custom NTP server -> Press OK.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void NtpClient_ErrorOccurred(object sender, NtpNetworkErrorEventArgs e)
        {
            if (!OtpAuthUtils.CheckInternetConnection())
                retryPollCustomNtpServer(CUSTOM_NTP_SERVER_RETRY_DELAY);
            else
                MessageBox.Show("Polling the NTP Server failed. Please confirm your entered address!\n\nError message:\n" + e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private static void NtpClient_TimeReceived(object sender, NtpTimeReceivedEventArgs e)
        {
            TimeSpan timeDifference = e.CurrentTime.Subtract(DateTime.UtcNow);
            timeDelta = (int)Math.Round(timeDifference.TotalSeconds);
            timeDeltaValidUntil = DateTime.UtcNow.AddSeconds(TIME_DELTA_VALID_SECONDS);
            retryCounter = 0;
        }
    }
}
