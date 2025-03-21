using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeePassLib.Utility;
using OtpNet;

namespace KeeOtp2
{
    public abstract class OtpBase
    {
        internal OtpAuthData data;

        public OtpBase(OtpAuthData data)
        {
            this.data = data;
        }

        public abstract string getTotpString();
        public abstract string getTotpString(DateTime timestamp);
        public abstract string getHotpString(long counter);
        public abstract int getRemainingSeconds();
        public abstract int getRemainingSeconds(DateTime timestamp);
    }

    public class OtpTotp : OtpBase
    {
        internal Totp totp;

        public OtpTotp(OtpAuthData data) : base(data)
        {
            this.totp = new Totp(data.Key.ReadData(), data.Period, data.Algorithm, data.Digits, null);
        }

        public override string getTotpString()
        {
            return getTotpString(DateTime.Now);
        }

        public override string getTotpString(DateTime timestamp)
        {
            return totp.ComputeTotp(timestamp);
        }

        public override int getRemainingSeconds()
        {
            return getRemainingSeconds(DateTime.Now);
        }

        public override int getRemainingSeconds(DateTime timestamp)
        {
            return totp.RemainingSeconds(timestamp);
        }

        public override string getHotpString(long counter)
        {
            throw new NotImplementedException();
        }
    }

    public class OtpHotp : OtpBase
    {
        private Hotp hotp;

        public OtpHotp(OtpAuthData data) : base(data)
        {
            this.hotp = new Hotp(data.Key.ReadData(), data.Algorithm, data.Digits);
        }

        public override string getHotpString(long counter)
        {
            return hotp.ComputeHOTP(counter);
        }

        public override string getTotpString()
        {
            throw new NotImplementedException();
        }

        public override string getTotpString(DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public override int getRemainingSeconds()
        {
            throw new NotImplementedException();
        }

        public override int getRemainingSeconds(DateTime timestamp)
        {
            throw new NotImplementedException();
        }
    }

    public class OtpSteam : OtpTotp
    {
        private static readonly char[] STEAMCHARS = new char[] {
            '2', '3', '4', '5', '6', '7', '8', '9', 'B', 'C',
            'D', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q',
            'R', 'T', 'V', 'W', 'X', 'Y'
        };

        public OtpSteam(OtpAuthData data) : base(data) { }

        public override string getTotpString()
        {
            return getTotpString(DateTime.Now);
        }

        public override string getTotpString(DateTime timestamp)
        {
            var elapsedSeconds = (long)Math.Floor(timestamp.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds) / data.Period;
            byte[] codeInterval = BitConverter.GetBytes((ulong)elapsedSeconds);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(codeInterval);

            InMemoryKey key = new InMemoryKey(data.Key.ReadData());
            byte[] hash = key.ComputeHmac(data.Algorithm, codeInterval);

            int start = hash[hash.Length - 1] & 0xf;
            byte[] totp = new byte[4];

            Array.Copy(hash, start, totp, 0, 4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(totp);

            var code = BitConverter.ToUInt32(totp, 0) & 0x7fffffff;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this.data.Digits; i++)
            {
                sb.Append(STEAMCHARS[code % STEAMCHARS.Length]);
                code /= (uint)STEAMCHARS.Length;
            }

            return sb.ToString();
        }
    }
}
