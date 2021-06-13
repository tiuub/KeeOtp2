using System;
using System.Text;
using OtpSharp;

namespace KeeOtp2
{
    public class OtpTotp
    {
        private readonly OtpTransform transform;
        private readonly Totp totp;
        private readonly OtpAuthData data;

        public OtpTotp(OtpAuthData data)
        {
            this.data = data;
            transform = OtpTransform.getTransform(data.Transform);
            totp = transform.getCustomTotp(data);
        }

        public int getRemainingSeconds()
        {
            return totp.RemainingSeconds();
        }

        public string getTotpString()
        {
            return getTotpString(DateTime.Now);
        }

        public string getTotpString(DateTime timestamp)
        {
            return transform.transformTotp(totp, timestamp);
        }
    }
}
