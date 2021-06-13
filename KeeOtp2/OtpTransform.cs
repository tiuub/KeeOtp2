using System;
using System.Text;
using OtpSharp;

namespace KeeOtp2
{
    public enum OtpTransformType
    {
        Digits,
        Steam
    }

    public abstract class OtpTransform
    {
        public abstract Totp getCustomTotp(OtpAuthData data);

        public abstract string transformTotp(Totp totp, DateTime timestamp);

        public static OtpTransform getTransform(OtpTransformType transform)
        {
            switch (transform)
            {
                case OtpTransformType.Steam:
                    return new OtpTransformSteam();
                case OtpTransformType.Digits:
                default:
                    return new OtpTransformDigits();
            }
        }
    }

    public class OtpTransformDigits : OtpTransform
    {
        int digits;

        public override Totp getCustomTotp(OtpAuthData data)
        {
            this.digits = data.Digits;
            return new Totp(data.Key, data.Period, data.Algorithm, data.Digits, null);
        }

        public override string transformTotp(Totp totp, DateTime timestamp)
        {
            return totp.ComputeTotp(timestamp).PadLeft(digits, '0');
        }
    }

    public class OtpTransformSteam : OtpTransform
    {
        private static readonly char[] STEAMCHARS = new char[] {
            '2', '3', '4', '5', '6', '7', '8', '9', 'B', 'C',
            'D', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q',
            'R', 'T', 'V', 'W', 'X', 'Y'
        };
        public override Totp getCustomTotp(OtpAuthData data)
        {
            return new Totp(data.Key, data.Period, data.Algorithm, int.MaxValue.ToString().Length, null);
        }

        public override string transformTotp(Totp totp, DateTime timestamp)
        {
            string code = totp.ComputeTotp(timestamp);
            long.TryParse(code, out long codeNum);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                sb.Append(STEAMCHARS[codeNum % STEAMCHARS.Length]);
                codeNum /= STEAMCHARS.Length;
            }

            return sb.ToString();
        }
    }
}
