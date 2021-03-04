using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using KeePassLib;
using KeePassLib.Security;
using OtpSharp;

namespace KeeOtp2
{
    /// <summary>
    /// Class that serializes and deserializes data into the Strings for the entry
    /// </summary>
    public class OtpAuthData
    {
        public Key Key { get; set; }
        public OtpType Type { get; set; }
        public OtpSecretEncoding Encoding { get; set; }

        public OtpHashMode Algorithm { get; set; }

        public int Period { get; set; }
        public int Digits { get; set; }

        public int Counter { get; set; }

        public bool KeeOtp1Mode { get; set; }

        public List<string> loadedFields { get; set; }

        public OtpAuthData()
        {
            this.Type = OtpType.Totp;
            this.Encoding = OtpSecretEncoding.Base32;
            this.Algorithm = OtpHashMode.Sha1;

            this.Counter = 25;
            this.Period = 30;
            this.Digits = 6;

            this.KeeOtp1Mode = false;
        }

        public string GetPlainSecret()
        {
            string secret = null;
            this.Key.UsePlainKey(key =>
            {
                if (this.Encoding == OtpSecretEncoding.Base32)
                    secret = Base32.Encode(key);
                else if (this.Encoding == OtpSecretEncoding.Base64)
                    secret = Convert.ToBase64String(key);
                else if (this.Encoding == OtpSecretEncoding.Hex)
                    secret = KeePassLib.Utility.MemUtil.ByteArrayToHexString(key);
                else if (this.Encoding == OtpSecretEncoding.UTF8)
                    secret = KeePassLib.Utility.StrUtil.Utf8.GetString(key);
            });
            return secret;
        }

        public void SetPlainSecret(string secret)
        {
            if (this.Encoding == OtpSecretEncoding.Base32)
                this.Key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(KeePassLib.Utility.MemUtil.ParseBase32(secret));
            else if (this.Encoding == OtpSecretEncoding.Base64)
                this.Key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(Convert.FromBase64String(secret));
            else if (this.Encoding == OtpSecretEncoding.Hex)
                this.Key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(KeePassLib.Utility.MemUtil.HexStringToByteArray(secret));
            else if (this.Encoding == OtpSecretEncoding.UTF8)
                this.Key = ProtectedKey.CreateProtectedKeyAndDestroyPlaintextKey(KeePassLib.Utility.StrUtil.Utf8.GetBytes(secret));
        }
    }
}
