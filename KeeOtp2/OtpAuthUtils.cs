using KeePassLib;
using KeePassLib.Security;
using OtpSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace KeeOtp2
{
    public class OtpAuthUtils
    {
        const string StringDictionaryKey = "otp";

        const string keyParameter = "key";
        const string typeParameter = "type";
        const string stepParameter = "step";
        const string sizeParameter = "size";
        const string encodingParameter = "encoding";
        const string counterParameter = "counter";
        const string otpHashModeParameter = "otpHashMode";

        const string builtInOtpPrefix = "TimeOtp";

        const string builtInOtpHashModeSha1 = "HMAC-SHA-1";
        const string builtInOtpHashModeSha256 = "HMAC-SHA-256";
        const string builtInOtpHashModeSha512 = "HMAC-SHA-512";

        public static OtpAuthData loadData(PwEntry entry)
        {
            //try
            //{
                if (checkKeeOtp1Mode(entry))
                {
                    OtpAuthData data = loadDataFromKeeOtp1String(entry);
                    data.KeeOtp1Mode = true;
                    return data;
                }
                else if (entry.Strings.GetKeys().Any(x => x.StartsWith(builtInOtpPrefix)))
                {
                    OtpAuthData data = loadDataFromBuiltInOtp(entry);
                    return data;
                }
                return null;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public static bool checkKeeOtp1Mode(PwEntry entry)
        {
            if (entry.Strings.Exists(StringDictionaryKey))
                return true;
            return false;
        }

        public static PwEntry purgeLoadedFields(OtpAuthData data, PwEntry entry)
        {
            if (data != null && data.loadedFields != null)
                foreach (string field in data.loadedFields)
                {
                    if (entry.Strings.Exists(field))
                        entry.Strings.Remove(field);
                }
            return entry;
        }

        public static OtpAuthData loadDataFromKeeOtp1String(PwEntry entry)
        {
            NameValueCollection parameters = ParseQueryString(entry.Strings.Get(StringDictionaryKey).ReadString());

            if (parameters[keyParameter] == null)
                throw new ArgumentException("Must have a key in the data");

            OtpAuthData otpData = new OtpAuthData();

            otpData.loadedFields = new List<string>() { StringDictionaryKey };

            if (parameters[encodingParameter] != null)
                otpData.Encoding = (OtpSecretEncoding)Enum.Parse(typeof(OtpSecretEncoding), parameters[encodingParameter]);

            otpData.SetPlainSecret(parameters[keyParameter].Replace("%3d", "="));

            if (parameters[typeParameter] != null)
                otpData.Type = (OtpType)Enum.Parse(typeof(OtpType), parameters[typeParameter]);

            if (parameters[otpHashModeParameter] != null)
                otpData.OtpHashMode = (OtpHashMode)Enum.Parse(typeof(OtpHashMode), parameters[otpHashModeParameter]);

            if (otpData.Type == OtpType.Totp)
                otpData.Step = GetIntOrDefault(parameters, stepParameter, 30);
            else if (otpData.Type == OtpType.Hotp)
                otpData.Counter = GetIntOrDefault(parameters, counterParameter, 0);

            otpData.Size = GetIntOrDefault(parameters, sizeParameter, 6);

            return otpData;
        }

        public static OtpAuthData loadDataFromBuiltInOtp(PwEntry entry)
        {
            OtpAuthData otpData = new OtpAuthData();

            otpData.loadedFields = new List<string>();

            string secretBase32Key = builtInOtpPrefix + "-Secret-Base32";
            string secretBase64Key = builtInOtpPrefix + "-Secret-Base64";
            string secretHexKey = builtInOtpPrefix + "-Secret-Hex";
            string secretUTF8Key = builtInOtpPrefix + "-Secret";
            if (entry.Strings.Exists(secretBase32Key))
            {
                otpData.Encoding = OtpSecretEncoding.Base32;
                otpData.SetPlainSecret(entry.Strings.Get(secretBase32Key).ReadString());
                otpData.loadedFields.Add(secretBase32Key);
            }
            else if (entry.Strings.Exists(secretBase64Key))
            {
                otpData.Encoding = OtpSecretEncoding.Base64;
                otpData.SetPlainSecret(entry.Strings.Get(secretBase64Key).ReadString());
                otpData.loadedFields.Add(secretBase64Key);
            }
            else if (entry.Strings.Exists(secretHexKey))
            {
                otpData.Encoding = OtpSecretEncoding.Hex;
                otpData.SetPlainSecret(entry.Strings.Get(secretHexKey).ReadString());
                otpData.loadedFields.Add(secretHexKey);
            }
            else if (entry.Strings.Exists(secretUTF8Key))
            {
                otpData.Encoding = OtpSecretEncoding.UTF8;
                otpData.SetPlainSecret(entry.Strings.Get(secretUTF8Key).ReadString());
                otpData.loadedFields.Add(secretUTF8Key);
            }
            else
                return null;

            string lengthKey = builtInOtpPrefix + "-Length";
            if (entry.Strings.Exists(lengthKey))
            {
                int size;
                if (int.TryParse(entry.Strings.Get(lengthKey).ReadString(), out size))
                {
                    otpData.Size = size;
                    otpData.loadedFields.Add(lengthKey);
                }
            }

            string stepKey = builtInOtpPrefix + "-Period";
            if (entry.Strings.Exists(stepKey))
            {
                int step;
                if (int.TryParse(entry.Strings.Get(stepKey).ReadString(), out step))
                {
                    otpData.Step = step;
                    otpData.loadedFields.Add(stepKey);
                }
            }

            string hashModeKey = builtInOtpPrefix + "-Algorithm";
            if (entry.Strings.Exists(hashModeKey))
            {
                string hashMode = entry.Strings.Get(hashModeKey).ReadString();
                if (hashMode == builtInOtpHashModeSha1)
                    otpData.OtpHashMode = OtpHashMode.Sha1;
                else if (hashMode == builtInOtpHashModeSha256)
                    otpData.OtpHashMode = OtpHashMode.Sha256;
                else if (hashMode == builtInOtpHashModeSha512)
                    otpData.OtpHashMode = OtpHashMode.Sha512;
                otpData.loadedFields.Add(hashModeKey);
            }

            return otpData;
        }

        public static PwEntry migrateToKeeOtp1String(OtpAuthData data, PwEntry entry)
        {
            NameValueCollection collection = new NameValueCollection();

            collection.Add(keyParameter, data.GetPlainSecret().Replace("=", "%3d"));

            if (data.Type != OtpType.Totp)
                collection.Add(typeParameter, data.Type.ToString());

            if (data.Type == OtpType.Hotp)
                collection.Add(counterParameter, data.Counter.ToString());
            else if (data.Type == OtpType.Totp)
            {
                if (data.Step != 30)
                    collection.Add(stepParameter, data.Step.ToString());
            }

            if (data.Size != 6)
                collection.Add(sizeParameter, data.Size.ToString());

            if (data.OtpHashMode != OtpHashMode.Sha1)
                collection.Add(otpHashModeParameter, data.OtpHashMode.ToString());

            if (data.Encoding != OtpSecretEncoding.Base32)
                collection.Add(encodingParameter, data.Encoding.ToString());

            string output = string.Empty;
            foreach (var key in collection.AllKeys)
            {
                output += string.Format("{0}={1}&", key, collection[key]);
            }

            entry.Strings.Set(StringDictionaryKey, new ProtectedString(true, output.TrimEnd('&')));

            return entry;
        }

        public static PwEntry migrateToBuiltInOtp(OtpAuthData data, PwEntry entry)
        {
            if (data.Encoding == OtpSecretEncoding.Base32)
                entry.Strings.Set(builtInOtpPrefix + "-Secret-Base32", new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.Base64)
                entry.Strings.Set(builtInOtpPrefix + "-Secret-Base64", new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.Hex)
                entry.Strings.Set(builtInOtpPrefix + "-Secret-Hex", new ProtectedString(true, data.GetPlainSecret()));
            else if (data.Encoding == OtpSecretEncoding.UTF8)
                entry.Strings.Set(builtInOtpPrefix + "-Secret", new ProtectedString(true, data.GetPlainSecret()));

            if (data.Size != 6)
                entry.Strings.Set(builtInOtpPrefix + "-Length", new ProtectedString(false, data.Size.ToString()));

            if (data.Step != 30)
                entry.Strings.Set(builtInOtpPrefix + "-Period", new ProtectedString(false, data.Step.ToString()));

            if (data.OtpHashMode != OtpHashMode.Sha1)
            {
                if (data.OtpHashMode == OtpHashMode.Sha1)
                    entry.Strings.Set(builtInOtpPrefix + "-Algorithm", new ProtectedString(false, builtInOtpHashModeSha1));
                else if (data.OtpHashMode == OtpHashMode.Sha256)
                    entry.Strings.Set(builtInOtpPrefix + "-Algorithm", new ProtectedString(false, builtInOtpHashModeSha256));
                else if (data.OtpHashMode == OtpHashMode.Sha512)
                    entry.Strings.Set(builtInOtpPrefix + "-Algorithm", new ProtectedString(false, builtInOtpHashModeSha512));
            }

            return entry;
        }

        /// <remarks>
        /// Hacky query string parsing.  This was done due to reports
        /// of people with just a 3.5 or 4.0 client profile getting errors
        /// as the System.Web assembly where .net's implementation of
        /// Url encoding and query string parsing is located.
        /// 
        /// This should be fine since the only thing stored in the string
        /// that needs to be encoded or decoded is the '=' sign.
        /// </remarks>
        private static NameValueCollection ParseQueryString(string data)
        {
            var collection = new NameValueCollection();

            var parameters = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                if (parameter.Contains("="))
                {
                    var pieces = parameter.Split('=');
                    if (pieces.Length != 2)
                        continue;

                    collection.Add(pieces[0], pieces[1].Replace("%3d", "="));
                }
            }

            return collection;
        }

        private static int GetIntOrDefault(NameValueCollection parameters, string parameterKey, int defaultValue)
        {
            if (parameters[parameterKey] != null)
            {
                int step;
                if (int.TryParse(parameters[parameterKey], out step))
                    return step;
                else
                    return defaultValue;
            }
            else
                return defaultValue;
        }
    }
}
